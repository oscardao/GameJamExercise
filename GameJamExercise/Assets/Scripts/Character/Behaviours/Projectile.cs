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

    public void Activate(Vector2 direction) {

        StartCoroutine(ShootCO(direction));
    }

    private IEnumerator ShootCO(Vector2 direction) {
        float timer = 0;

        direction.Normalize();
        this.transform.right = direction;
        while (timer < this.destroyTime) {
            timer += Time.deltaTime;
            transform.position += (Vector3)direction * this.speed * Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }

}
