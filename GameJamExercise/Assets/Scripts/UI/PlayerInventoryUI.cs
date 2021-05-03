using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour {

    [SerializeField]
    private Image image;

    [SerializeField]
    private ItemVariable item;

    [SerializeField]
    private Sprite noneItem;

    private void Start() {
        UpdateDisplay();
    }

    private void OnEnable() {
        this.item.onChange.AddListener(UpdateDisplay);
    }

    private void OnDisable() {
        this.item.onChange.RemoveListener(UpdateDisplay);
    }

    public void UpdateDisplay() {
        if (this.item.Value == null) {
            this.image.sprite = this.noneItem;
        } else {
            this.image.sprite = this.item.Value.Sprite;
        }

    }

}
