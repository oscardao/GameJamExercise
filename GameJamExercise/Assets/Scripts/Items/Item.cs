using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class Item : ScriptableObject {

    [SerializeField]
    private string itemName;
    public string ItemName {
        get { return this.itemName; }
    }

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite { get { return this.sprite; } }

}
