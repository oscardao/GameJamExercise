using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pathfinding")]
public class PathFinding : ScriptableObject {

    [SerializeField]
    private World world;
    [SerializeField, Min(0)]
    private int breakEveryIteration;

    public IEnumerator FindPath(WorldTile endTile, WorldTile startTile, Stack<WorldTile> outPath) {
        Stack<Node> queue = new Stack<Node>();
        Dictionary<WorldTile, Node> visitedTiles = new Dictionary<WorldTile, Node>();

        queue.Push(new Node(startTile));

        int iterations = 0;

        Debug.Log("Target: " + endTile.Position + "   Start: " + startTile.Position);

        while (queue.Count > 0) {
            iterations++;

            if (iterations % this.breakEveryIteration == 0) {
                yield return null;
            }

            Node tile = queue.Pop();
            if (EvaluateTile(tile, endTile, visitedTiles, queue)) {
                break;
            }
        }

        WorldTile currentPosition = endTile;

        while (visitedTiles.ContainsKey(currentPosition) && visitedTiles[currentPosition] != null) {
            if (currentPosition != null) {
                outPath.Push(currentPosition);
            }
            currentPosition = visitedTiles[currentPosition].Tile;

        }
    }

    private bool EvaluateTile(Node evaluatedNode, WorldTile target, Dictionary<WorldTile, Node> visitedTiles, Stack<Node> queue) {
        if (visitedTiles.ContainsKey(evaluatedNode.Tile)) {
            return false;
        }

        visitedTiles.Add(evaluatedNode.Tile, evaluatedNode.Link);
        if (evaluatedNode.Tile == target) {
            Debug.Log("End Found: EvaluatedNode=" + evaluatedNode.Tile.Position + "  EndNode=" + target.Position);
            return true;
        }

        List<WorldTile> neighbourTiles = this.world.GetNeighbourTiles(evaluatedNode.Tile.Position);
        for (int i = 0; i < neighbourTiles.Count; i++) {
            if (visitedTiles.ContainsKey(neighbourTiles[i])) {
                continue;
            }
            queue.Push(new Node(neighbourTiles[i], evaluatedNode));
        }
        return false;

    }

}
