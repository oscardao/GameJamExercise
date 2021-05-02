using System.Collections;
using UnityEngine;


public interface IDamageable {

    public abstract bool IsDead {
        get;
    }

    public abstract void OnDamage();
}
