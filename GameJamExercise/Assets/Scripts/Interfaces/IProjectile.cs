using System.Collections;
using UnityEngine;


public interface IProjectile {

    public abstract float Speed {
        get;
    }
    public abstract void ActivateProjectile(Vector2 direction);

    public abstract void HitTarget(GameObject shooter, GameObject target, Transform shootingPoint);

    public abstract IEnumerator Activate(Vector2 endPoint);

}
