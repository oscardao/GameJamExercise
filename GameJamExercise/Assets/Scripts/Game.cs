using OsukaCreative.Utility.GameEvent;
using OsukaCreative.Utility.Variables;
using System.Collections;
using UnityEngine;


public class Game : MonoBehaviour {

    [Header("Game")]
    [SerializeField]
    private IntReference level;
    [SerializeField]
    private TurnHandler turnHandler;
    [SerializeField]
    private WorldHandler world;

    [SerializeField]
    private NoArgEvent onGameStarted;

    [Header("Timers")]
    [SerializeField]
    private float startGameDelay;
    [SerializeField]
    private float nextLevelDelay;
    [SerializeField]
    private float endGameDelay;

    public void StartGame() {
        this.level.Value = 1;
        StartCoroutine(StartGameCO());
    }

    public void NextLevel() {
        StartCoroutine(NextLevelCO());
    }

    public void EndGame() {
        StartCoroutine(EndGameCO());
    }

    private IEnumerator EndGameCO() {
        yield return new WaitForSeconds(this.endGameDelay);
        yield return this.world.ClearWorld();
    }

    private IEnumerator NextLevelCO() {
        yield return new WaitForSeconds(this.nextLevelDelay);
        yield return this.world.ClearWorld();
        this.level.Value++;
        yield return new WaitForSeconds(this.nextLevelDelay);
        yield return this.world.Generate();
        this.turnHandler.StartRound();
    }

    private IEnumerator StartGameCO() {
        yield return new WaitForSeconds(this.startGameDelay);
        yield return this.world.Generate();
        this.onGameStarted.Raise();
        this.turnHandler.StartRound();
    }


}
