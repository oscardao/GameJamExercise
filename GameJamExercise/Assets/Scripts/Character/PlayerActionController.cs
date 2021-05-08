using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IPositionable))]
public class PlayerActionController : MonoBehaviour {

    [SerializeField]
    private World world;

    [SerializeField]
    private GameObject highlightPrefab;
    private Stack<GameObject> inactiveHighlights;
    private Stack<GameObject> activeHighlights;
    [SerializeField]
    private GameObject selectorPrefab;
    private GameObject selector;

    private IPositionable position;

    private void Awake() {
        this.selector = Instantiate(this.selectorPrefab, transform);
        this.selector.SetActive(false);
        this.position = GetComponent<IPositionable>();
        this.inactiveHighlights = new Stack<GameObject>();
        this.activeHighlights = new Stack<GameObject>();
    }

    public void HighlightTiles() {
        List<WorldTile> tiles = this.world.GetNeighbourTiles(this.position.WorldTile.Position);

        for (int i = 0; i < tiles.Count; i++) {
            GameObject highlightObject = this.inactiveHighlights.Count > 0 ? this.inactiveHighlights.Pop() : InstantiateHighlight();
            this.activeHighlights.Push(highlightObject);
            TileHighlight highlight = highlightObject.GetComponent<TileHighlight>();
            highlight.HighLight(tiles[i]);
        }
    }

    public void ClearHighlights() {
        this.selector.SetActive(false);
        while (this.activeHighlights.Count > 0) {
            GameObject highlight = this.activeHighlights.Pop();
            highlight.SetActive(false);
            this.inactiveHighlights.Push(highlight);
        }
    }

    public void SetSelector(Vector3 position) {
        this.selector.transform.position = position;
        this.selector.SetActive(true);
    }

    public void DisableSelector() {
        this.selector.SetActive(false);
    }

    private GameObject InstantiateHighlight() {
        GameObject highlight = Instantiate(this.highlightPrefab, Vector3.zero, Quaternion.identity);
        highlight.transform.SetParent(transform);
        highlight.SetActive(false);
        highlight.GetComponent<TileHighlight>().playerActions = this;
        return highlight;
    }

}
