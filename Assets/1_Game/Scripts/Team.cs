using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Team : MonoBehaviour {
    [Header("Parameters")]
    public bool setupPlayerOnJoin = false;

    [Header("Player References")]
    public Transform player1Pos;
    public Transform player2Pos;

    [Header("Car References")]
    public Transform carPath;
    public Transform carWaitPos;
    public CarEntity carPrefab;

    [Header("Char Graphics")]
    public GameObject[] meshes;

    [HideInInspector] public List<PlayerEntity> playerEntities = new List<PlayerEntity>();
    private int score;

    public void AddPlayer ( PlayerEntity playerEntity ) {
        if ( !playerEntities.Contains( playerEntity ) ) {
            playerEntities.Add( playerEntity );
            //GameObject mesh = null;

            switch ( playerEntities.Count ) {
                case 1:
                playerEntity.transform.position = player1Pos.position;
                //mesh = meshes[0];
                break;
                case 2:
                playerEntity.transform.position = player2Pos.position;
                //mesh = meshes[1];
                break;
            }

            playerEntity.team = this;
            //GameObject instMesh = Instantiate( mesh, playerEntity.graphics );

            if ( setupPlayerOnJoin )
                playerEntity.Setup();
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
            item.Setup();
            item.playerInput.SwitchCurrentActionMap( "Gameplay" );
        }
    }
}