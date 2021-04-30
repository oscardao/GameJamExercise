using OsukaCreative.Utility.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour, IInteractable, ICommandable {

    [Header("Turn Handling")]
    [SerializeField]
    private TurnHandler turnHandler;
    [SerializeField]
    private int team;

    [SerializeField]
    private World world;

    [Header("Actions")]
    [SerializeField]
    private GameObjectReference target;
    [SerializeField]
    private PathFinding pathFinding;
    [SerializeField]
    private BaseAction moveAction;
    [SerializeField]
    private BaseAction attackAction;

    private IPositionable positionable;

    private void Awake() {
        this.positionable = GetComponent<IPositionable>();
        this.turnHandler.AddCommandable(this.team, this);
    }

    public void OnInteract(GameObject interacter) {

    }

    public void TakeTurn() {
        StartCoroutine(PerformAction());
    }

    private IEnumerator PerformAction() {

        Stack<WorldTile> pathToTarget = new Stack<WorldTile>();
        IPositionable targetPositionable = this.target.Value.GetComponent<IPositionable>();
        yield return this.pathFinding.FindPath(targetPositionable.WorldTile, this.positionable.WorldTile, pathToTarget);

        Debug.Log(pathToTarget.Count);
        while (pathToTarget.Count > 0) {
            WorldTile tile = pathToTarget.Pop();
            Debug.Log("IsOnTile: " + this.positionable.WorldTile.Position + "Checking Tile: " + tile.ToString());

            if (tile == this.positionable.WorldTile) {
                Debug.Log("Continueing");
                continue;

            } else if (tile.ObjectOnTile == this.target.Value) {
                //Attack;
                Debug.Log("Attacking");
                break;

            } else if ((tile.ObjectOnTile != null) && (tile.ObjectOnTile != this.target.Value)) {
                //Switch
                Debug.Log("Switching");
                break;

            } else {
                //Move
                Debug.Log("moving");
                yield return PerformAction(this.moveAction, tile);
                break;
            }
        }

        Debug.Log("Passing Turn");
        this.turnHandler.NextTurn();
    }

    private IEnumerator PerformAction(BaseAction action, WorldTile tile) {
        action.Perform(tile, gameObject);
        yield return new WaitForSeconds(action.Duration);
    }
}
