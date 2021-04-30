using OsukaCreative.Utility.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICommandable {

    [SerializeField]
    private GameObjectVariable player;

    [SerializeField]
    private PlayerActionController playerActionController;

    [Header("Turn")]
    [SerializeField]
    private TurnHandler turnHandler;
    [SerializeField]
    private int team;
    [SerializeField]
    private IntReference baseTurns;
    [SerializeField]
    private IntReference remainingTurns;
    [SerializeField]
    private BaseAction defaultAction;

    private void Awake() {
        this.player.Value = gameObject;
        this.turnHandler.AddCommandable(this.team, this);
    }

    public void TakeTurn() {
        this.remainingTurns.Value = this.baseTurns.Value;
        this.playerActionController.HighlightTiles();
    }

    public void PerformAction(WorldTile targetTile) {
        StartCoroutine(PerformActionCO(targetTile));
    }

    public IEnumerator PerformActionCO(WorldTile targetTile) {
        this.remainingTurns.Value--;
        Debug.Log(targetTile);
        if (targetTile.ObjectOnTile == null) {
            this.defaultAction.Perform(targetTile, gameObject);
            yield return new WaitForSeconds(this.defaultAction.Duration);

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
