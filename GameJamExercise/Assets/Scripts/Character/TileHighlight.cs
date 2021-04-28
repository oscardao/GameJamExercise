using System.Collections;
using UnityEngine;

public class TileHighlight : MonoBehaviour {

    public PlayerActionController playerActions;

    public void HighLight(WorldTile tile) {
        transform.position = tile.WorldPosition;
        gameObject.SetActive(true);
    }

    private void OnMouseEnter() {
        this.playerActions.SetSelector(transform.position);
    }

    private void OnMouseDown() {
        Debug.Log("sdas");
    }

    private void OnMouseExit() {
        this.playerActions.DisableSelector();
    }

}
