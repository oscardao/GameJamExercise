using System.Collections;
using UnityEngine;


public class UIPage : MonoBehaviour {

    [HideInInspector]
    public GameObject previousPage;
    [HideInInspector]
    public bool IsOverlaying;

    [Header("Animations")]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string onHideTrigger = "onHide";
    [SerializeField]
    private string onExposeTrigger = "onExpose";

    public void OpenPage(GameObject pageObject) {
        Hide(false);

        GameObject newPageObject = Instantiate(pageObject, transform.parent);
        UIPage newPage = newPageObject.GetComponent<UIPage>();
        newPage.previousPage = gameObject;
        newPage.Expose();
    }

    public void OpenPageAsOverlay(GameObject pageObject) {
        EnableFunctions(false);

        GameObject newPageObject = Instantiate(pageObject, transform.parent);
        UIPage newPage = newPageObject.GetComponent<UIPage>();
        newPage.previousPage = gameObject;
        newPage.IsOverlaying = true;
        newPage.Expose();
    }

    public void Close() {
        Hide(true);

        if (this.previousPage != null) {
            UIPage previousPage = this.previousPage.GetComponent<UIPage>();

            if (this.IsOverlaying) {
                previousPage.EnableFunctions(true);
            } else {
                previousPage.Expose();
            }
        }
    }

    public void Expose() {
        gameObject.SetActive(true);
        this.animator.SetTrigger(this.onExposeTrigger);
    }

    private void Hide(bool destroyOnClose) {
        EnableFunctions(false);
        this.animator.SetTrigger(this.onHideTrigger);
        if (destroyOnClose) StartCoroutine(DestroyCoroutine(3f));
    }

    public void EnableFunctions() {
        EnableFunctions(true);
    }

    public void EnableFunctions(bool value) {
        IDeactivateable[] elements = GetComponentsInChildren<IDeactivateable>(true);
        for (int i = 0; i < elements.Length; i++) {

            if (value) {
                elements[i].OnActivate();
            } else {
                elements[i].OnDeactivate();
            }
        }
    }

    private IEnumerator DestroyCoroutine(float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

}
