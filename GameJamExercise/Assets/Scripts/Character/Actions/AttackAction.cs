using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Attack")]
public class AttackAction : BaseAction {

    public override float Duration { get { return this.duration + this.PreAttackDuration; } }

    [Header("Attack Properties")]
    [SerializeField]
    private float PreAttackDuration;
    [Header("Animations")]
    [SerializeField]
    private string onPreAttackTrigger = "onPrepareAttack";

    [Header("Effects")]
    [SerializeField]
    private Attack attack;
    [SerializeField]
    private OnDamage damage;
    [SerializeField]
    private Flip flip;
    [SerializeField]
    private MoveTowards moveTowards;

    public override void Perform(WorldTile tile, GameObject attacker) {
        GameObject target = tile.ObjectOnTile;

        tile.StartCoroutine(this.flip.FlipObject(target.transform.position, 0, attacker));
        tile.StartCoroutine(this.flip.FlipObject(attacker.transform.position, 0, target));

        tile.StartCoroutine(AttackCO(tile, attacker));
    }

    private IEnumerator AttackCO(WorldTile tile, GameObject attacker) {
        IAnimateable attackerAnimator = attacker.GetComponent<IAnimateable>();
        attackerAnimator.SetTrigger(this.onPreAttackTrigger);

        yield return new WaitForSeconds(this.PreAttackDuration);
        PerformEffects(tile, attacker);
    }

    private void PerformEffects(WorldTile tile, GameObject attacker) {

        GameObject target = tile.ObjectOnTile;

        tile.StartCoroutine(this.attack.AttackTarget(target, this.duration, attacker));

        tile.StartCoroutine(this.damage.DamageObject(target, this.duration));

        Vector3 knockbackDirection = target.transform.position - attacker.transform.position;
        tile.StartCoroutine(this.moveTowards.MoveObjectTowards(target, knockbackDirection, this.duration));
    }

}
