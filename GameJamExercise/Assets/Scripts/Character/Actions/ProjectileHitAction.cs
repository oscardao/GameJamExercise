using System.Collections;
using UnityEngine;


public class ProjectileHitAction : ScriptableObject {

    [SerializeField]
    private Flip flip;
    [SerializeField]
    private OnDamage onDamage;
    [SerializeField]
    private float duration;

    public void Perform(GameObject attacker, GameObject target) {

    }
}
