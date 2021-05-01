using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Effects/Move Towards")]
public class MoveTowards : ScriptableObject {

    [SerializeField]
    private bool moveTowards;
    [SerializeField]
    private float moveSpeed;

    public IEnumerator MoveObjectTowards(GameObject mover, Vector3 direction, float duration) {
        IAnimateable animator = mover.GetComponent<IAnimateable>();

        Transform transform = animator.AnimateableTransform;
        Vector3 initialPosition = transform.position;

        float time = 0;

        Vector3 normalizedDirection = direction.normalized;

        while (time < duration) {
            time += Time.deltaTime;
            transform.position = transform.position + normalizedDirection * moveSpeed * Time.deltaTime;
            yield return null;
        }
        transform.position = initialPosition;
    }

}
