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
    public UnityPlayerInputEvent onPlayerJoined;
    public UnityPlayerInputEvent onPlayerLeft;

    public void Setup () {
        if ( Instance == null )
            Instance = this;
        playerInputManager.EnableJoining();
    }

    #region Player Input Manager Messages
    private void OnPlayerJoined ( PlayerInput player ) {
        onPlayerJoined.Invoke( player );
    }

    private void OnPlayerLeft ( PlayerInput player ) {
        onPlayerLeft.Invoke( player );
    }
    #endregion
}

[System.Serializable]
public class UnityPlayerEntityEvent : UnityEvent<PlayerEntity> { }

[System.Serializable]
public class UnityPlayerInputEvent : UnityEvent<PlayerInput> { }