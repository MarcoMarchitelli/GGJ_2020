using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputManagerController : MonoBehaviour {
    [Header("References")]
    public PlayerInputManager playerInputManager;

    [Header( "Events" )]
    public UnityPlayerInputEvent OnPlayerJoined;
    public UnityPlayerInputEvent OnPlayerLeft;

    public void Setup () {
        playerInputManager.EnableJoining();
    }

    public void LeftHandler ( PlayerInput obj ) {
        OnPlayerLeft.Invoke( obj );
    }

    public void JoinHandler ( PlayerInput obj ) {
        OnPlayerJoined.Invoke( obj );
    }
}

[System.Serializable]
public class UnityPlayerEntityEvent : UnityEvent<PlayerEntity> { }

[System.Serializable]
public class UnityPlayerInputEvent : UnityEvent<PlayerInput> { }