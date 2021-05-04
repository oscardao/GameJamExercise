using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Deactivateable : MonoBehaviour, IDeactivateable {

    [SerializeField]
    private UnityEvent onActivate;
    [SerializeField]
    private UnityEvent onDeactivate;

    public void OnActivate() {
        this.onActivate.Invoke();
    }

    public void OnDeactivate() {
        this.onDeactivate.Invoke();
    }
}
