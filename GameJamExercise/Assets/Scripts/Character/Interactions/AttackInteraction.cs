using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Interactions/Player Attack")]
public class AttackInteraction : BaseInteraction {

    [SerializeField]
    private BaseAction attackAction;
    [SerializeField]
    private Item swordToCheck;

    public override bool Evaluate(WorldTile tile, GameObject interacter) {
        IInventory inventory = interacter.GetComponent<IInventory>();

        return inventory.HasItem(this.swordToCheck);
    }

    public override IEnumerator Perform(WorldTile tile, GameObject interacter) {
        this.attackAction.Perform(tile, interacter);
        yield return new WaitForSeconds(this.attackAction.Duration);
    }

}
