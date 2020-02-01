using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Team : MonoBehaviour {
    [Header("Parameters")]
    public bool setupPlayerOnJoin = false;
    public Color color;

    [Header("Player References")]
    public Transform player1Pos;
    public Transform player2Pos;

    [Header("Car References")]
    public Transform carPath;
    public Transform carWaitPos;
    public CarEntity carPrefab;

    [HideInInspector] public List<PlayerEntity> playerEntities = new List<PlayerEntity>();
    private int score;

    public void AddPlayer ( PlayerEntity playerEntity ) {
        if ( !playerEntities.Contains( playerEntity ) ) {
            playerEntities.Add( playerEntity );

            switch ( playerEntities.Count ) {
                case 1:
                playerEntity.transform.position = player1Pos.position;
                break;
                case 2:
                playerEntity.transform.position = player2Pos.position;
                break;
            }

            playerEntity.team = this;
            playerEntity.GetComponentInChildren<MeshRenderer>().material.color = color;

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
}