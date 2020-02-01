﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TeamManager : MonoBehaviour {
    [Header("References")]
    public Team teamA;
    public Team teamB;

    [Header("Events")]
    public UnityEvent OnCanPlay;
    public UnityEvent OnCannotPlay;
    public UnityEvent OnGameStart;

    private bool canPlay;

    public void HandlePlayerJoining ( PlayerInput playerInput ) {
        switch ( playerInput.playerIndex ) {
            case 0:
            case 2:
            teamA.AddPlayer( playerInput.GetComponent<PlayerEntity>() );
            break;
            case 1:
            case 3:
            teamB.AddPlayer( playerInput.GetComponent<PlayerEntity>() );
            break;
        }
        CheckGameStartConditions();
    }

    public void HandlePlayerLeaving ( PlayerInput playerInput ) {
        switch ( playerInput.playerIndex ) {
            case 0:
            case 2:
            teamA.RemovePlayer( playerInput.GetComponent<PlayerEntity>() );
            break;
            case 1:
            case 3:
            teamB.RemovePlayer( playerInput.GetComponent<PlayerEntity>() );
            break;
        }
        CheckGameStartConditions();
    }

    public void HandleStartGameButtonClick () {
        if ( canPlay )
            StartGame();
    }

    private void CheckGameStartConditions () {
        int teamAPlayers = teamA.playerEntities.Count;
        int teambPlayers = teamB.playerEntities.Count;
        if ( teamAPlayers != 0 && teambPlayers != 0 && teamAPlayers == teambPlayers ) {
            //can play
            canPlay = true;
            OnCanPlay.Invoke();
        }
        else {
            //cannot play
            canPlay = false;
            OnCannotPlay.Invoke();
        }
    }

    private void StartGame () {
        OnGameStart.Invoke();
        for ( int i = 0; i < teamA.playerEntities.Count; i++ ) {
            teamA.playerEntities[i].Setup();
        }
        for ( int i = 0; i < teamB.playerEntities.Count; i++ ) {
            teamB.playerEntities[i].Setup();
        }
    }
}