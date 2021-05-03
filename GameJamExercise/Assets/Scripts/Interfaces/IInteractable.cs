
using System.Collections;
using UnityEngine;

public interface IInteractable {

    public Transform SymbolLocation {
        get;
    }

    public abstract BaseInteraction GetInteraction(WorldTile tile, GameObject interacter);

}

