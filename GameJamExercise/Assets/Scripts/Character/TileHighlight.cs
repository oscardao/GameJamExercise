using System.Collections;
using UnityEngine;

public class TileHighlight : MonoBehaviour {

    public void HighLight(WorldTile tile) {
        transform.position = tile.WorldPosition;
        gameObject.SetActive(true);
    }

}
