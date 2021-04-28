using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "World/World")]
public class World : ScriptableObject {

    private Dictionary<Vector2Int, WorldTile> tiles = new Dictionary<Vector2Int, WorldTile>();

    public List<WorldTile> EmptyTiles {
        get {
            List<WorldTile> tiles = new List<WorldTile>();
            foreach (KeyValuePair<Vector2Int, WorldTile> entry in this.tiles) {
                if (entry.Value.Objects.Count <= 0) tiles.Add(entry.Value);
            }
            return tiles;
        }
    }

    public int TileCount {
        get { return this.tiles.Count; }
    }

    public List<WorldTile> GetNeighbourTiles(Vector2Int position) {
        List<WorldTile> tiles = new List<WorldTile>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (Mathf.Abs(x) == Mathf.Abs(y)) continue;
                Vector2Int spot = position + new Vector2Int(x, y);

                if (this.tiles.ContainsKey(spot)) {
                    tiles.Add(this.tiles[spot]);
                }
            }
        }

        return tiles;
    }

    public void AddTile(WorldTile tile) {
        this.tiles.Add(tile.Position, tile);
    }

    public bool ContainsTile(Vector2Int position) {
        return this.tiles.ContainsKey(position);
    }

    public void Clear() {
        this.tiles = new Dictionary<Vector2Int, WorldTile>();
    }

}
