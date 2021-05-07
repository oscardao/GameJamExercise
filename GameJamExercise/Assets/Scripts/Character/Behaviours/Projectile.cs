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

    [SerializeField]
    private Flip flip;
    [SerializeField]
    private OnDamage onDamage;
    [SerializeField]
    private float onDamageDuration;

    public IEnumerator HitTarget(GameObject shooter, GameObject target) {


        yield return this.flip.FlipObject(shooter.transform.position, 0, target);
        yield return this.onDamage.DamageObject(target, this.onDamageDuration);
    }

    public IEnumerator Activate(Vector2 direction) {
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
