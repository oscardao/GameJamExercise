using OsukaCreative.Utility.RangedVariables;
using OsukaCreative.Utility.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShootableBehaviour))]
public class RangedEnemy : MonoBehaviour, ITargeting {
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
    private bool IsPreparedToShoot;
    [SerializeField]
    private RangedInt shotCoolDown;
    private int coolDown;

    [Header("Actions")]
    private ShootableBehaviour shootingAction;

    [SerializeField]
    private AIBrain brain;

    private void Awake() {
        this.coolDown = this.shotCoolDown.Value;
        this.IsPreparedToShoot = false;
        this.commandable = GetComponent<ICommandable>();
        this.shootingAction = GetComponent<ShootableBehaviour>();
    }

    public void TakeTurn() {
        StartCoroutine(PerformAction());
    }

    private IEnumerator PerformAction() {
        if (this.IsPreparedToShoot) {
            this.IsPreparedToShoot = false;
            this.coolDown = this.shotCoolDown.Value;
            yield return this.shootingAction.Shoot();

        } else {
            this.coolDown--;
            if (this.coolDown <= 0) {
                this.IsPreparedToShoot = true;
                yield return this.shootingAction.PrepareShot();

            } else {
                yield return this.brain.OnTakeTurn(gameObject);
            }
        }

        this.commandable.TurnHandler.NextTurn();
    }

}
