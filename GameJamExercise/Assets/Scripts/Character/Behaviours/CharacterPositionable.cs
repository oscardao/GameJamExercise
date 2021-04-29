using System.Collections;
using UnityEngine;


public class CharacterPositionable : MonoBehaviour, IPositionable {

    private WorldTile position;
    public WorldTile Position {
        get { return this.position; }
        set { this.position = value; }
    }

}
