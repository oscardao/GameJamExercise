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

    public IEnumerator DamageObject(GameObject target, float duration) {
        IAnimateable targetAnimator = target.GetComponent<IAnimateable>();
        targetAnimator.SetTrigger(this.onDamageTrigger);

        SpriteRenderer spriteRenderer = targetAnimator.SpriteRenderer;
        Material initialMaterial = spriteRenderer.material;
        spriteRenderer.material = this.flashMaterial;

        float time = 0;

        while (time < duration) {
            time += Time.deltaTime;

            spriteRenderer.material.SetFloat("_FlashAmount", 0.6f - (time / duration));
            yield return null;
        }

        spriteRenderer.material.SetFloat("_FlashAmount", 0);
        spriteRenderer.material = initialMaterial;
        targetAnimator.SetTrigger(this.onIdleTrigger);
    }

}
