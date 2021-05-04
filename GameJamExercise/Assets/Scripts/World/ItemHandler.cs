using OsukaCreative.Utility.Sets;
using OsukaCreative.Utility.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemHandler : MonoBehaviour {

    [Header("Properties")]
    [SerializeField]
    private IntReference level;
    [SerializeField]
    private int numberOfItemsOffset;
    [SerializeField]
    private ItemSet itemsInWorld;
    [SerializeField]
    private World world;

    [Header("Items")]
    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private Item[] items;
    [SerializeField]
    private Item sword;

    private ICommandable commandable;

    private void Awake() {
        this.commandable = GetComponent<ICommandable>();
    }

    public void ClearItemsInWorld() {
        this.itemsInWorld.Clear();
    }

    public void TakeTurn() {
        PlaceItems();
        this.commandable.TurnHandler.NextTurn();
    }

    public void PlaceItems() {
        List<WorldTile> emptyTiles = this.world.EmptyTiles;
        int itemsToPlace = this.level.Value + this.numberOfItemsOffset - this.itemsInWorld.Length;

        for (int i = 0; i < itemsToPlace; i++) {
            if (emptyTiles.Count <= 0) return;

            WorldTile tile = emptyTiles[Random.Range(0, emptyTiles.Count)];
            emptyTiles.Remove(tile);

            if (this.itemsInWorld.Contains(this.sword)) {
                PlaceItem(tile, this.items[Random.Range(0, this.items.Length)]);
            } else {
                PlaceItem(tile, this.sword);
            }

        }

    }

    private void PlaceItem(WorldTile tile, Item item) {
        GameObject itemObject = Instantiate(this.itemPrefab);
        tile.ObjectOnTile = itemObject;
        itemObject.transform.position = tile.WorldPosition;
        ItemBehaviour itemBehaviour = itemObject.GetComponent<ItemBehaviour>();
        itemBehaviour.Setup(item);
    }

}
