using OsukaCreative.Utility.Variables;
using System.Collections;
using UnityEngine;


public class Enemy : BaseEntity, IInteractable, ICommandable {

    [SerializeField]
    private GameObjectReference target;

    [SerializeField]
    private TurnHandler turnHandler;
    [SerializeField]
    private int team;

    private void Awake() {
        this.turnHandler.AddCommandable(this.team, this);
    }

    public void OnInteract(GameObject interacter) {

    }

    public void TakeTurn() {
        StartCoroutine(PerformAction());
    }

    private IEnumerator PerformAction() {
        Debug.Log(gameObject.name + " Performing Action");
        yield return new WaitForSeconds(1f);
        Debug.Log(gameObject.name + " Done");
        this.turnHandler.NextTurn();
    }
}
