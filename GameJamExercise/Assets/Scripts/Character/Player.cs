using OsukaCreative.Utility.GameEvent;
using OsukaCreative.Utility.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private PlayerActionController playerActionController;
    [SerializeField]
    private BoolReference isGameOn;

    [Header("Turn Handling")]
    [SerializeField]
    private IntReference baseTurns;
    [SerializeField]
    private IntReference remainingTurns;
    private bool hasTurn;
    private ICommandable commandable;
    [SerializeField]
    private NoArgEvent onPlayerTakeAction;

    [Header("Inventory")]
    [SerializeField]
    private IntReference clocks;

    private void Awake() {
        this.commandable = GetComponent<ICommandable>();
    }

    public void TakeTurn() {
        this.hasTurn = true;
        this.remainingTurns.Value = this.baseTurns.Value;
        this.playerActionController.HighlightTiles();
    }

    public void PerformAction(BaseInteraction interaction, WorldTile tile) {
        this.hasTurn = false;
        this.onPlayerTakeAction.Raise();
        StartCoroutine(PerformActionCO(interaction, tile));
    }

    public IEnumerator PerformActionCO(BaseInteraction interaction, WorldTile tile) {
        this.remainingTurns.Value--;

        yield return interaction.Perform(tile, gameObject);

        if (this.isGameOn.Value) {
            if (this.remainingTurns.Value <= 0) {
                this.commandable.TurnHandler.NextTurn();
            } else {
                this.hasTurn = true;
                this.playerActionController.HighlightTiles();
            }
        }

    }

    private void Update() {
        if (this.hasTurn && Input.GetKeyDown(KeyCode.C) && this.clocks.Value > 0) {
            this.clocks.Value--;
            this.remainingTurns.Value++;
        }
    }

}
