using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Deirin.EB;
using UnityEngine.InputSystem;

public class PlayerEntity : MonoBehaviour {
    [Header("Data")]
    public PlayerData data;

    [Header("References")]
    public Transform graphics;
    public PlayerInput playerInput;
    public PlayerInputHandlerBehaviour playerInputHandler;
    public PlayerRigidBodyBehaviour playerRigidBody;

    [Header("Events")]
    public UnityEvent OnStartGameButtonClick;
    public UnityVector2Event OnMoveInputVector;
    public UnityEvent OnPauseButtonClick;
    public UnityPlayerInputEvent OnStartMinigameButtonClick;
    public UnityEvent onRepairStart;
    public UnityEvent onRepairEnd;

    Animator animator;
    PlayerUI playerUI;

    public bool canRepair {
        set {
            playerUI.CanRepair( value );
        }
    }
    public System.Action<PlayerEntity> OnRepairButtonDown,OnRepairButtonUp;

    public void Setup ( PlayerData data ) {
        this.data = data;
        transform.position = data.spawnPos.position;
        animator = Instantiate( data.animator, graphics );
        playerRigidBody.OnMove.AddListener( HandleMovementStart );
        playerRigidBody.OnStop.AddListener( HandleMovementStop );
        playerUI = Instantiate( data.playerUIPrefab );
        playerUI.Setup( this );
    }

    private void OnDisable () {
        playerRigidBody.OnMove.RemoveListener( HandleMovementStart );
        playerRigidBody.OnStop.RemoveListener( HandleMovementStop );
    }

    private void HandleMovementStart () {
        animator.SetTrigger( "Run" );
    }

    private void HandleMovementStop () {
        animator.SetTrigger( "Idle" );
    }

    private void OnRepair ( InputValue value ) {
        float val = value.Get<float>();
        if ( val == 1 ) {
            OnRepairButtonDown?.Invoke( this );
        }
        else {
            OnRepairButtonUp?.Invoke( this );
        }
    }
    private bool repairing;
    public void StartRepair () {
        onRepairStart.Invoke();
        animator.SetTrigger( "RepairStart" );
        repairing = true;
    }

    public void StopRepair () {
        onRepairEnd.Invoke();
        animator.SetTrigger( "RepairEnd" );
        repairing = false;
    }

    private void Update () {
        print( playerInput.user.id + " " + playerInput.currentActionMap );
    }

    #region Input Handlers
    public void OnStartGame () {
        OnStartGameButtonClick.Invoke();
    }

    public void OnMove ( InputValue value ) {
        if ( repairing ) {
            OnMoveInputVector.Invoke( Vector2.zero );
            return;
        }
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