using OsukaCreative.Utility.Sets;
using OsukaCreative.Utility.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSpawner : MonoBehaviour {

    [Header("Properties")]
    [SerializeField]
    private LevelDataReference chosenLevelData;

    [Header("Player")]
    [SerializeField]
    private GameObjectVariable playerVariable;
    [SerializeField]
    private GameObject playerPrefab;

    [Header("Enemies")]
    [SerializeField]
    private GameObjectSet enemiesInWorld;

    public void PlaceCharacters(List<WorldTile> playerTiles, List<WorldTile> enemyTiles) {
        this.enemiesInWorld.Clear();

        WorldTile playerTile = playerTiles[Random.Range(0, playerTiles.Count)];
        GameObject player = Instantiate(this.playerPrefab, playerTile.WorldPosition, Quaternion.identity);
        playerTile.ObjectOnTile = player;
        this.playerVariable.Value = player;

        GameObject[] enemies = this.chosenLevelData.Value.Enemies;

        for (int i = 0; i < this.chosenLevelData.Value.NumberOfEnemies; i++) {
            WorldTile enemyTile = enemyTiles[Random.Range(0, enemyTiles.Count)];
            enemyTiles.Remove(enemyTile);
            GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], enemyTile.WorldPosition, Quaternion.identity);
            enemyTile.ObjectOnTile = enemy;
            this.enemiesInWorld.Add(enemy);
        }
    }

}
