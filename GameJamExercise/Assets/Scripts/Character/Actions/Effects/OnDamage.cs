using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Effects/On Damage")]
public class OnDamage : ScriptableObject {

    [Header("Animations")]
    [SerializeField]
    private string onDamageTrigger = "onDamage";
    [SerializeField]
    private string onIdleTrigger = "onIdle";

    [Header("Game Feel")]
    [SerializeField]
    private Material flashMaterial;
    [SerializeField]
    private float knockBackForce;

    [SerializeField]
    private AudioClip[] sounds;

    public IEnumerator DamageObject(GameObject target, float duration) {
        AudioManager.Instance.PlayAudio(this.sounds[UnityEngine.Random.Range(0, this.sounds.Length)]);
        IAnimateable targetAnimator = target.GetComponent<IAnimateable>();
        targetAnimator.SetTrigger(this.onDamageTrigger);

        IDamageable targetDamageable = target.GetComponent<IDamageable>();
        targetDamageable.OnDamage();

        if (targetDamageable.IsDead) {
            IPositionable targetPositionable = target.GetComponent<IPositionable>();
            targetPositionable.WorldTile.ObjectOnTile = null;
        }

        SpriteRenderer spriteRenderer = targetAnimator.SpriteRenderer;
        Material initialMaterial = spriteRenderer.material;
        spriteRenderer.material = this.flashMaterial;

        float time = 0;

        while (time < duration) {
            time += Time.deltaTime;

            spriteRenderer.material.SetFloat("_FlashAmount", 1f - (time / duration));
            yield return null;
        }

        spriteRenderer.material.SetFloat("_FlashAmount", 0);
        spriteRenderer.material = initialMaterial;
        targetAnimator.SetTrigger(this.onIdleTrigger);

        if (targetDamageable.IsDead) {
            target.SetActive(false);
        }

    }

}
