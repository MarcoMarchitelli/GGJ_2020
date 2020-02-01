using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlayerEntityGameEvent : ScriptableObject {
    private List<PlayerEntityGameEventListener> listeners = new List<PlayerEntityGameEventListener>();

    public void Subscribe ( PlayerEntityGameEventListener listener ) {
        listeners.Add( listener );
    }

    public void Unsubscribe ( PlayerEntityGameEventListener listener ) {
        listeners.Remove( listener );
    }

    public void Invoke ( PlayerEntity playerEntity ) {
        for ( int i = 0; i < listeners.Count; i++ ) {
            listeners[i].OnInvoke( playerEntity );
        }
    }
}