using System.Collections;
using TMPro;
using UnityEngine;


public class TextColorSetter : MonoBehaviour {

    [SerializeField]
    private Color color;
    [SerializeField]
    private TextMeshProUGUI[] texts;


    private void Update() {

        for (int i = 0; i < texts.Length; i++) {
            texts[i].faceColor = this.color;
            // texts[i].color = this.color;
        }
    }


}
