using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Move")]
public class MoveAction : BaseAction {

    [Header("Effects")]
    [SerializeField]
    private Flip flip;
    [SerializeField]
    private Move move;

    [SerializeField, Range(0, 1)]
    private float flipFractionOfDuration;

    public override void Perform(WorldTile tile, GameObject gameObject) {
        tile.StartCoroutine(this.flip.FlipObject(tile.WorldPosition, this.duration * this.flipFractionOfDuration, gameObject));
        tile.StartCoroutine(this.move.MoveTo(tile, this.duration, gameObject));
    }

}
