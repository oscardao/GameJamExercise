using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Brain")]
public class AIBrain : ScriptableObject {

    [SerializeField]
    private World world;

    [Header("Actions")]
    [SerializeField]
    private BaseAction moveAction;
    [SerializeField]
    private BaseAction switchAction;
    [SerializeField]
    private BaseAction attackAction;

    [Header("Path Finding")]
    [SerializeField]
    private PathFinding pathFinding;

    public IEnumerator OnTakeTurn(GameObject ai) {
        IPositionable positionable = ai.GetComponent<IPositionable>();
        ITargeting targeting = ai.GetComponent<ITargeting>();

        List<WorldTile> tiles = this.world.GetNeighbourTiles(positionable.WorldTile);

        for (int i = 0; i < tiles.Count; i++) {
            if (tiles[i].ObjectOnTile == targeting.Target) {
                yield return PerformAction(this.attackAction, tiles[i], ai);
                yield break;
            }
        }

        Stack<WorldTile> pathToTarget = new Stack<WorldTile>();
        IPositionable targetPositionable = targeting.Target.GetComponent<IPositionable>();
        yield return this.pathFinding.FindPath(targetPositionable.WorldTile, positionable.WorldTile, pathToTarget);

        while (pathToTarget.Count > 0) {
            WorldTile tile = pathToTarget.Pop();
            Debug.Log("IsOnTile: " + positionable.WorldTile.Position + "Checking Tile: " + tile.ToString());
            if (tile == positionable.WorldTile) {
                continue;
            } else if (!tile.IsEmpty) {
                Debug.Log("Switching");
                break;

            } else {
                Debug.Log("moving");
                yield return PerformAction(this.moveAction, tile, ai);
                break;
            }
        }
    }

    private IEnumerator PerformAction(BaseAction action, WorldTile tile, GameObject gameObject) {
        action.Perform(tile, gameObject);
        yield return new WaitForSeconds(action.Duration);
    }

}
