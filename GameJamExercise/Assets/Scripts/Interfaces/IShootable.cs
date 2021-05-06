using System.Collections;
using UnityEngine;


public interface IShootable {
    public abstract Vector2Int ShootDirection {
        get;
        set;
    }

}
