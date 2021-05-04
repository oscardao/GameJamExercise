using OsukaCreative.Utility.Variables;
using System.Collections;
using UnityEngine;


public class Game : MonoBehaviour {

    [SerializeField]
    private IntReference level;

    [SerializeField]
    private TurnHandler turnHandler;
    [SerializeField]
    private WorldHandler world;

    [SerializeField]
    private float startGameDelay;

    public void StartGame() {
        this.level.Value = 1;
        StartCoroutine(StartGameCO());
    }

    public void NextLevel() {
        this.level.Value++;
    }

    public void EndGame() {

    }

    private IEnumerator StartGameCO() {
        yield return new WaitForSeconds(this.startGameDelay);
        this.world.Generate();
        this.turnHandler.StartRound();
    }


}
