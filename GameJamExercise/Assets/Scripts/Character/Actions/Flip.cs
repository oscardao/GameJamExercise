using System.Collections;
using UnityEngine;


public class Flip {

    public IEnumerator Perform(float duration, Quaternion endValue, GameObject gameObject) {
        float time = 0;
        Transform transform = gameObject.transform;
        Quaternion startValue = transform.rotation;

        while (time < duration) {
            transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endValue;

    }

}
