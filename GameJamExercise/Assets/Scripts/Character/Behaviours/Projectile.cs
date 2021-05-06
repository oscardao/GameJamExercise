using System.Collections;
using UnityEngine;


public class Projectile : MonoBehaviour {

    [SerializeField, Min(0)]
    private float destroyTime;
    [SerializeField]
    private float speed;
    public float Speed {
        get { return this.speed; }
    }

    public void Go(Vector2 direction) {

    }

    private IEnumerator GoCO(Vector2 direction) {

        float timer = 0;

        while (timer < this.destroyTime) {
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

}
