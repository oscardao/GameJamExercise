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

        yield return new WaitForSeconds(duration);

        attackerAnimator.SetTrigger(this.onIdleTrigger);
    }
}
