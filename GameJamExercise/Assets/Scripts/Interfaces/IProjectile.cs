﻿using System.Collections;
using UnityEngine;


public interface IProjectile {

    public abstract float Speed {
        get;
    }

    public abstract IEnumerator Activate(Vector2 endPoint);

}