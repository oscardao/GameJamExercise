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

    [Header("Events")]
    [SerializeField]
    private NoArgEvent onGameStarted;
    [SerializeField]
    private NoArgEvent onGameEnded;
    [SerializeField]
    private NoArgEvent onClearTrash;

    [Header("Timers")]
    [SerializeField]
    private float startGameDelay;
    [SerializeField]
    private float nextLevelDelay;
    [SerializeField]
    private float endGameDelay;

    public static Game Instance;

    private void Awake() {
        Game.Instance = this;
    }

    public void StartGame() {
        this.level.Value = 1;
        this.onClearTrash.Raise();
        StartCoroutine(StartGameCO());
    }

    public void NextLevel() {
        this.onClearTrash.Raise();
        StartCoroutine(NextLevelCO());
    }

    public void EndGame() {
        this.onClearTrash.Raise();
        StartCoroutine(EndGameCO());
    }

    private IEnumerator EndGameCO() {
        yield return new WaitForSeconds(this.endGameDelay);
        this.onGameEnded.Raise();
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
