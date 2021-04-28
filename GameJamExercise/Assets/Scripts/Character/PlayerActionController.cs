using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerActionController : MonoBehaviour {

    [SerializeField]
    private World world;

    [SerializeField]
    private GameObject tileHighlight;

    private Stack<GameObject> inactiveHighlights;
    private Stack<GameObject> activeHighlights;

    private IPositionable position;

    private void Awake() {
        this.position = GetComponent<IPositionable>();
        this.inactiveHighlights = new Stack<GameObject>();
        this.activeHighlights = new Stack<GameObject>();
    }

    public void HighlightTiles() {
        List<WorldTile> tiles = this.world.GetNeighbourTiles(this.position.Position);

        for (int i = 0; i < tiles.Count; i++) {
            GameObject highlight = this.inactiveHighlights.Count > 0 ? this.inactiveHighlights.Pop() : Instantiate(this.tileHighlight, transform);
            this.activeHighlights.Push(highlight);
            highlight.transform.position = tiles[i].WorldPosition;
            highlight.SetActive(true);
        }
    }

    public void ClearHighlights() {
        while (this.activeHighlights.Count > 0) {
            GameObject highlight = this.activeHighlights.Pop();
            highlight.SetActive(false);
            this.inactiveHighlights.Push(highlight);
        }
    }

}
