using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    [SerializeField]
    private GameObject panelPrefab;
    [SerializeField]
    private GameObject panel;
    private Image image;

    [SerializeField]
    private Color fadeOut;
    [SerializeField]
    private Color fadeIn;
    [SerializeField]
    private float fadeDuration;

    private void Awake() {
        this.panel.SetActive(true);
        this.image = this.panel.GetComponent<Image>();
    }

    // Use this for initialization
    void Start() {
        StartCoroutine(StartCO());
    }

    public void ChangeScene(string sceneName) {
        StartCoroutine(ChangeCO(sceneName));
    }

    private IEnumerator ChangeCO(string sceneName) {
        this.panel = Instantiate(this.panelPrefab, transform);
        this.image = this.panel.GetComponent<Image>();
        this.image.color = this.fadeIn;
        yield return Fade(this.fadeOut);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator StartCO() {
        this.panel.SetActive(true);
        yield return Fade(this.fadeIn);
        Destroy(this.panel);
    }

    private IEnumerator Fade(Color endValue) {
        float time = 0;
        Color startValue = this.image.color;

        while (time < this.fadeDuration) {
            this.image.color = Color.Lerp(startValue, endValue, time / this.fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        this.image.color = endValue;
    }


}
