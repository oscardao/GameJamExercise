using OsukaCreative.Utility.GameEvent;
using OsukaCreative.Utility.Variables;
using System.Collections;
using UnityEngine;


public class PlayerDamageable : MonoBehaviour, IDamageable {

    [SerializeField]
    private BoolReference HasArmor;
    public bool IsArmored {
        get { return this.HasArmor.Value; }
    }

    [SerializeField]
    private NoArgEvent onPlayerDeath;
    [SerializeField]
    private BoolReference IsGameOn;


    public void OnDamage() {
        if (this.HasArmor.Value) {
            this.HasArmor.Value = false;
        } else {
            this.IsGameOn.Value = false;
            this.onPlayerDeath.Raise();
        }
    }

    public void AddArmor() {
        this.HasArmor.Value = true;
    }

}
