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

    public void Generate() {
        this.world.Clear();
        this.itemHandler.ClearItemsInWorld();

        this.playerTiles = new List<WorldTile>();
        this.enemyTiles = new List<WorldTile>();

        GenerateTiles(this.level.Value + this.numberOfPatchesOffset);

        this.characterSpawner.PlaceCharacters(this.playerTiles, this.enemyTiles);
        this.itemHandler.PlaceItems();
    }

    public void Clear() {

    }

    private void GenerateTiles(int worldSize) {
        List<Vector2Int> freePatchSpots = new List<Vector2Int>();
        List<Vector2Int> usedPatchSpots = new List<Vector2Int>();

        freePatchSpots.Add(Vector2Int.zero);
        AddPatch(freePatchSpots, usedPatchSpots, this.playerTiles);

        for (int i = 0; i < worldSize - 1; i++) {
            AddPatch(freePatchSpots, usedPatchSpots, this.enemyTiles);
        }

    }

    private void AddPatch(List<Vector2Int> freePatches, List<Vector2Int> usedPatchSpots, List<WorldTile> tileSpawns) {
        Vector2Int patch = freePatches[Random.Range(0, freePatches.Count)];
        usedPatchSpots.Add(patch);
        freePatches.Remove(patch);

        for (int x = 0; x < this.patchSize; x++) {
            for (int y = 0; y < this.patchSize; y++) {
                int tileX = x + this.patchSize * patch.x;
                int tileY = y + this.patchSize * patch.y;
                Vector2Int tilePosition = new Vector2Int(tileX, tileY);

                GameObject newTile = Instantiate(this.tilePrefab, transform);
                WorldTile worldTile = newTile.GetComponent<WorldTile>();
                worldTile.Position = tilePosition;
                newTile.transform.position = this.tilemap.GetCellCenterWorld((Vector3Int)tilePosition);
                tileSpawns.Add(worldTile);
                this.world.AddTile(worldTile);
            }
        }

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (Mathf.Abs(x) == Mathf.Abs(y)) continue;
                Vector2Int potentialFreeSpot = new Vector2Int(x, y) + patch;
                if (!usedPatchSpots.Contains(potentialFreeSpot)) {
                    freePatches.Add(potentialFreeSpot);
                }
            }
        }
    }


}
