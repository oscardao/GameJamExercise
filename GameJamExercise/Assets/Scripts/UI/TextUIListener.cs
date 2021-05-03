using OsukaCreative.Utility.Variables;
using System.Collections;
using TMPro;
using UnityEngine;


public class TextUIListener : MonoBehaviour {

    [SerializeField]
    private IntVariable variable;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private string preText;

    private void Start() {
        UpdateDisplay();
    }

    private void OnEnable() {
        this.variable.onChange.AddListener(UpdateDisplay);
    }

    private void OnDisable() {
        this.variable.onChange.RemoveListener(UpdateDisplay);
    }

    public void UpdateDisplay() {
        this.text.text = this.preText + this.variable.Value;
    }

}
