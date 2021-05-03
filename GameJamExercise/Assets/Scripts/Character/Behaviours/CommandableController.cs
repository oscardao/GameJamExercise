using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CommandableController : MonoBehaviour, ICommandable {

    [SerializeField]
    private UnityEvent onTakeTurn;

    private bool isActive;
    public bool IsActive {
        get { return this.isActive; }
        set { this.isActive = value; }
    }

    [Header("Turn")]
    [SerializeField]
    private TurnHandler turnHandler;
    public TurnHandler TurnHandler {
        get { return this.turnHandler; }
    }
    [SerializeField]
    private int team;

    private void Awake() {
        this.turnHandler.AddCommandable(this.team, this);
    }

    private void OnDestroy() {
        this.turnHandler.RemoveCommandable(this.team, this);
    }

    public void TakeTurn() {
        this.onTakeTurn.Invoke();
    }
}
