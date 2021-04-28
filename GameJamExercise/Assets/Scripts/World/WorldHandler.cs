using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldHandler : MonoBehaviour {

    [SerializeField]
    private Texture[] startRooms;
    [SerializeField]
    private Texture[] defaultRooms;

    [Header("World Properties")]
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField, Min(2)]
    private int numberOfTilesOffset;
    [SerializeField]
    private int level;
    [SerializeField]
    private World world;

    [Header("World Objects")]
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject[] enemies;

    public void Generate() {
        this.world.Clear();

        int worldSize = CalculateWorldSize();
        GenerateTiles(worldSize);

        AddObjects();
    }

    public void Clear() {

    }

    private void GenerateTiles(int worldSize) {
        List<Vector2Int> freeTileSpots = new List<Vector2Int>();
        freeTileSpots.Add(Vector2Int.zero);
        while (this.world.TileCount < worldSize) {
            AddTile(freeTileSpots);
        }
    }

    private void AddTile(List<Vector2Int> freeTileSpots) {
        Vector2Int spot = freeTileSpots[Random.Range(0, freeTileSpots.Count)];
        freeTileSpots.Remove(spot);

        GameObject newTile = Instantiate(this.tilePrefab, transform);
        WorldTile worldTile = newTile.GetComponent<WorldTile>();
        worldTile.Position = spot;
        newTile.transform.position = this.tilemap.GetCellCenterWorld((Vector3Int)spot);

        this.world.AddTile(worldTile);

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (Mathf.Abs(x) == Mathf.Abs(y)) continue;
                Vector2Int potentialFreeSpot = new Vector2Int(x, y) + spot;
                if (!this.world.ContainsTile(potentialFreeSpot) && !freeTileSpots.Contains(potentialFreeSpot)) {
                    freeTileSpots.Add(potentialFreeSpot);
                }
            }
        }
    }

    private void AddObjects() {
        List<WorldTile> emptyTiles = this.world.EmptyTiles;

        WorldTile playerTile = emptyTiles[Random.Range(0, emptyTiles.Count)];
        emptyTiles.Remove(playerTile);
        GameObject player = Instantiate(this.playerPrefab, playerTile.WorldPosition, Quaternion.identity);
        playerTile.ObjectOnTile = player;

        for (int i = 0; i < 2; i++) {
            WorldTile enemyTile = emptyTiles[Random.Range(0, emptyTiles.Count)];
            emptyTiles.Remove(enemyTile);
            GameObject enemy = Instantiate(this.enemies[Random.Range(0, this.enemies.Length)], enemyTile.WorldPosition, Quaternion.identity);
            enemyTile.ObjectOnTile = enemy;
        }
    }

    private int CalculateWorldSize() {
        return this.level + this.numberOfTilesOffset;
    }

}
