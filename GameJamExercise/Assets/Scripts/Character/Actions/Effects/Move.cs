using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Effects/Move")]
public class Move : ScriptableObject {

    public IEnumerator MoveTo(WorldTile targetTile, float duration, GameObject gameObject) {
        WorldTile currentTile = gameObject.GetComponent<IPositionable>().WorldTile;
        currentTile.ObjectOnTile = null;
        targetTile.ObjectOnTile = gameObject;
        yield return MoveCO(targetTile.WorldPosition, duration, gameObject);
    }

    private IEnumerator MoveCO(Vector3 targetPosition, float duration, GameObject gameObject) {
        float time = 0;
        Transform transform = gameObject.transform;
        Vector3 startPosition = transform.position;

        while (time < duration) {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

}
