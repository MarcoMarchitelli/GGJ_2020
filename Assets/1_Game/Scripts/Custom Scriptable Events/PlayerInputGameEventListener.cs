using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputGameEventListener : MonoBehaviour {
    public PlayerInputGameEvent gameEvent;
    public UnityPlayerInputEvent response;

    private void OnEnable () {
        gameEvent.Subscribe( this );
    }

    private void OnDisable () {
        gameEvent.Unsubscribe( this );
    }

    public void OnInvoke ( PlayerInput playerInput ) {
        response.Invoke( playerInput );
    }
}