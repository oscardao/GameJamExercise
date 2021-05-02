using OsukaCreative.Utility.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour, ICommandable, ITargeting {

    [Header("Turn Handling")]
    [SerializeField]
    private TurnHandler turnHandler;
    [SerializeField]
    private int team;

    private bool isActive;
    public bool IsActive {
        get { return this.isActive; }
        set { this.isActive = value; }
    }

    [Header("Targeting")]
    [SerializeField]
    private GameObjectReference target;
    public GameObject Target {
        get { return this.target.Value; }
    }



    [Header("AI")]
    [SerializeField]
    private AIBrain brain;

    private void Awake() {
        this.turnHandler.AddCommandable(this.team, this);
    }

    public void TakeTurn() {
        StartCoroutine(PerformAction());
    }

    private IEnumerator PerformAction() {
        yield return this.brain.OnTakeTurn(gameObject);
        this.turnHandler.NextTurn();
    }

}
