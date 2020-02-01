using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Deirin.EB;

public abstract class BaseMinigame : MonoBehaviour {
    [HideInInspector] public bool completed;

    protected List<PlayerEntity> playersInRange;
    protected PlayerEntity currentPlayer;

    //detect player entrance (more than one)
    private void OnTriggerEnter ( Collider other ) {
        PlayerEntity p = other.GetComponent<PlayerEntity>();
        if ( p ) {
            if ( !playersInRange.Contains( p ) )
                playersInRange.Add( p );
        }
    }

    private void OnTriggerExit ( Collider other ) {
        PlayerEntity p = other.GetComponent<PlayerEntity>();
        if ( p ) {
            if ( playersInRange.Contains( p ) )
                playersInRange.Remove( p );
        }
    }

    //detect specific input to start minigame => start minigame

    //see if fail/win => exit minigame
    //if win set completed
    protected void Complete () {
        CustomComplete();
        completed = true;
    }

    protected void ResetMinigame () {
        CustomReset();
    }

    public void StartMinigame () {
        CustomStart();
    }

    protected virtual void CustomComplete () {

    }

    protected virtual void CustomReset () {

    }

    protected virtual void CustomStart () {

    }
}