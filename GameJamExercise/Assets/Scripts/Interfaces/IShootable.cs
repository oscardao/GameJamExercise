using System.Collections;
using UnityEngine;


public interface IShootable {

    public abstract Transform ProjectileSpawn {
        get;
    }

    public abstract Vector2Int ShootDirection {
        get;
        set;
    }

}
