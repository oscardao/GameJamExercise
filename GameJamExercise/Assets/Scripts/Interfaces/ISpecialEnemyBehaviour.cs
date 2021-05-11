using System.Collections;
using UnityEngine;


public interface ISpecialEnemyBehaviour {

    public abstract IEnumerator Prepare();

    public abstract IEnumerator Perform();

}
