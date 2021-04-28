using System.Collections;
using UnityEngine;


public class Movement : MonoBehaviour {

    [SerializeField]
    private IPositionable positionable;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string moveTrigger = "move";
    [SerializeField, Min(0)]
    private float moveDuration;

    public IEnumerator MoveTo(WorldTile targetTile) {
        this.animator.SetTrigger(moveTrigger);
        WorldTile currentTile = this.positionable.Position;
        currentTile.RemoveObject(gameObject);
        targetTile.AddObject(gameObject);
    }
}
