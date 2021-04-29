using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Move")]
public class MoveAction : BaseAction {

    private Flip flip = new Flip();
    private Move move = new Move();

    [SerializeField, Range(0, 1)]
    private float flipFractionOfDuration;

    public override void Perform(WorldTile tile, GameObject gameObject) {
        tile.StartCoroutine(this.flip.FlipObject(tile, this.duration * this.flipFractionOfDuration, gameObject));
        tile.StartCoroutine(this.move.MoveTo(tile, this.duration, gameObject));
    }

}
