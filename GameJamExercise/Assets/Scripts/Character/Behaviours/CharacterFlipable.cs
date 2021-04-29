using System.Collections;
using UnityEngine;

public class CharacterFlipable : MonoBehaviour, IFlipable {
    [SerializeField, Tooltip("True=facing left, False=facing right")]
    private bool valueOnAwake;

    private bool isFlipped;
    public bool IsFlipped {
        get { return this.isFlipped; }
        set { this.isFlipped = value; }
    }

    private void Awake() {
        this.isFlipped = this.valueOnAwake;
    }

}
