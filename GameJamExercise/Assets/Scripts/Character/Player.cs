using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICommandable, IPositionable {

    [SerializeField]
    private TurnHandler turnHandler;
    [SerializeField]
    private PlayerActionController playerActionController;

    private WorldTile position;
    public WorldTile Position {
        get { return this.position; }
        set { this.position = value; }
    }

    private void Awake() {
        this.turnHandler.player = this;
    }

    public void TakeTurn() {
        this.playerActionController.HighlightTiles();
    }

}
