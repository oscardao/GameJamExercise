using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Move")]
public class MoveAction : BaseAction {

    private Flip flip = new Flip();
    private Move move = new Move();

    [SerializeField, Range(0, 1)]
    private float flipFractionOfDuration;

    public override void Perform(WorldTile tile, GameObject gameObject) {

        Flip(tile, gameObject);
        tile.StartCoroutine(this.move.MoveTo(tile, this.duration, gameObject));
    }

    private void Flip(WorldTile tile, GameObject gameObject) {
        Vector3 currentPosition = gameObject.transform.position;
        Vector3 targetPosition = tile.WorldPosition;

        bool shouldFlip = false;
        float y = 0;

        Quaternion currentRotation = gameObject.transform.rotation;
        IFlipable flipable = gameObject.GetComponent<IFlipable>();

        if ((currentPosition.x > targetPosition.x) && !flipable.IsFlipped) {
            y = 180;
            shouldFlip = true;
        } else if ((currentPosition.x < targetPosition.x) && flipable.IsFlipped) {
            shouldFlip = true;
        }

        if (shouldFlip) {
            flipable.IsFlipped = !flipable.IsFlipped;
            Quaternion flipDirection = Quaternion.Euler(new Vector3(currentRotation.x, y, currentRotation.z));
            tile.StartCoroutine(this.flip.Perform(this.duration * this.flipFractionOfDuration, flipDirection, gameObject));
        }
    }

}
