using OsukaCreative.Utility.Variables;
using System.Collections;
using UnityEngine;


public class GamePauser : MonoBehaviour {

    [SerializeField]
    private BoolReference isGamePaused;

    public void SetValue(bool value) {
        this.isGamePaused.Value = value;
    }

}
