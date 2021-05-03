using OsukaCreative.Utility.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turn Handler")]
public class TurnHandler : ScriptableObject {

    private Dictionary<int, List<ICommandable>> teams = new Dictionary<int, List<ICommandable>>();

    private List<ICommandable> round;
    private int currentTurn;

    [SerializeField]
    private BoolReference IsGameOn;

    public void AddCommandable(int team, ICommandable commandable) {
        if (!this.teams.ContainsKey(team)) {
            this.teams.Add(team, new List<ICommandable>());
        }

        this.teams[team].Add(commandable);
    }

    public void RemoveCommandable(int team, ICommandable commandable) {
        if (this.teams.ContainsKey(team)) {
            this.teams[team].Remove(commandable);
        }

    }

    private void PrepareRound() {
        this.currentTurn = -1;
        this.round = new List<ICommandable>();

        List<int> sortedTeams = new List<int>(this.teams.Keys);
        sortedTeams.Sort();

        for (int i = 0; i < sortedTeams.Count; i++) {
            this.round.AddRange(this.teams[sortedTeams[i]]);
        }

    }

    public void StartRound() {
        this.IsGameOn.Value = true;
        PrepareRound();

        for (int i = 0; i < this.round.Count; i++) {
            this.round[i].IsActive = true;
        }

        NextTurn();
    }

    public void NextTurn() {
        if (!this.IsGameOn.Value) return;
        this.currentTurn++;

        if (this.currentTurn >= this.round.Count) {
            PrepareRound();
            this.currentTurn++;
        }

        if (this.round[this.currentTurn].IsActive) {
            this.round[this.currentTurn].TakeTurn();
        } else {
            NextTurn();
        }

    }

}
