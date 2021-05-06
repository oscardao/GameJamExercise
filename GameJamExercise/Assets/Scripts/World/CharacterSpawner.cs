using OsukaCreative.Utility.Sets;
using OsukaCreative.Utility.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterSpawner : MonoBehaviour {

    [Header("Properties")]
    [SerializeField]
    private IntReference Level;
    [SerializeField, Min(0)]
    private int numberOfEnemiesOffset;

    [Header("Player")]
    [SerializeField]
    private GameObjectVariable playerVariable;
    [SerializeField]
    private GameObject playerPrefab;

    [Header("Enemies")]
    [SerializeField]
    private GameObjectSet enemiesInWorld;
    [SerializeField]
    private GameObject[] enemies;

    public void PlaceCharacters(List<WorldTile> playerTiles, List<WorldTile> enemyTiles) {
        this.enemiesInWorld.Clear();

        WorldTile playerTile = playerTiles[Random.Range(0, playerTiles.Count)];
        GameObject player = Instantiate(this.playerPrefab, playerTile.WorldPosition, Quaternion.identity);
        playerTile.ObjectOnTile = player;
        this.playerVariable.Value = player;

        for (int i = 0; i < this.Level.Value + this.numberOfEnemiesOffset; i++) {
            WorldTile enemyTile = enemyTiles[Random.Range(0, enemyTiles.Count)];
            enemyTiles.Remove(enemyTile);
            GameObject enemy = Instantiate(this.enemies[Random.Range(0, this.enemies.Length)], enemyTile.WorldPosition, Quaternion.identity);
            enemyTile.ObjectOnTile = enemy;
            this.enemiesInWorld.Add(enemy);
        }
    }

}
