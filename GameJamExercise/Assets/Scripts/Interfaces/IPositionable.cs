using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public interface IPositionable {

    public abstract WorldTile WorldTile {
        get;
        set;
    }

    public abstract UnityEvent OnReposition {
        get;
    }

}
