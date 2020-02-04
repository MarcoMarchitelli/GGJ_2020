using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputManagerController : MonoBehaviour {
    public static PlayerInputManagerController Instance;
    
    public class Player {
        public string name;
        public Animator animator;
        public Team team;
    }

    [Header("References")]
    public PlayerInputManager playerInputManager;

    [Header( "Events" )]
    public UnityPlayerInputEvent OnPlayerJoined;
    public UnityPlayerInputEvent OnPlayerLeft;

    public void Setup () {
        if ( Instance == null )
            Instance = this;
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