using System.Collections;
using UnityEngine;


public interface IDamageable {

    public abstract bool IsArmored {
        get;
    }

    public abstract void AddArmor();

    public abstract void OnDamage();
}
