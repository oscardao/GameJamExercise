using System.Collections;
using UnityEngine;


public interface IProjectile {

    public abstract float Speed {
        get;
    }

    public abstract void Activate(Vector2 direction);

}
