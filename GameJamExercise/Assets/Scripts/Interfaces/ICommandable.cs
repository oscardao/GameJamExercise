using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface ICommandable {

    public abstract bool IsActive {
        get;
        set;
    }

    public abstract void TakeTurn();

}

