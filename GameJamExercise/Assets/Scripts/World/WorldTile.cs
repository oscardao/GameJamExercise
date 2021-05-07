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
            value.GetComponent<IPositionable>().WorldTile = this;
            value.transform.SetParent(transform);
        }
    }

    public bool IsEmpty {
        get { return this.objectOnTile == null; }
    }

    public Vector2Int Position;
    public Vector3 WorldPosition { get { return transform.position; } }

    public Sprite Sprite {
        get { return this.spriteRenderer.sprite; }
        set { this.spriteRenderer.sprite = value; }
    }
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override string ToString() {
        return $"Tile: [Position={this.Position}, WorldPosition={WorldPosition}, ObjectOnTile={this.objectOnTile}, IsEmpty={this.IsEmpty}]";
    }
}
