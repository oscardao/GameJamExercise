using System.Collections;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInventory {

    [SerializeField]
    private Item item;
    public Item Item {
        get { return this.item; }
    }

    public bool IsEmpty {
        get { return this.item == null; }
    }

    public void AddItem(Item item) {
        this.item = item;
    }

    public bool HasItem(Item item) {
        return this.item == item;
    }

    public void RemoveItem(Item item) {
        if (this.item == item) {
            this.item = null;
        }
    }
}
