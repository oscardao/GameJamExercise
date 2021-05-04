using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PressKeyPrompt : MonoBehaviour, IDeactivateable {

    private bool isActivated;

    [SerializeField]
    private KeyCode listenForKey;
    [SerializeField]
    private UnityEvent onKeyPressed;

    private void Update() {
        if (this.isActivated && Input.GetKeyDown(this.listenForKey)) {
            this.onKeyPressed.Invoke();
        }
    }

    public void OnActivate() {
        this.isActivated = true;
    }

    public void OnDeactivate() {
        this.isActivated = false;
    }
}
