using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Effects/Attack")]
public class Attack : ScriptableObject {

    [Header("Animations")]
    [SerializeField]
    private string onAttackTrigger = "onAttack";

    [SerializeField]
    private string onIdleTrigger = "onIdle";

    public IEnumerator AttackTarget(GameObject target, float duration, GameObject attacker) {
        IAnimateable attackerAnimator = attacker.GetComponent<IAnimateable>();
        attackerAnimator.SetTrigger(this.onAttackTrigger);

        IDamageable targetDamageable = target.GetComponent<IDamageable>();
        targetDamageable.OnDamage();

        if (targetDamageable.IsDead) {
            IPositionable targetPositionable = target.GetComponent<IPositionable>();
            targetPositionable.WorldTile.ObjectOnTile = null;
        }

        yield return new WaitForSeconds(duration);

        attackerAnimator.SetTrigger(this.onIdleTrigger);

        if (targetDamageable.IsDead) {
            target.SetActive(false);
        }

    }
}
