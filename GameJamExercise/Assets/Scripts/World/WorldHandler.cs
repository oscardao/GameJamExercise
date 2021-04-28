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
    private TileBase tile;
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

        List<Vector2Int> freeTileSpots = new List<Vector2Int>();
        int worldSize = CalculateWorldSize();

        freeTileSpots.Add(Vector2Int.zero);

        while (this.world.TileCount < worldSize) {
            AddTile(freeTileSpots);
        }
        AddObjects();
    }

    public void Clear() {

    }

    private void AddTile(List<Vector2Int> freeTileSpots) {
        Vector2Int spot = freeTileSpots[Random.Range(0, freeTileSpots.Count)];
        freeTileSpots.Remove(spot);
        this.world.AddTile(new WorldTile(spot, this.tilemap.GetCellCenterWorld((Vector3Int)spot)));
        this.tilemap.SetTile((Vector3Int)spot, this.tile);

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

        WorldTile playerSpawn = emptyTiles[Random.Range(0, emptyTiles.Count)];
        emptyTiles.Remove(playerSpawn);
        GameObject player = Instantiate(this.playerPrefab, playerSpawn.WorldPosition, Quaternion.identity);
        player.GetComponent<IPositionable>().Position = playerSpawn.Position;
        playerSpawn.AddObject(player);

        for (int i = 0; i < 2; i++) {
            WorldTile enemySpawn = emptyTiles[Random.Range(0, emptyTiles.Count)];
            emptyTiles.Remove(enemySpawn);
            GameObject enemy = Instantiate(this.enemies[Random.Range(0, this.enemies.Length)], enemySpawn.WorldPosition, Quaternion.identity);
            enemy.GetComponent<IPositionable>().Position = enemySpawn.Position;
            enemySpawn.AddObject(enemy);
        }
    }

    private int CalculateWorldSize() {
        return this.level + this.numberOfTilesOffset;
    }

}
