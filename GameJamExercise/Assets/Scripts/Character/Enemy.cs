using OsukaCreative.Utility.Sets;
using OsukaCreative.Utility.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour, ITargeting {

    private ICommandable commandable;

    [Header("Targeting")]
    [SerializeField]
    private GameObjectReference target;
    public GameObject Target {
        get { return this.target.Value; }
    }

    [Header("AI")]
    [SerializeField]
    private AIBrain brain;
    [SerializeField]
    private GameObjectSet enemiesInWorld;

    private void Awake() {
        this.commandable = GetComponent<ICommandable>();
    }

    public void TakeTurn() {
        StartCoroutine(PerformAction());
    }

    private IEnumerator PerformAction() {
        yield return this.brain.OnTakeTurn(gameObject);
        this.commandable.TurnHandler.NextTurn();
    }

}
