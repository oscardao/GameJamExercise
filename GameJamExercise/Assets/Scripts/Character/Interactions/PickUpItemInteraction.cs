using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Interactions/Pickup item")]
public class PickUpItemInteraction : BaseInteraction {

    [SerializeField]
    private BaseAction moveAction;

    public override bool Evaluate(WorldTile tile, GameObject interacter) {
        return true;
    }

    public override IEnumerator Perform(WorldTile tile, GameObject interacter) {
        IPickupable pickupAble = tile.ObjectOnTile.GetComponent<IPickupable>();
        this.moveAction.Perform(tile, interacter);

        yield return new WaitForSeconds(this.moveAction.Duration);

        pickupAble.OnPickup(interacter);
    }

}
