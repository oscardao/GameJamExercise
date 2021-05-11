using OsukaCreative.Utility.RangedVariables;
using OsukaCreative.Utility.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemy : MonoBehaviour, ITargeting {
    [Header("Targeting")]
    [SerializeField]
    private GameObjectReference target;
    public GameObject Target {
        get { return this.target.Value; }
    }

    [SerializeField]
    private World world;

    [Header("Turn Handling")]
    private ICommandable commandable;

    [Header("Behaviour")]
    private bool IsPrepared;
    [SerializeField]
    private RangedInt shotCoolDown;
    private int coolDown;

    [Header("Actions")]
    private ISpecialEnemyBehaviour specialBehaviour;

    [SerializeField]
    private AIBrain brain;

    private void Awake() {
        this.coolDown = this.shotCoolDown.Value;
        this.IsPrepared = false;
        this.commandable = GetComponent<ICommandable>();
        this.specialBehaviour = GetComponent<ISpecialEnemyBehaviour>();
    }

    public void TakeTurn() {
        StartCoroutine(PerformAction());
    }

    private IEnumerator PerformAction() {
        if (this.IsPrepared) {
            this.IsPrepared = false;
            this.coolDown = this.shotCoolDown.Value;
            yield return this.specialBehaviour.Perform();

        } else {
            this.coolDown--;
            if (this.coolDown <= 0) {
                this.IsPrepared = true;
                yield return this.specialBehaviour.Prepare();

            } else {
                yield return this.brain.OnTakeTurn(gameObject);
            }
        }

        this.commandable.TurnHandler.NextTurn();
    }

}
