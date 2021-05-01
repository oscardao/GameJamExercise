using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IFlipable {

    public abstract Transform FlipableTransform {
        get;
    }

    public abstract bool IsFlipped {
        get;
        set;
    }

}

