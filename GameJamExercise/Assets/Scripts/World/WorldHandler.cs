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
    private IntReference level;
    [SerializeField]
    private World world;

    [Header("World Generation")]
    [SerializeField]
    private LevelData[] levelDatas;
    [SerializeField]
    private LevelDataReference chosenLevelData;
    [SerializeField]
    private GameObject tilePrefab;

    [Header("World Objects")]
    [SerializeField]
    private ItemHandler itemHandler;
    [SerializeField]
    private CharacterSpawner characterSpawner;

    private List<WorldTile> enemyTiles;
    private List<WorldTile> playerTiles;

    [Header("World Presentation")]
    [SerializeField]
    private Vector3 tileSpawn;
    [SerializeField]
    private float flyDuration;
    [SerializeField]
    private float timeBetweeenFlys;
    private float yOffset;
    private List<GameObject> tilesSpawned;



    public IEnumerator Generate() {
        this.world.Clear();
        this.itemHandler.ClearItemsInWorld();

        this.playerTiles = new List<WorldTile>();
        this.enemyTiles = new List<WorldTile>();
        this.tilesSpawned = new List<GameObject>();

        for (int i = 0; i < this.levelDatas.Length; i++) {
            if (this.levelDatas[i].AppearsAtLevel <= this.level.Value) {
                this.chosenLevelData.Value = this.levelDatas[i];
            }
        }

        GenerateTiles();
        this.characterSpawner.PlaceCharacters(this.playerTiles, this.enemyTiles);
        this.itemHandler.PlaceItems();
        yield return PresentWorld();
    }

    private void GenerateTiles() {
        Texture2D map = this.chosenLevelData.Value.Maps[Random.Range(0, this.chosenLevelData.Value.Maps.Length)];

        for (int x = 0; x < map.width; x++) {
            for (int y = 0; y < map.height; y++) {
                Color cellColor = map.GetPixel(x, y);

                if (cellColor.Equals(Color.blue)) {
                    continue;
                } else if (cellColor.Equals(Color.green)) {
                    WorldTile worldTile = AddTile(new Vector2Int(x, y));
                    this.playerTiles.Add(worldTile);
                } else if (cellColor.Equals(Color.red)) {
                    WorldTile worldTile = AddTile(new Vector2Int(x, y));
                    this.enemyTiles.Add(worldTile);
                } else {
                    AddTile(new Vector2Int(x, y));
                }

            }
        }

    }

    private WorldTile AddTile(Vector2Int position) {
        GameObject newTile = Instantiate(this.tilePrefab, transform);
        WorldTile worldTile = newTile.GetComponent<WorldTile>();
        worldTile.Position = position;
        Vector3 spawnOffset = new Vector3(this.tileSpawn.x, this.tileSpawn.y - this.yOffset);
        this.tilesSpawned.Add(newTile);
        newTile.transform.position = this.tilemap.GetCellCenterWorld((Vector3Int)position) + spawnOffset;
        this.world.AddTile(worldTile);
        return worldTile;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.tileSpawn, 0.2f);
    }

    public IEnumerator ClearWorld() {
        for (int i = 0; i < this.tilesSpawned.Count; i++) {
            GameObject tile = this.tilesSpawned[i];
            Vector3 spawnOffset = new Vector3(this.tileSpawn.x, this.tileSpawn.y - this.yOffset);
            this.StartCoroutine(FlyTile(tile, tile.transform.position - spawnOffset, true));
            yield return new WaitForSeconds(this.timeBetweeenFlys);
        }
    }

    private IEnumerator PresentWorld() {
        for (int i = 0; i < this.tilesSpawned.Count; i++) {
            GameObject tile = this.tilesSpawned[i];

            yield return new WaitForSeconds(this.timeBetweeenFlys);

            WorldTile worldTile = tile.GetComponent<WorldTile>();
            Vector3 endValue = this.tilemap.GetCellCenterWorld((Vector3Int)worldTile.Position);

            this.StartCoroutine(FlyTile(tile, endValue, false));

        }

        yield return new WaitForSeconds(this.flyDuration);
    }

    [SerializeField]
    private AudioClip[] spawnSounds;

    [SerializeField]
    private AudioClip[] destroySounds;

    private IEnumerator FlyTile(GameObject tile, Vector3 endValue, bool destroy) {

        if (destroy) {
            AudioManager.Instance.PlayAudio(this.destroySounds[Random.Range(0, this.destroySounds.Length)]);
        }

        Transform transform = tile.transform;
        Vector3 startValue = transform.position;

        float time = 0;

        while (time < this.flyDuration) {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startValue, endValue, time / this.flyDuration);
            time += Time.deltaTime;
            yield return null;

        }

        transform.position = endValue;
        if (destroy) {
            Destroy(tile);
        } else {
            AudioManager.Instance.PlayAudio(this.spawnSounds[Random.Range(0, this.spawnSounds.Length)]);
        }
    }



}
