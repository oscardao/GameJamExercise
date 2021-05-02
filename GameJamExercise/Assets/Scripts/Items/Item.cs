using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class Item : ScriptableObject {

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite { get { return this.sprite; } }

}
