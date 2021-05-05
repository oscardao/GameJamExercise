using OsukaCreative.Utility.Variables;
using System.Collections;
using UnityEngine;


public class StatController : MonoBehaviour {

    [SerializeField]
    private IntReference stat;

    public void IncrementStat() {
        this.stat.Value++;
    }

    public void ResetStat() {
        this.stat.Value = 0;
    }
}
