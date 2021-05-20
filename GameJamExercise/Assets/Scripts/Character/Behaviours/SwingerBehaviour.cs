using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwingerBehaviour : MonoBehaviour, ISpecialEnemyBehaviour {

    [Header("Data")]
    [SerializeField]
    private World world;
    [SerializeField]
    private GameObject dangerIndicatorPrefab;
    private Stack<GameObject> activeIndicators;

    private IPositionable positionable;
    private ITargeting targeting;
    private IAnimateable animator;

    private List<WorldTile> targetedTiles;

    [Header("Animations")]
    [SerializeField]
    private string prepareAttackTrigger = "onPrepareAttack";
    [SerializeField]
    private string onIdleTrigger = "onIdle";

    [SerializeField]
    private BaseAction attackAction;

    private void Awake() {
        this.activeIndicators = new Stack<GameObject>();
        this.targetedTiles = new List<WorldTile>();
        this.positionable = GetComponent<IPositionable>();
        this.targeting = GetComponent<ITargeting>();
        this.animator = GetComponent<IAnimateable>();
    }

    public IEnumerator Perform() {
        this.positionable.OnReposition.RemoveListener(OnReposition);
        this.animator.SetBool("isPrepared", false);

        DestroyIndicators();

        for (int i = 0; i < this.targetedTiles.Count; i++) {
            if (this.targetedTiles[i].ObjectOnTile == this.targeting.Target) {

                this.attackAction.Perform(this.targetedTiles[i], gameObject);
                yield return new WaitForSeconds(this.attackAction.Duration);
                yield break;
            }
        }

        this.animator.SetTrigger(this.onIdleTrigger);
    }

    public IEnumerator Prepare() {
        this.positionable.OnReposition.AddListener(OnReposition);
        this.animator.SetTrigger(this.prepareAttackTrigger);
        this.animator.SetBool("isPrepared", true);
        yield return UpdateIndicators(0.1f);
    }

    public void OnReposition() {
        StartCoroutine(UpdateIndicators(0));
    }

    private IEnumerator UpdateIndicators(float delay) {
        DestroyIndicators();

        this.targetedTiles = this.world.GetDiagonalNeighbourTiles(this.positionable.WorldTile);

        for (int i = this.targetedTiles.Count - 1; i >= 0; i--) {
            WorldTile tile = this.targetedTiles[i];
            GameObject indicator = Instantiate(this.dangerIndicatorPrefab, tile.transform);
            indicator.transform.position = tile.WorldPosition;
            this.activeIndicators.Push(indicator);
            yield return new WaitForSeconds(delay);
        }

    }

    private void OnDisable() {
        DestroyIndicators();
        this.positionable.OnReposition.RemoveListener(OnReposition);
    }

    private void DestroyIndicators() {
        while (this.activeIndicators.Count > 0) {
            Destroy(this.activeIndicators.Pop());
        }

    }
}
