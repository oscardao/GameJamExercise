using OsukaCreative.Utility.GameEvent;
using OsukaCreative.Utility.Sets;
using System.Collections;
using UnityEngine;


public class EnemyDamageable : MonoBehaviour, IDamageable {

    public bool IsArmored;

    private ICommandable commandable;

    [SerializeField]
    private GameObjectEvent onDeath;

    private bool isDead;
    public bool IsDead {
        get { return this.isDead; }
    }

    private void Awake() {
        this.commandable = GetComponent<ICommandable>();
        this.isDead = false;
    }

    public void OnDamage() {
        if (this.IsArmored) {
            this.IsArmored = false;
        } else {

            this.isDead = true;
            this.commandable.IsActive = false;
            this.onDeath.Raise(gameObject);
        }
    }

}
