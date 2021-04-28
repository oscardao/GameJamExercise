using OsukaCreative.Utility.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseEntity, ICommandable {

    [SerializeField]
    private GameObjectVariable player;

    [SerializeField]
    private PlayerActionController playerActionController;

    [Header("Turn")]
    [SerializeField]
    private TurnHandler turnHandler;
    [SerializeField]
    private IntReference baseTurns;
    [SerializeField]
    private IntReference remainingTurns;
    [SerializeField]
    private Movement moveAction;

    private void Awake() {
        this.player.Value = gameObject;
        this.turnHandler.player = this;
    }

    public void TakeTurn() {
        this.remainingTurns.Value = this.baseTurns.Value;
        this.playerActionController.HighlightTiles();
    }

    public void PerformAction(WorldTile targetTile) {
        StartCoroutine(PerformActionCO(targetTile));
    }

    public IEnumerator PerformActionCO(WorldTile targetTile) {
        Debug.Log(targetTile);
        if (targetTile.ObjectOnTile == null) {
            yield return this.moveAction.MoveTo(targetTile, gameObject);
        } else {
            GameObject objectOnTile = targetTile.ObjectOnTile;
        }

        if (this.remainingTurns.Value <= 0) {
            this.turnHandler.NextTurn();
        } else {
            this.playerActionController.HighlightTiles();
        }
    }

}
