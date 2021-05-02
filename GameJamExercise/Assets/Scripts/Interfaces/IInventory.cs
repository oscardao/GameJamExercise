using System.Collections;
using UnityEngine;


public interface IInventory {

    public abstract bool IsEmpty {
        get;
    }

    public abstract void AddItem(Item item);

    public abstract bool HasItem(Item item);

    public abstract void RemoveItem(Item item);

}
