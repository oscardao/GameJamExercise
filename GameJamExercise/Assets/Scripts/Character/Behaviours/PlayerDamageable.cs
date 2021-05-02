using OsukaCreative.Utility.GameEvent;
using OsukaCreative.Utility.Variables;
using System.Collections;
using UnityEngine;


public class PlayerDamageable : MonoBehaviour, IDamageable {

    private IInventory playerInventory;

    [SerializeField]
    private Item armorItem;
    public bool IsArmored {
        get { return this.playerInventory.HasItem(this.armorItem); }
    }

    private bool isDead;
    public bool IsDead {
        get { return this.isDead; }
    }

    [SerializeField]
    private NoArgEvent onPlayerDeath;
    [SerializeField]
    private BoolReference IsGameOn;

    private void Awake() {
        this.playerInventory = GetComponent<IInventory>();
        this.isDead = false;
    }

    public void OnDamage() {
        if (this.IsArmored) {
            this.playerInventory.RemoveItem(this.armorItem);
        } else {
            this.isDead = true;
            this.IsGameOn.Value = false;
            this.onPlayerDeath.Raise();
        }
    }

}
