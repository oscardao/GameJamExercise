using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Interactions/Enemy Switch Attack")]
public class EnemySwitchAttack : BaseInteraction {

    [SerializeField]
    private BaseAction switchAction;

    public override bool Evaluate(WorldTile tile, GameObject interacter) {
        IDamageable damageable = interacter.GetComponent<IDamageable>();

        return damageable.IsArmored;
    }

    public override IEnumerator Perform(WorldTile tile, GameObject interacter) {
        IAnimateable attackerAnimateable = tile.ObjectOnTile.GetComponent<IAnimateable>();

        IDamageable targetDamageable = interacter.GetComponent<IDamageable>();

        attackerAnimateable.SetTrigger("onPrepareAttack");
        yield return new WaitForSeconds(0.15f);
        attackerAnimateable.SetTrigger("onAttack");
        targetDamageable.OnDamage();
        this.switchAction.Perform(tile, interacter);
        yield return new WaitForSeconds(this.switchAction.Duration);
        attackerAnimateable.SetTrigger("onIdle");
    }

}
