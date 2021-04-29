using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldTile : MonoBehaviour {

    private GameObject objectOnTile;
    public GameObject ObjectOnTile {
        get { return this.objectOnTile; }
        set {
            this.objectOnTile = value;
            if (value == null) return;
            value.GetComponent<IPositionable>().Position = this;
            value.transform.SetParent(transform);
        }
    }

    public bool IsEmpty {
        get { return this.objectOnTile == null; }
    }

    public Vector2Int Position;
    public Vector3 WorldPosition { get { return transform.position; } }

}
