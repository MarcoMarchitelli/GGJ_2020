using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Team : MonoBehaviour {
    [Header("Player Data")]
    public PlayerData[] players;

    [Header("Car References")]
    public Transform carPath;
    public Transform carWaitPos;
    public CarEntity carPrefab;

    [HideInInspector] public List<PlayerEntity> playerEntities = new List<PlayerEntity>();
    private int score;

    public void AddPlayer ( PlayerEntity playerEntity ) {
        if ( !playerEntities.Contains( playerEntity ) ) {
            playerEntities.Add( playerEntity );
            //GameObject mesh = null;

            switch ( playerEntities.Count ) {
                case 1:
                playerEntity.Setup( players[0] );             
                break;
                case 2:
                playerEntity.Setup( players[1] );
                break;
            }
        }
    }

    public void RemovePlayer ( PlayerEntity playerEntity ) {
        if ( playerEntities.Contains( playerEntity ) ) {
            playerEntities.Remove( playerEntity );
            Destroy( playerEntity.gameObject );
        }
    }

    public void OnCarRepair ( Team team ) {
        if ( team == this )
            score++;
    }

    public void SetupPlayers () {
        foreach ( var item in playerEntities ) {
            item.playerInput.SwitchCurrentActionMap( "Gameplay" );
        }
    }
}

[System.Serializable]
public class PlayerData {
    public string name;
    public Animator animator;
    public Transform spawnPos;
    public Sprite icon;
    public Team team;
    public PlayerUI playerUIPrefab;
}