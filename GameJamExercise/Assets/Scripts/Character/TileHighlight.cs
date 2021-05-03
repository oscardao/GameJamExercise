using OsukaCreative.Utility.GameEvent;
using OsukaCreative.Utility.Variables;
using System.Collections;
using UnityEngine;

public class TileHighlight : MonoBehaviour {

    public PlayerActionController playerActions;

    private WorldTile tile;

    [SerializeField]
    private GameObjectReference player;
    private Player playerMono;

    [Header("Interaction")]
    [SerializeField]
    private BaseInteraction defaultInteraction;
    private BaseInteraction interaction;
    [SerializeField]
    private StringEvent onPreviewInteraction;
    [SerializeField]
    private NoArgEvent onExitPreview;

    private void Awake() {
        this.playerMono = this.player.Value.GetComponent<Player>();
    }

    public void HighLight(WorldTile tile) {
        this.tile = tile;
        transform.position = tile.WorldPosition;

        if (tile.IsEmpty) {
            this.interaction = defaultInteraction;

        } else {
            IInteractable interactable = tile.ObjectOnTile.GetComponent<IInteractable>();
            this.interaction = interactable.GetInteraction(tile, this.player.Value);

        }
        gameObject.SetActive(true);
    }

    private void OnMouseEnter() {
        this.playerActions.SetSelector(transform.position);
        this.onPreviewInteraction.Raise(this.interaction.Description);
    }

    private void OnMouseDown() {
        this.playerActions.ClearHighlights();
        this.playerMono.PerformAction(this.interaction, this.tile);
        this.onExitPreview.Raise();
    }

    private void OnMouseExit() {
        this.playerActions.DisableSelector();
        this.onExitPreview.Raise();
    }

}
