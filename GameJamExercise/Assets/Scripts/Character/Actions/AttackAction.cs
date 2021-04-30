using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Attack")]
public class AttackAction : BaseAction {

    [SerializeField]
    private Attack attack = new Attack();
    private Flip flip = new Flip();

    public override void Perform(WorldTile tile, GameObject gameObject) {
        GameObject objectOnTile = tile.ObjectOnTile;

        tile.StartCoroutine(this.flip.FlipObject(objectOnTile.transform.position, 0, gameObject));
        tile.StartCoroutine(this.flip.FlipObject(gameObject.transform.position, 0, objectOnTile));

        tile.StartCoroutine(this.attack.AttackTarget(objectOnTile, this.Duration, gameObject));

    }

}
