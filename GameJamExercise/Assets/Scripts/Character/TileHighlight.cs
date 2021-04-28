using OsukaCreative.Utility.GameEvent;
using System.Collections;
using UnityEngine;

public class TileHighlight : MonoBehaviour {

    public PlayerActionController playerActions;
    [SerializeField]
    private WorldTileEvent onClick;
    private WorldTile tile;

    public void HighLight(WorldTile tile) {
        this.tile = tile;
        transform.position = tile.WorldPosition;
        gameObject.SetActive(true);
    }

    private void OnMouseEnter() {
        this.playerActions.SetSelector(transform.position);
    }

    private void OnMouseDown() {
        this.playerActions.ClearHighlights();
        this.onClick.Raise(this.tile);
    }

    private void OnMouseExit() {
        this.playerActions.DisableSelector();
    }

}
