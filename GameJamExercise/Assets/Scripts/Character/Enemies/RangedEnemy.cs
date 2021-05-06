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

    [Header("Actions")]
    [SerializeField]
    private PrepareShot prepareShotAction;
    private IAnimateable animator;

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
        this.animator = GetComponent<IAnimateable>();
    }

    public void TakeTurn() {
        StartCoroutine(PerformAction());
    }

    private IEnumerator PerformAction() {
        if (this.IsPreparedToShoot) {
            this.IsPreparedToShoot = false;
            this.coolDown = this.shotCoolDown.Value;
            yield return Shoot();

        } else {
            this.coolDown--;
            if (this.coolDown <= 0) {
                this.IsPreparedToShoot = true;
                this.animator.SetBool("isPrepared", this.IsPreparedToShoot);
                yield return this.prepareShotAction.Perform(gameObject, this.activeDangerIcons);
                yield return new WaitForSeconds(0.15f);
                this.commandable.TurnHandler.NextTurn();
                yield break;
            }
            yield return this.brain.OnTakeTurn(gameObject);

        }

        this.commandable.TurnHandler.NextTurn();
    }

    private IEnumerator Shoot() {
        this.animator.SetBool("isPrepared", false);
        while (this.activeDangerIcons.Count > 0) {
            Destroy(this.activeDangerIcons.Pop());
        }
        yield return null;
    }
}
