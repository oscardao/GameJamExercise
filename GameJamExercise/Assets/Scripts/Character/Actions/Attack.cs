using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Attack {

    [Header("Animations")]
    [SerializeField]
    private string onDamageTrigger = "onDamage";
    [SerializeField]
    private string onAttackTrigger = "onAttack";
    [SerializeField]
    private string onIdleTrigger = "onIdle";

    public IEnumerator AttackTarget(GameObject target, float duration, GameObject attacker) {
        IAnimateable targetAnimator = target.GetComponent<IAnimateable>();
        IAnimateable attackerAnimator = attacker.GetComponent<IAnimateable>();

        targetAnimator.SetTrigger(this.onDamageTrigger);
        attackerAnimator.SetTrigger(this.onAttackTrigger);

        IDamageable targetDamageable = target.GetComponent<IDamageable>();
        targetDamageable.OnDamage();

        yield return new WaitForSeconds(duration);

        targetAnimator.SetTrigger(this.onIdleTrigger);
        attackerAnimator.SetTrigger(this.onIdleTrigger);

    }
}
