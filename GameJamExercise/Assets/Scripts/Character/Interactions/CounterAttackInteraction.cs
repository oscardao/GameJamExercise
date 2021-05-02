﻿using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Interactions/Enemy Counter Attack")]
public class CounterAttackInteraction : BaseInteraction {

    [SerializeField]
    private BaseAction attackAction;
    [SerializeField]
    private Item armorToCheck;

    public override bool Evaluate(WorldTile tile, GameObject interacter) {
        IInventory inventory = interacter.GetComponent<IInventory>();

        return !inventory.HasItem(this.armorToCheck);
    }

    public override IEnumerator Perform(WorldTile tile, GameObject interacter) {
        IPositionable interacterPositionable = interacter.GetComponent<IPositionable>();
        this.attackAction.Perform(interacterPositionable.WorldTile, tile.ObjectOnTile);
        yield return new WaitForSeconds(this.attackAction.Duration);
    }
}
