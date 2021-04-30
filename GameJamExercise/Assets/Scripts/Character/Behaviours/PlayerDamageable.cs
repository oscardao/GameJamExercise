using OsukaCreative.Utility.GameEvent;
using OsukaCreative.Utility.Variables;
using System.Collections;
using UnityEngine;


public class PlayerDamageable : MonoBehaviour, IDamageable {

    [SerializeField]
    private BoolReference HasArmor;

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

}
