using OsukaCreative.Utility.GameEvent;
using System.Collections;
using UnityEngine;


public class ItemBehaviour : MonoBehaviour, IInteractable, IPickupable {

    [SerializeField]
    private ItemSet itemsInWorld;

    private Item itemData;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private BaseInteraction interaction;

    public string promptDescription {
        get { return this.itemData.ItemName; }
    }

    [Header("Stats")]
    [SerializeField]
    private NoArgEvent onItemPickedUp;

    public void Setup(Item item) {
        itemsInWorld.Add(item);
        this.itemData = item;
        this.spriteRenderer.sprite = this.itemData.Sprite;
    }

    public BaseInteraction GetInteraction(WorldTile tile, GameObject interacter) {
        return this.interaction;
    }

    public void OnPickup(GameObject interacter) {
        this.onItemPickedUp.Raise();
        IInventory inventory = interacter.GetComponent<IInventory>();
        inventory.AddItem(this.itemData);
        this.itemsInWorld.Remove(this.itemData);
        Destroy(gameObject);
    }
}
