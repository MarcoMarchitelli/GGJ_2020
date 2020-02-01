using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TeamManager : MonoBehaviour {
    [Header("References")]
    public Team teamA;
    public Team teamB;

    [Header("Events")]
    public UnityEvent OnCanPlay;
    public UnityEvent OnCannotPlay;

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

    private void CheckGameStartConditions () {
        int teamAPlayers = teamA.playerEntities.Count;
        int teambPlayers = teamB.playerEntities.Count;
        if ( teamAPlayers != 0 && teambPlayers != 0 && teamAPlayers == teambPlayers ) {
            //can play
            OnCanPlay.Invoke();
        }
        else {
            //cannot play
            OnCannotPlay.Invoke();
        }
    }
}