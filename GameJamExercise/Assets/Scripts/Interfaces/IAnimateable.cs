using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IAnimateable {

    public abstract Transform AnimateableTransform {
        get;
    }

    public abstract SpriteRenderer SpriteRenderer {
        get;
    }

    public abstract void SetTrigger(String trigger);

}

