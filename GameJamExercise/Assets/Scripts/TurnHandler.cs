using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turn Handler")]
public class TurnHandler : ScriptableObject {

    public ICommandable player;
    public readonly List<ICommandable> enemies = new List<ICommandable>();

    public void NextTurn() {
        this.player.TakeTurn();
    }

}
