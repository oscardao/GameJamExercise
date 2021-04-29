using OsukaCreative.Utility.Variables;
using System.Collections;
using UnityEngine;


public class CharacterDamageable : MonoBehaviour, IDamageable {

    [SerializeField]
    private BoolReference HasArmor;

    public void OnDamage() {
        if (this.HasArmor.Value) {
            this.HasArmor.Value = false;
        } else {
            //Die
        }
    }

}
