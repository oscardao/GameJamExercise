using System.Collections;
using UnityEngine;


public class ItemBehaviour : MonoBehaviour, IInteractable {

    [SerializeField]
    private Item itemData;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private BaseInteraction interaction;

    private void Awake() {
        this.spriteRenderer.sprite = this.itemData.Sprite;
    }

    public BaseInteraction GetInteraction(WorldTile tile, GameObject interacter) {
        return this.interaction;
    }
}
