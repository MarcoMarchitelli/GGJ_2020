using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TeamGameEvent : ScriptableObject {
    private List<TeamGameEventListener> listeners = new List<TeamGameEventListener>();

    public void Subscribe ( TeamGameEventListener listener ) {
        listeners.Add( listener );
    }

    public void Unsubscribe ( TeamGameEventListener listener ) {
        listeners.Remove( listener );
    }

    public void Invoke ( Team team ) {
        for ( int i = 0; i < listeners.Count; i++ ) {
            listeners[i].OnInvoke( team );
        }
    }
}