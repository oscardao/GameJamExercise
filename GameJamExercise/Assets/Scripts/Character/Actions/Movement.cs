using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Move")]
public class Movement : ScriptableObject {

    [SerializeField, Min(0)]
    private float moveDuration;

    public IEnumerator MoveTo(WorldTile targetTile, GameObject gameObject) {
        WorldTile currentTile = gameObject.GetComponent<IPositionable>().Position;
        currentTile.ObjectOnTile = null;
        targetTile.ObjectOnTile = gameObject;
        yield return Move(targetTile.WorldPosition, gameObject);
    }

    private IEnumerator Move(Vector3 targetPosition, GameObject gameObject) {
        float time = 0;
        Transform transform = gameObject.transform;
        Vector3 startPosition = transform.position;

        while (time < this.moveDuration) {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / this.moveDuration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

}
