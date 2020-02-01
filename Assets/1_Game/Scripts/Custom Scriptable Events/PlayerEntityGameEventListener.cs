using UnityEngine;
using UnityEngine.Events;

public class PlayerEntityGameEventListener : MonoBehaviour {
    public PlayerEntityGameEvent gameEvent;
    public UnityPlayerEntityEvent response;

    private void OnEnable () {
        gameEvent.Subscribe( this );
    }

    private void OnDisable () {
        gameEvent.Unsubscribe( this );
    }

    public void OnInvoke ( PlayerEntity playerEntity ) {
        response.Invoke( playerEntity );
    }
}