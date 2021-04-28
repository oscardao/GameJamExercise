using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct WorldTile {

    public readonly List<GameObject> Objects;
    public Vector2Int Position;
    public GameObject TileObject;
    public Vector3 WorldPosition { get { return this.TileObject.transform.position; } }

    public WorldTile(Vector2Int position, GameObject tileObject) {
        this.Position = position;
        this.TileObject = tileObject;
        this.Objects = new List<GameObject>();
    }

    public void AddObject(GameObject gameObject) {
        if (!this.Objects.Contains(gameObject)) {
            this.Objects.Add(gameObject);
        }
    }

    public void RemoveObject(GameObject gameObject) {
        if (this.Objects.Contains(gameObject)) {
            this.Objects.Remove(gameObject);
        }
    }

    public bool Equals(WorldTile worldTile) {
        return this.Position.x == worldTile.Position.x && this.Position.y == worldTile.Position.y;
    }

}
