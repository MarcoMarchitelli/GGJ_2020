using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Deirin.EB;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerEntity : MonoBehaviour {
    [Header("Data")]
    public PlayerData data;

    [Header("References")]
    public Transform graphics;
    public PlayerInput playerInput;
    public PlayerInputHandlerBehaviour playerInputHandler;
    public PlayerRigidBodyBehaviour playerRigidBody;
    public Transform itemHolder;

    [Header("Events")]
    public UnityEvent OnStartGameButtonClick;
    public UnityVector2Event OnMoveInputVector;
    public UnityEvent OnPauseButtonClick;
    public UnityPlayerInputEvent OnStartMinigameButtonClick;
    public UnityEvent onRepairStart;
    public UnityEvent onRepairEnd;

    Animator animator;
    PlayerUI playerUI;

    public bool hasItem;

    public bool canRepair {
        set {
            playerUI.CanRepair( value );
        }
    }
    public System.Action<PlayerEntity> OnRepairButtonDown,OnRepairButtonUp;
    public System.Action<PlayerEntity> OnItemButtonPress;

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
        playerUI.StartRepair( true );
        repairing = true;
    }

    public void UpdateRepairPercent ( float percent ) {
        playerUI.Repairing( percent );
    }

    public void StopRepair () {
        onRepairEnd.Invoke();
        animator.SetTrigger( "RepairEnd" );
        playerUI.StartRepair( false );
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
        if ( repairing || stunned ) {
            OnMoveInputVector.Invoke( Vector2.zero );
            return;
        }
        OnMoveInputVector.Invoke( value.Get<Vector2>() );
    }

    public void OnPause () {
        OnPauseButtonClick.Invoke();
    }

    public void OnStartMinigame () {
        if ( stunned )
            return;
        OnStartMinigameButtonClick.Invoke( playerInput );
    }

    public void OnItem () {
        if ( stunned )
            return;
        OnItemButtonPress?.Invoke( this );
        if ( item ) {
            item.Use();
        }
    }
    #endregion

    Item item;
    public void EquipItem ( Item item ) {
        hasItem = true;
        item.team = data.team;
        this.item = item;
        item.transform.DOMove( itemHolder.position, .5f ).SetEase( Ease.OutCubic );
    }

    bool stunned;
    public void Stun () {
        animator.SetTrigger( "Stun" );
        stunned = true;
        if ( repairing )
            StopRepair();
    }
}