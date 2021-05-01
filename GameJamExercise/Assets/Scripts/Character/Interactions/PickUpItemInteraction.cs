using System.Collections;
using UnityEngine;


public class PickUpItemInteraction : BaseInteraction {

    public override bool Evaluate(WorldTile tile, GameObject interacter) {
        return true;
    }

    public override IEnumerator Perform(WorldTile tile, GameObject interacter) {
        throw new System.NotImplementedException();
    }

}
