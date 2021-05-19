using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Switch Tiles")]
public class SwitchTileAction : BaseAction {

    [Header("Effects")]
    [SerializeField]
    private Flip flip;


    [SerializeField]
    private AudioClip[] sounds;

    public override void Perform(WorldTile tile, GameObject performer) {
        AudioManager.Instance.PlayAudio(this.sounds[Random.Range(0, this.sounds.Length)]);

        GameObject target = tile.ObjectOnTile;

        tile.StartCoroutine(this.flip.FlipObject(target.transform.position, 0, performer));
        tile.StartCoroutine(this.flip.FlipObject(performer.transform.position, 0, target));

        WorldTile tileA = target.GetComponent<IPositionable>().WorldTile;
        WorldTile tileB = performer.GetComponent<IPositionable>().WorldTile;

        tileA.ObjectOnTile = performer;
        tileB.ObjectOnTile = target;

        tile.StartCoroutine(MoveCO(tileA.WorldPosition, this.duration, performer));
        tile.StartCoroutine(MoveCO(tileB.WorldPosition, this.duration, target));

    }

    private IEnumerator MoveCO(Vector3 targetPosition, float duration, GameObject gameObject) {
        float time = 0;
        Transform transform = gameObject.transform;
        Vector3 startPosition = transform.position;

        while (time < duration) {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
