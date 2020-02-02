using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Deirin.EB;
using UnityEngine.InputSystem;

public class PlayerEntity : MonoBehaviour {
    [Header("References")]
    public Team team;
    public Transform graphics;
    public PlayerInput playerInput;
    public PlayerInputHandlerBehaviour playerInputHandler;
    public PlayerRigidBodyBehaviour playerRigidBody;

    [Header("Events")]
    public UnityEvent OnStartGameButtonClick;
    public UnityVector2Event OnMoveInputVector;
    public UnityEvent OnPauseButtonClick;
    public UnityPlayerInputEvent OnStartMinigameButtonClick;

    private void Update () {
        print( playerInput.user.id + " " + playerInput.currentActionMap );
    }

    #region Input Handlers
    public void OnStartGame () {
        OnStartGameButtonClick.Invoke();
    }

    public void OnMove ( InputValue value ) {
        OnMoveInputVector.Invoke( value.Get<Vector2>() );
    }

    public void OnPause () {
        OnPauseButtonClick.Invoke();
    }

    public void OnStartMinigame () {
        OnStartMinigameButtonClick.Invoke( playerInput );
    }
    #endregion
}