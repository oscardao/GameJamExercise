using OsukaCreative.Utility.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICommandable {

    [SerializeField]
    private GameObjectVariable player;

    [SerializeField]
    private PlayerActionController playerActionController;
    [SerializeField]
    private BoolReference isGameOn;

    [Header("Turn")]
    [SerializeField]
    private TurnHandler turnHandler;
    [SerializeField]
    private int team;
    [SerializeField]
    private IntReference baseTurns;
    [SerializeField]
    private IntReference remainingTurns;

    private bool isActive;
    public bool IsActive {
        get { return this.isActive; }
        set { this.isActive = value; }
    }

    private void Awake() {
        this.player.Value = gameObject;
        this.turnHandler.AddCommandable(this.team, this);
    }

    public void TakeTurn() {
        this.remainingTurns.Value = this.baseTurns.Value;
        this.playerActionController.HighlightTiles();
    }

    public void PerformAction(BaseInteraction interaction, WorldTile tile) {
        StartCoroutine(PerformActionCO(interaction, tile));
    }

    public IEnumerator PerformActionCO(BaseInteraction interaction, WorldTile tile) {
        this.remainingTurns.Value--;

        yield return interaction.Perform(tile, gameObject);

        if (this.isGameOn.Value) {
            if (this.remainingTurns.Value <= 0) {
                this.turnHandler.NextTurn();
            } else {
                this.playerActionController.HighlightTiles();
            }
        }

    }

}
