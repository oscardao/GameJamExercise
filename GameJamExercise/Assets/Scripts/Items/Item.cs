using System.Collections;
using UnityEngine;


public class Item : ScriptableObject {

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite { get { return this.sprite; } }

}
