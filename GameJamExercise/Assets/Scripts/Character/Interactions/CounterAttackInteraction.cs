using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Interactions/Enemy Counter Attack")]
public class CounterAttackInteraction : BaseInteraction {
    [SerializeField]
    private BaseAction attackAction;

    public override bool Evaluate(WorldTile tile, GameObject interacter) {
        IDamageable damageable = interacter.GetComponent<IDamageable>();

        return damageable.IsArmored ? false : true;
    }

    public override IEnumerator Perform(WorldTile tile, GameObject interacter) {
        IPositionable interacterPositionable = interacter.GetComponent<IPositionable>();
        this.attackAction.Perform(interacterPositionable.WorldTile, tile.ObjectOnTile);
        yield return new WaitForSeconds(this.attackAction.Duration);
    }
}
