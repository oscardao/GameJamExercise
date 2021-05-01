using System.Collections;
using UnityEngine;


public abstract class BaseAction : ScriptableObject {

    [SerializeField]
    protected float duration;
    public virtual float Duration { get { return this.duration; } }

    public abstract void Perform(WorldTile tile, GameObject gameObject);

}
