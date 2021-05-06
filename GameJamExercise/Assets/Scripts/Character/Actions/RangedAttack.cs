using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Ranged Attack")]
public class RangedAttack : ScriptableObject {
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

    [Header("Animations")]
    [SerializeField]
    private string prepareAttackTrigger = "onPrepareAttack";
    [SerializeField]
    private string onAttackTrigger = "onAttack";


    public IEnumerator PrepareShot(GameObject performer, Stack<GameObject> activeDangerIcons) {
        IAnimateable animator = performer.GetComponent<IAnimateable>();
        animator.SetTrigger(this.prepareAttackTrigger);
        animator.SetBool("isPrepared", true);

        IShootable shootable = performer.GetComponent<IShootable>();
        IPositionable positionable = performer.GetComponent<IPositionable>();

        List<WorldTile> tiles = this.world.GetNeighbourTiles(positionable.WorldTile);
        WorldTile tileDirection = tiles[Random.Range(0, tiles.Count)];

        shootable.ShootDirection = tileDirection.Position - positionable.WorldTile.Position;

        Vector2Int nextDirection = positionable.WorldTile.Position + shootable.ShootDirection;

        yield return this.flip.FlipObject(new Vector3(nextDirection.x, nextDirection.y), this.flipDuration, performer);

        while (this.world.ContainsTile(nextDirection)) {
            WorldTile tile = this.world.GetTile(nextDirection);
            GameObject newIndicator = Instantiate(this.dangerIcon, performer.transform);
            newIndicator.transform.position = tile.WorldPosition;
            activeDangerIcons.Push(newIndicator);

            List<WorldTile> nextTiles = this.world.GetNeighbourTiles(tile);
            bool hasNextTile = false;
            for (int i = 0; i < nextTiles.Count; i++) {
                if (nextTiles[i].Position.Equals(nextDirection + shootable.ShootDirection)) {
                    nextDirection = nextDirection + shootable.ShootDirection;
                    hasNextTile = true;
                }
            }

            yield return new WaitForSeconds(0.1f);
            if (!hasNextTile) {
                break;
            }
        }
    }

    public IEnumerator UpdateIndicators() {
        yield return null;
    }

    public IEnumerator Shoot(GameObject performer, Stack<GameObject> activeDangerIcons) {
        IAnimateable animator = performer.GetComponent<IAnimateable>();
        animator.SetBool("isPrepared", false);
        animator.SetTrigger(this.onAttackTrigger);

        while (activeDangerIcons.Count > 0) {
            Destroy(activeDangerIcons.Pop());
        }

        IShootable shootable = performer.GetComponent<IShootable>();
        GameObject projectile = Instantiate(this.projectile, shootable.ProjectileSpawn.position, Quaternion.identity);



        yield return null;
    }

}
