using System.Collections;
using UnityEngine;


public interface IPositionable {

    public abstract WorldTile Position {
        get;
        set;
    }

}
