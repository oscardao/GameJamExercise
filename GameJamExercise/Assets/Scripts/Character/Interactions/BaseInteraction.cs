using System.Collections;
using UnityEngine;


public abstract class BaseInteraction : ScriptableObject {

    [SerializeField]
    private string description;
    public string Description {
        get { return this.description; }
    }

    public abstract bool Evaluate(WorldTile tile, GameObject interacter);

    public abstract IEnumerator Perform(WorldTile tile, GameObject interacter);

}
