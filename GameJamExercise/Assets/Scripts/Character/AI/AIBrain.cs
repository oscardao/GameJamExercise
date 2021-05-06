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

        WorldTile tile = tiles[Random.Range(0, tiles.Count)];
        if (!tile.IsEmpty) {
            yield return PerformAction(this.switchAction, tile, ai);

        } else {
            yield return PerformAction(this.moveAction, tile, ai);
        }
    }

    private IEnumerator PerformAction(BaseAction action, WorldTile tile, GameObject gameObject) {
        action.Perform(tile, gameObject);
        yield return new WaitForSeconds(action.Duration);
    }

}
