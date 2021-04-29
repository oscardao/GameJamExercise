using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turn Handler")]
public class TurnHandler : ScriptableObject {

    private Dictionary<int, List<ICommandable>> teams = new Dictionary<int, List<ICommandable>>();

    private List<ICommandable> round;
    private int currentTurn;

    public void AddCommandable(int team, ICommandable commandable) {
        if (!this.teams.ContainsKey(team)) {
            this.teams.Add(team, new List<ICommandable>());
        }

        this.teams[team].Add(commandable);
    }

    private void PrepareRound() {
        this.currentTurn = 0;
        this.round = new List<ICommandable>();

        List<int> sortedTeams = new List<int>(this.teams.Keys);
        sortedTeams.Sort();

        for (int i = 0; i < sortedTeams.Count; i++) {
            this.round.AddRange(this.teams[sortedTeams[i]]);
        }

    }

    public void StartRound() {
        PrepareRound();
        NextTurn();
    }

    public void NextTurn() {
        this.round[this.currentTurn].TakeTurn();
        this.currentTurn++;

        if (this.currentTurn >= this.round.Count) {
            PrepareRound();
        }

    }

}
