using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public abstract class BaseMinigame : MonoBehaviour {
    [Header("Events")]
    public UnityEvent OnMinigameStart;
    public UnityEvent OnMinigameComplete;
    public UnityEvent OnMinigameReset;

    [HideInInspector] public bool completed;
    private bool inMinigame;

    protected List<PlayerInput> playersInRange = new List<PlayerInput>();
    protected PlayerInput currentPlayer;

    //detect player entrance (more than one)
    private void OnTriggerEnter ( Collider other ) {
        PlayerInput p = other.GetComponentInParent<PlayerInput>();
        if ( p ) {
            if ( !playersInRange.Contains( p ) ) {
                playersInRange.Add( p );
            }
        }
    }

    private void OnTriggerExit ( Collider other ) {
        PlayerInput p = other.GetComponentInParent<PlayerInput>();
        if ( p ) {
            if ( playersInRange.Contains( p ) ) {
                playersInRange.Remove( p );
            }
        }
    }

    //detect specific input to start minigame => start minigame

    //see if fail/win => exit minigame
    //if win set completed
    protected void Complete () {
        Debug.Log( name + " minigame completed." );
        inMinigame = false;
        CustomComplete();
        currentPlayer.SwitchCurrentActionMap( "Gameplay" );
        completed = true;
        OnMinigameComplete.Invoke();
    }

    public void ResetMinigame () {
        Debug.Log( name + " minigame reset." );
        inMinigame = false;
        CustomReset();
        currentPlayer.SwitchCurrentActionMap( "Gameplay" );
        OnMinigameReset.Invoke();
    }

    public void StartMinigame () {
        Debug.Log( name + " minigame started." );
        inMinigame = true;
        CustomStart();
        OnMinigameStart.Invoke();
    }

    public void StartMinigameButtonHandler ( PlayerInput playerInput ) {
        if ( inMinigame )
            return;
        if ( playersInRange.Contains( playerInput ) ) {
            currentPlayer = playerInput;
            StartMinigame();
        }
    }

    protected virtual void CustomComplete () {

    }

    protected virtual void CustomReset () {

    }

    protected virtual void CustomStart () {

    }
}