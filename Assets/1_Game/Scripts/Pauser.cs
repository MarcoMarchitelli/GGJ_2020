using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pauser : MonoBehaviour {
    [Header("Events")]
    public UnityEvent OnPause;
    public UnityEvent OnUnpause;

    private bool paused;

    public void TogglePause () {
        paused = !paused;
        if ( paused )
            OnPause.Invoke();
        else
            OnUnpause.Invoke();
    }
}