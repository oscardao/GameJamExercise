using System.Collections;
using UnityEngine;


public class ItemBehaviour : MonoBehaviour, IInteractable {

    [SerializeField]
    private BaseInteraction interaction;

    public BaseInteraction GetInteraction(WorldTile tile, GameObject interacter) {
        return this.interaction;
    }
}
