using OsukaCreative.Utility.RangedVariables;
using OsukaCreative.Utility.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RangedEnemy : MonoBehaviour, ITargeting, IShootable {
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
    private Vector2Int shootDirection;
    public Vector2Int ShootDirection {
        get { return this.shootDirection; }
        set { this.shootDirection = value; }
    }

    [SerializeField]
    private Transform shootingPoint;
    public Transform ProjectileSpawn {
        get { return this.shootingPoint; }
    }

    [Header("Actions")]
    [SerializeField]
    private RangedAttack shootingAction;

    [SerializeField]
    private GameObject dangerIcon;
    private Stack<GameObject> activeDangerIcons;

    [SerializeField]
    private AIBrain brain;

    private void Awake() {
        this.coolDown = this.shotCoolDown.Value;
        this.activeDangerIcons = new Stack<GameObject>();
        this.IsPreparedToShoot = false;
        this.commandable = GetComponent<ICommandable>();
    }

    public void TakeTurn() {
        StartCoroutine(PerformAction());
    }

    private IEnumerator PerformAction() {
        if (this.IsPreparedToShoot) {
            this.IsPreparedToShoot = false;
            this.coolDown = this.shotCoolDown.Value;
            yield return this.shootingAction.Shoot(gameObject, this.activeDangerIcons);

        } else {
            this.coolDown--;
            if (this.coolDown <= 0) {
                this.IsPreparedToShoot = true;
                yield return this.shootingAction.PrepareShot(gameObject, this.activeDangerIcons);

            } else {
                yield return this.brain.OnTakeTurn(gameObject);
            }
        }

        this.commandable.TurnHandler.NextTurn();
    }

}
