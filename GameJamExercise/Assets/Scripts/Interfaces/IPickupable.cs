using System.Collections;
using UnityEngine;


public interface IPickupable {

    public abstract string promptDescription {
        get;
    }

    public abstract void OnPickup(GameObject interacter);

}
