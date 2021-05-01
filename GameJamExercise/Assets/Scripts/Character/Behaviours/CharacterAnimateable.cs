using System.Collections;
using UnityEngine;


public class CharacterAnimateable : MonoBehaviour, IAnimateable, IFlipable {

    [Header("Animateable")]
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer {
        get { return this.spriteRenderer; }
    }

    [SerializeField]
    private Transform animateableTransform;
    public Transform AnimateableTransform {
        get { return this.animateableTransform; }
    }

    public void SetTrigger(string trigger) {
        this.animator.SetTrigger(trigger);
    }

    [Header("Flipable")]
    [SerializeField, Tooltip("True=facing left, False=facing right")]
    private bool valueOnAwake;

    private bool isFlipped;
    public bool IsFlipped {
        get { return this.isFlipped; }
        set { this.isFlipped = value; }
    }

    public Transform FlipableTransform {
        get { return this.animateableTransform; }
    }

    private void Awake() {
        this.isFlipped = this.valueOnAwake;
    }
}
