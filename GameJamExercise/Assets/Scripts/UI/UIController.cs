using System.Collections;
using UnityEngine;


public class UIController : MonoBehaviour {
    [SerializeField]
    private GameObject startPagePrefab;
    [SerializeField]
    private float delayBeforeStart;

    private void Start() {
        StartCoroutine(StartCO());
    }

    private IEnumerator StartCO() {
        yield return new WaitForSeconds(this.delayBeforeStart);
        GameObject startPage = Instantiate(this.startPagePrefab, transform);
        UIPage uiPage = startPage.GetComponent<UIPage>();
        uiPage.Expose();
    }

}
