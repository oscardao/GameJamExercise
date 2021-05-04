using System.Collections;
using TMPro;
using UnityEngine;


public class TextAlphaPulse : MonoBehaviour {

    private TextMeshProUGUI text;
    [Space(10)]
    [SerializeField, Min(0)]
    private int min = 0;
    [SerializeField, Min(255)]
    private int max = 255;
    [SerializeField]
    private float rate = 1;
    [SerializeField]
    private float amplitude = 1;
    private float degree;

    public void Awake() {
        this.degree = 90;
        this.text = GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable() {
        this.degree = 90;
    }

    private void Update() {

        float OldRange = this.amplitude - -this.amplitude;
        int NewRange = (this.max - this.min);
        this.degree += Time.deltaTime * this.rate;
        if (this.degree > 360) {
            this.degree = 0;
        }

        float y = this.amplitude * Mathf.Sin(this.degree);
        float alpha = (((y - -1) * NewRange) / OldRange) + this.min;
        this.text.alpha = alpha / 255;

    }
}
