using System.Collections;
using UnityEngine;

public class Node {

    public WorldTile Tile;
    public Node Link;
    public bool HasLink {
        get { return this.Link == null; }
    }

    public Node(WorldTile Position, Node Link) {
        this.Tile = Position;
        this.Link = Link;
    }

    public Node(WorldTile Position) {
        this.Tile = Position;
    }

}