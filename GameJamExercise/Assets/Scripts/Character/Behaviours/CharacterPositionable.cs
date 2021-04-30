﻿using System.Collections;
using UnityEngine;


public class CharacterPositionable : MonoBehaviour, IPositionable {

    private WorldTile position;
    public WorldTile WorldTile {
        get { return this.position; }
        set { this.position = value; }
    }

}
