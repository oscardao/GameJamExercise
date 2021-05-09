using System.Collections;
using UnityEngine;


public class Projectile : MonoBehaviour, IProjectile {

    [Header("Projectile Properties")]
    [SerializeField, Min(0)]
    private float destroyTime;
    [SerializeField]
    private float speed;
    public float Speed {
        get { return this.speed; }
    }
    [SerializeField]
    private float rotationPerSecond;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [Header("Behaviour")]
    [SerializeField]
    private Flip flip;
    [SerializeField]
    private OnDamage onDamage;
    [SerializeField]
    private MoveTowards knockBack;
    public float onDamageDuration;
    public float onHitTargetDuration;

    public void HitTarget(GameObject shooter, GameObject target, Transform shootingPoint) {
        StartCoroutine(HitTargetCO(shooter, target, shootingPoint));
    }

    private IEnumerator HitTargetCO(GameObject shooter, GameObject target, Transform shootingPoint) {
        Vector3 endValue = target.transform.position + shootingPoint.localPosition;
        // this.transform.right = (endValue - shootingPoint.position).normalized;

        float timer = 0;
        while (timer < this.onHitTargetDuration) {
            transform.position = Vector3.Lerp(shootingPoint.position, endValue, timer / this.onHitTargetDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        this.spriteRenderer.enabled = false;
        OnDamageTarget(shooter, target);
    }

    private void OnDamageTarget(GameObject shooter, GameObject target) {
        StartCoroutine(this.flip.FlipObject(shooter.transform.position, 0, target));
        StartCoroutine(this.onDamage.DamageObject(target, this.onDamageDuration));
        StartCoroutine(this.knockBack.MoveObjectTowards(target, transform.right, this.onDamageDuration));
    }

    public void ActivateProjectile(Vector2 direction) {
        StartCoroutine(Activate(direction));
    }

    public IEnumerator Activate(Vector2 direction) {
        float timer = 0;

        direction.Normalize();
        // this.transform.right = direction;
        while (timer < this.destroyTime) {
            timer += Time.deltaTime;
            transform.position += (Vector3)direction * this.speed * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void Update() {
        transform.Rotate(0, 0, this.rotationPerSecond * Time.deltaTime);
    }

}
