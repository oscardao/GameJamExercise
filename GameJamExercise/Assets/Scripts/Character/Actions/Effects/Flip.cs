using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Effects/Flip")]
public class Flip : ScriptableObject {

    public IEnumerator FlipObject(Vector3 towardsPosition, float duration, GameObject gameObject) {
        Vector3 currentPosition = gameObject.transform.position;

        bool shouldFlip = false;
        float y = 0;

        Quaternion currentRotation = gameObject.transform.rotation;
        IFlipable flipable = gameObject.GetComponent<IFlipable>();

        if ((currentPosition.x > towardsPosition.x) && !flipable.IsFlipped) {
            y = 180;
            shouldFlip = true;
        } else if ((currentPosition.x < towardsPosition.x) && flipable.IsFlipped) {
            shouldFlip = true;
        }

        if (shouldFlip) {
            flipable.IsFlipped = !flipable.IsFlipped;
            Quaternion flipDirection = Quaternion.Euler(new Vector3(currentRotation.x, y, currentRotation.z));
            yield return Perform(duration, flipDirection, flipable.FlipableTransform);
        }

    }

    private IEnumerator Perform(float duration, Quaternion endValue, Transform transform) {
        float time = 0;
        Quaternion startValue = transform.rotation;

        while (time < duration) {
            transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endValue;

    }

}
