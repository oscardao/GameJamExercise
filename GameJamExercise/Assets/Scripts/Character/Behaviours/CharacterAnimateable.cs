using System.Collections;
using UnityEngine;


public class CharacterAnimateable : MonoBehaviour, IAnimateable {

    [SerializeField]
    private Animator animator;

    public void SetTrigger(string trigger) {
        this.animator.SetTrigger(trigger);
    }
}
