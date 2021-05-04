using OsukaCreative.Utility.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldHandler : MonoBehaviour {

    [Header("World Properties")]
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField, Min(1)]
    private int patchSize;
    [SerializeField, Min(1)]
    private int numberOfPatchesOffset;
    [SerializeField]
    private IntReference level;
    [SerializeField]
    private World world;

    [Header("World Objects")]
    [SerializeField]
    private ItemHandler itemHandler;
    [SerializeField]
    private CharacterSpawner characterSpawner;

    private List<WorldTile> enemyTiles;
    private List<WorldTile> playerTiles;

    [SerializeField]
    private Vector3 tileSpawn;
    int topBound;

    public void Generate() {
        this.world.Clear();
        this.itemHandler.ClearItemsInWorld();

        this.playerTiles = new List<WorldTile>();
        this.enemyTiles = new List<WorldTile>();

        GenerateTiles(this.level.Value + this.numberOfPatchesOffset);

        this.characterSpawner.PlaceCharacters(this.playerTiles, this.enemyTiles);
        this.itemHandler.PlaceItems();
    }

    private void GenerateTiles(int worldSize) {
        List<Vector2Int> freePatchSpots = new List<Vector2Int>();
        Queue<Vector2Int> usedPatchSpots = new Queue<Vector2Int>();

        this.topBound = 1;

        freePatchSpots.Add(Vector2Int.zero);

        for (int i = 0; i < worldSize; i++) {
            Vector2Int patch = freePatchSpots[Random.Range(0, freePatchSpots.Count)];
            usedPatchSpots.Enqueue(patch);
            freePatchSpots.Remove(patch);
            Debug.Log(patch);
            if (this.topBound < patch.y) {
                Debug.Log(patch.y);
                this.topBound = patch.y;
            }

            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {
                    if (Mathf.Abs(x) == Mathf.Abs(y)) continue;
                    Vector2Int potentialFreeSpot = new Vector2Int(x, y) + patch;
                    if (!usedPatchSpots.Contains(potentialFreeSpot) && !freePatchSpots.Contains(potentialFreeSpot)) {
                        freePatchSpots.Add(potentialFreeSpot);
                    }
                }
            }
        }
        Debug.Log(this.topBound);
        AddTile(usedPatchSpots.Dequeue(), this.playerTiles);

        while (usedPatchSpots.Count > 0) {
            AddTile(usedPatchSpots.Dequeue(), this.enemyTiles);
        }

    }

    private void AddTile(Vector2Int patch, List<WorldTile> tileSpawns) {
        for (int x = 0; x < this.patchSize; x++) {
            for (int y = 0; y < this.patchSize; y++) {
                int tileX = x + this.patchSize * patch.x;
                int tileY = y + this.patchSize * patch.y;
                Vector2Int tilePosition = new Vector2Int(tileX, tileY);

                GameObject newTile = Instantiate(this.tilePrefab, transform);
                WorldTile worldTile = newTile.GetComponent<WorldTile>();
                worldTile.Position = tilePosition;

                Vector3 spawnOffset = new Vector3(this.tileSpawn.x, this.tileSpawn.y - this.topBound * this.patchSize);

                newTile.transform.position = this.tilemap.GetCellCenterWorld((Vector3Int)tilePosition) + spawnOffset;
                tileSpawns.Add(worldTile);
                this.world.AddTile(worldTile);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.tileSpawn, 0.2f);
    }
}
