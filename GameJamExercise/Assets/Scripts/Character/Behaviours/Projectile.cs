using System.Collections;
using UnityEngine;


public class Projectile : MonoBehaviour, IProjectile {

    [SerializeField, Min(0)]
    private float destroyTime;
    [SerializeField]
    private float speed;
    public float Speed {
        get { return this.speed; }
    }

    public IEnumerator Activate(Vector2 endPoint) {
        float timer = 0;

        endPoint.Normalize();
        this.transform.right = endPoint;
        while (timer < this.destroyTime) {
            timer += Time.deltaTime;
            transform.position += (Vector3)endPoint * this.speed * Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }

}
