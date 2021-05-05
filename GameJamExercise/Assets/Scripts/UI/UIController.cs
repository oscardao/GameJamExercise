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
        OpenPage(this.startPagePrefab);
    }

    public void OpenPage(GameObject page) {
        GameObject newPage = Instantiate(page, transform);
        UIPage uiPage = newPage.GetComponent<UIPage>();
        uiPage.Expose();
    }

}
