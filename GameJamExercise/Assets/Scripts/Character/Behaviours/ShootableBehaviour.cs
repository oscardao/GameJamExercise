using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootableBehaviour : MonoBehaviour, ISpecialEnemyBehaviour {

    [Header("Effects")]
    [SerializeField]
    private Flip flip;
    [SerializeField]
    private float flipDuration;

    [Header("Data")]
    [SerializeField]
    private World world;
    [SerializeField]
    private GameObject dangerIndicatorPrefab;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform projectileSpawnPoint;

    [Header("Animations")]
    [SerializeField]
    private string prepareAttackTrigger = "onPrepareAttack";
    [SerializeField]
    private string onAttackTrigger = "onAttack";
    [SerializeField]
    private string onIdleTrigger = "onIdle";

    private IAnimateable animator;
    private IPositionable positionable;
    private ITargeting targeting;

    private Stack<GameObject> activeDangerIcons;
    private List<WorldTile> targetedTiles;
    private Vector2Int shootingDirection;

    private void Awake() {
        this.activeDangerIcons = new Stack<GameObject>();
        this.targetedTiles = new List<WorldTile>();
        this.animator = GetComponent<IAnimateable>();
        this.positionable = GetComponent<IPositionable>();
        this.targeting = GetComponent<ITargeting>();
    }

    private void OnDisable() {
        DestroyIndicators();
        this.positionable.OnReposition.RemoveListener(OnReposition);
    }

    public IEnumerator Prepare() {
        this.positionable.OnReposition.AddListener(OnReposition);
        this.animator.SetTrigger(this.prepareAttackTrigger);
        this.animator.SetBool("isPrepared", true);

        List<WorldTile> tiles = this.world.GetNeighbourTiles(this.positionable.WorldTile);
        WorldTile tileDirection = tiles[Random.Range(0, tiles.Count)];

        this.shootingDirection = tileDirection.Position - this.positionable.WorldTile.Position;

        yield return UpdateIndicators(0.1f);
    }

    public void OnReposition() {
        StartCoroutine(UpdateIndicators(0));
    }

    public IEnumerator UpdateIndicators(float delay) {
        DestroyIndicators();

        Vector2Int nextDirection = this.positionable.WorldTile.Position + this.shootingDirection;

        if (this.world.ContainsTile(nextDirection)) {
            yield return this.flip.FlipObject(this.world.GetTile(nextDirection).WorldPosition, this.flipDuration, gameObject);
        }

        while (this.world.ContainsTile(nextDirection)) {
            WorldTile tile = this.world.GetTile(nextDirection);
            this.targetedTiles.Add(tile);

            GameObject newIndicator = Instantiate(this.dangerIndicatorPrefab, tile.transform);
            newIndicator.transform.position = tile.WorldPosition;
            this.activeDangerIcons.Push(newIndicator);

            List<WorldTile> nextTiles = this.world.GetNeighbourTiles(tile);
            bool hasNextTile = false;
            for (int i = 0; i < nextTiles.Count; i++) {
                if (nextTiles[i].Position.Equals(nextDirection + this.shootingDirection)) {
                    nextDirection = nextDirection + this.shootingDirection;
                    hasNextTile = true;
                }
            }

            yield return new WaitForSeconds(delay);
            if (!hasNextTile) {
                break;
            }
        }
    }

    public IEnumerator Perform() {
        this.positionable.OnReposition.RemoveListener(OnReposition);
        this.animator.SetBool("isPrepared", false);
        this.animator.SetTrigger(this.onAttackTrigger);


        if (this.targetedTiles.Count > 0) {
            WorldTile endTile = this.targetedTiles[0];
            bool tileHasTarget = false;
            for (int i = 0; i < this.targetedTiles.Count; i++) {
                endTile = this.targetedTiles[i];
                if (endTile.ObjectOnTile == this.targeting.Target) {
                    tileHasTarget = true;
                    break;
                }
            }

            DestroyIndicators();

            GameObject projectile = Instantiate(this.projectilePrefab, this.projectileSpawnPoint.position, Quaternion.identity);
            Projectile projectileMono = projectile.GetComponent<Projectile>();
            Vector2 projectTileDirection = endTile.WorldPosition - this.positionable.WorldTile.WorldPosition;

            if (tileHasTarget) {
                projectileMono.HitTarget(gameObject, this.targeting.Target, this.projectileSpawnPoint);
                yield return new WaitForSeconds(projectileMono.onDamageDuration + projectileMono.onHitTargetDuration);
            } else {
                projectileMono.ActivateProjectile(projectTileDirection);
                yield return new WaitForSeconds(0.3f);

            }

        } else {
            DestroyIndicators();
            yield return new WaitForSeconds(0.1f);
        }
        this.animator.SetTrigger(this.onIdleTrigger);
    }

    private void DestroyIndicators() {
        while (this.activeDangerIcons.Count > 0) {
            Destroy(this.activeDangerIcons.Pop());
        }
        this.targetedTiles.Clear();
    }

}
