using System.Collections;
using UnityEngine;


public interface IProjectile {

    public abstract float Speed {
        get;
    }

    public abstract IEnumerator HitTarget(GameObject shooter, GameObject target);

    public abstract IEnumerator Activate(Vector2 endPoint);

}
