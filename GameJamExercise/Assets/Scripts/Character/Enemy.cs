using OsukaCreative.Utility.Variables;
using System.Collections;
using UnityEngine;


public class Enemy : BaseEntity, IInteractable {

    [SerializeField]
    private GameObjectReference target;

    public void OnInteract(GameObject interacter) {

    }
}
