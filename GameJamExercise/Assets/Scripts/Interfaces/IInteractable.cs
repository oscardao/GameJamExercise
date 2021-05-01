
using System.Collections;
using UnityEngine;

public interface IInteractable {

    public abstract BaseInteraction GetInteraction(WorldTile tile, GameObject interacter);

}

