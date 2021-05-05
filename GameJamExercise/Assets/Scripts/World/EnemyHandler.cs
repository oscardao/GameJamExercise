using OsukaCreative.Utility.GameEvent;
using OsukaCreative.Utility.Sets;
using OsukaCreative.Utility.Variables;
using System.Collections;
using UnityEngine;


public class EnemyHandler : MonoBehaviour {

    [SerializeField]
    private GameObjectSet enemiesInWorld;
    [SerializeField]
    private NoArgEvent onLevelComplete;
    [SerializeField]
    private BoolReference isGameOn;

    public void Clear() {
        this.enemiesInWorld.Clear();
    }

    public void OnEnemyDeath(GameObject enemy) {
        this.enemiesInWorld.Remove(enemy);

        if (this.enemiesInWorld.Length <= 0) {
            this.isGameOn.Value = false;
            this.onLevelComplete.Raise();
        }
    }

}
