using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Interactions/Player Move")]
public class MoveInteraction : BaseInteraction {

    [SerializeField]
    private BaseAction moveAction;

    public override bool Evaluate(WorldTile tile, GameObject interacter) {
        return tile.IsEmpty ? true : false;
    }

    public override IEnumerator Perform(WorldTile tile, GameObject interacter) {
        this.moveAction.Perform(tile, interacter);
        yield return new WaitForSeconds(this.moveAction.Duration);
    }
}
