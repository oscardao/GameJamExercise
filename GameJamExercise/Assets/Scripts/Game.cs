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

    private void Start() {
        StartGame();
    }

    public void StartGame() {
        this.world.Generate();
        this.turnHandler.StartRound();
    }

    public void NextLevel() {

    }

    public void EndGame() {

    }


}
