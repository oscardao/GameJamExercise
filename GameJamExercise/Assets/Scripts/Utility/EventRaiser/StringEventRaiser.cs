using OsukaCreative.Utility.GameEvent;
using System.Collections;
using UnityEngine;


public class StringEventRaiser : MonoBehaviour {
    [SerializeField]
    private StringEvent eventToRaise;

    [SerializeField]
    private string parameter;

    public void Raise() {
        this.eventToRaise.Raise(this.parameter);
    }

    public void Raise(string parameter) {
        this.eventToRaise.Raise(parameter);
    }
}
