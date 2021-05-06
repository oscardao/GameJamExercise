using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterPositionable : MonoBehaviour, IPositionable {

    private WorldTile position;
    public WorldTile WorldTile {
        get { return this.position; }
        set {
            this.position = value;
            this.onReposition.Invoke();
        }
    }

    public UnityEvent onReposition;
    public UnityEvent OnReposition {
        get { return this.onReposition; }
    }



}
