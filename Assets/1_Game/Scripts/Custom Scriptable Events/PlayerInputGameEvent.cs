using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu()]
public class PlayerInputGameEvent : ScriptableObject {
    private List<PlayerInputGameEventListener> listeners = new List<PlayerInputGameEventListener>();

    public void Subscribe ( PlayerInputGameEventListener listener ) {
        listeners.Add( listener );
    }

    public void Unsubscribe ( PlayerInputGameEventListener listener ) {
        listeners.Remove( listener );
    }

    public void Invoke ( PlayerInput playerInput ) {
        for ( int i = 0; i < listeners.Count; i++ ) {
            listeners[i].OnInvoke( playerInput );
        }
    }
}