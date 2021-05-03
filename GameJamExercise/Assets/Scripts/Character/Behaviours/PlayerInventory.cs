using OsukaCreative.Utility.Variables;
using System.Collections;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInventory {
    [SerializeField]
    private ItemVariable item;
    public Item Item {
        get { return this.item.Value; }
    }

    [SerializeField]
    private Item clock;
    [SerializeField]
    private IntVariable extraTurns;

    private void Awake() {
        this.item.Value = null;
        this.extraTurns.Value = 0;
    }

    public bool IsEmpty {
        get { return this.item.Value == null; }
    }

    public void AddItem(Item item) {
        if (item == this.clock) {
            this.extraTurns.Value++;
        } else {
            this.item.Value = item;
        }

    }

    public bool HasItem(Item item) {
        return this.item.Value == item;
    }

    public void RemoveItem(Item item) {
        if (this.item.Value == item) {
            this.item.Value = null;
        }
    }
}
