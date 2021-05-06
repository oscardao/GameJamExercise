using System.Collections;
using UnityEngine;


public class InteractableController : MonoBehaviour, IInteractable {

    [SerializeField]
    private BaseInteraction[] interactions;
    [SerializeField]
    private BaseInteraction defaultInteraction;

    public BaseInteraction GetInteraction(WorldTile tile, GameObject interacter) {

        for (int i = 0; i < this.interactions.Length; i++) {
            if (this.interactions[i].Evaluate(tile, interacter)) {
                return this.interactions[i];
            }
        }
        return this.defaultInteraction;
    }
}
