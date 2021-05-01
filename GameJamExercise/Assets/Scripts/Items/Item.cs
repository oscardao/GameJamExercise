using System.Collections;
using UnityEngine;


public class Item : ScriptableObject {

    [SerializeField]
    private Sprite image;
    public Sprite Image { get { return this.image; } }

}
