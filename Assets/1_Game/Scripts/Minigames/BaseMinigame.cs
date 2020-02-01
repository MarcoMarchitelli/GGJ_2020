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
        CustomComplete();
        currentPlayer.SwitchCurrentActionMap( "Gameplay" );
        completed = true;
        print( name + " minigame completed." );
        OnMinigameComplete.Invoke();
    }

    protected void ResetMinigame () {
        CustomReset();
        currentPlayer.SwitchCurrentActionMap( "Gameplay" );
        print( name + " minigame reset." );
        OnMinigameReset.Invoke();
    }

    public void StartMinigame () {
        CustomStart();
        print( name + " minigame started." );
        OnMinigameStart.Invoke();
    }

    public void StartMinigameButtonHandler ( PlayerInput playerInput ) {
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