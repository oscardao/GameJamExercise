using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldTile : MonoBehaviour {

    public readonly List<GameObject> Objects = new List<GameObject>();
    public Vector2Int Position;
    public Vector3 WorldPosition { get { return transform.position; } }

    public void AddObject(GameObject gameObject) {
        if (!this.Objects.Contains(gameObject)) {
            gameObject.GetComponent<IPositionable>().Position = this.Position;
            this.Objects.Add(gameObject);
            gameObject.transform.SetParent(transform);
        }
    }

    public void RemoveObject(GameObject gameObject) {
        if (this.Objects.Contains(gameObject)) {
            this.Objects.Remove(gameObject);
            gameObject.transform.SetParent(null);
        }
    }

}
