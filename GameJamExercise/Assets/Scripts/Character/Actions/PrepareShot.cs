using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/PrepareShot")]
public class PrepareShot : ScriptableObject {
    [Header("Effects")]
    [SerializeField]
    private Flip flip;
    [SerializeField]
    private float flipDuration;
    [SerializeField]
    private string prepareAttackTrigger;

    [Header("Data")]
    [SerializeField]
    private World world;
    [SerializeField]
    private GameObject dangerIcon;

    public IEnumerator Perform(GameObject performer, Stack<GameObject> activeDangerIcons) {
        IPositionable positionable = performer.GetComponent<IPositionable>();
        IShootable shootable = performer.GetComponent<IShootable>();
        IAnimateable animator = performer.GetComponent<IAnimateable>();

        animator.SetTrigger(this.prepareAttackTrigger);

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
}
