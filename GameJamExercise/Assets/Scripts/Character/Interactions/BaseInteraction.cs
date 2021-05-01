using System.Collections;
using UnityEngine;


public abstract class BaseInteraction : ScriptableObject {

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite {
        get { return this.sprite; }
    }

    public abstract bool Evaluate(WorldTile tile, GameObject interacter);

    public abstract IEnumerator Perform(WorldTile tile, GameObject interacter);

}
