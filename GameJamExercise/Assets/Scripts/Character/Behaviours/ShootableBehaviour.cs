using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootableBehaviour : MonoBehaviour {

    [Header("Effects")]
    [SerializeField]
    private Flip flip;
    [SerializeField]
    private float flipDuration;

    [Header("Data")]
    [SerializeField]
    private World world;
    [SerializeField]
    private GameObject dangerIcon;
    [SerializeField]
    private GameObject projectile;
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
    private Vector2Int shootingDirection;
    private int numberOfTilesToTravel;

    private void Awake() {
        this.activeDangerIcons = new Stack<GameObject>();
        this.animator = GetComponent<IAnimateable>();
        this.positionable = GetComponent<IPositionable>();
        this.targeting = GetComponent<ITargeting>();
    }

    private void OnDisable() {
        this.positionable.OnReposition.RemoveListener(OnReposition);
    }

    public IEnumerator PrepareShot() {
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
        while (this.activeDangerIcons.Count > 0) {
            Destroy(this.activeDangerIcons.Pop());
        }

        Vector2Int nextDirection = this.positionable.WorldTile.Position + this.shootingDirection;

        if (this.world.ContainsTile(nextDirection)) {
            yield return this.flip.FlipObject(this.world.GetTile(nextDirection).WorldPosition, this.flipDuration, gameObject);
        }

        this.numberOfTilesToTravel = 0;
        while (this.world.ContainsTile(nextDirection)) {
            this.numberOfTilesToTravel++;
            WorldTile tile = this.world.GetTile(nextDirection);
            GameObject newIndicator = Instantiate(this.dangerIcon, transform);
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

    public IEnumerator Shoot() {
        this.positionable.OnReposition.RemoveListener(OnReposition);
        this.animator.SetBool("isPrepared", false);
        this.animator.SetTrigger(this.onAttackTrigger);

        while (this.activeDangerIcons.Count > 0) {
            Destroy(this.activeDangerIcons.Pop());
        }

        Vector2Int targetedTile = this.shootingDirection + this.positionable.WorldTile.Position;
        if (this.world.ContainsTile(targetedTile)) {
            Vector2 projectTileDirection = this.world.GetTile(targetedTile).WorldPosition - this.positionable.WorldTile.WorldPosition;

            GameObject projectile = Instantiate(this.projectile, this.projectileSpawnPoint.position, Quaternion.identity);
            IProjectile projectileMono = projectile.GetComponent<IProjectile>();
            projectileMono.Activate(projectTileDirection);

            this.animator.SetTrigger(this.onIdleTrigger);
            yield return new WaitForSeconds(this.numberOfTilesToTravel * projectileMono.Speed * Time.deltaTime);
        } else {
            yield return new WaitForSeconds(0.1f);
            this.animator.SetTrigger(this.onIdleTrigger);
        }

    }

}
