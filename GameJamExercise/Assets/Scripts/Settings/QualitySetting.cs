using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace OsukaCreative.TheDayIRice.UI {

    public class QualitySetting : MonoBehaviour {

        [SerializeField]
        private TMP_Dropdown dropDown;

        private string[] qualities;

        private void Start() {
            this.dropDown.ClearOptions();
            this.qualities = QualitySettings.names;
            this.dropDown.AddOptions(new List<string>(this.qualities));
            this.dropDown.value = QualitySettings.GetQualityLevel();
            this.dropDown.RefreshShownValue();
            this.dropDown.onValueChanged.AddListener(SetQuality);

        }

        private void OnEnable() {
            this.dropDown.interactable = true;
        }

        private void OnDisable() {
            this.dropDown.interactable = false;
        }

        public void SetQuality(int index) {
            QualitySettings.SetQualityLevel(index);

        }

    }

}