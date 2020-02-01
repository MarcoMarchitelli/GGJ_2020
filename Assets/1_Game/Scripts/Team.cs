using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour {
    [Header("References")]
    public Transform player1Pos;
    public Transform player2Pos;
    public Color color;

    [HideInInspector] public List<PlayerEntity> playerEntities = new List<PlayerEntity>();

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

            playerEntity.GetComponentInChildren<MeshRenderer>().material.color = color;
        }
    }

    public void RemovePlayer ( PlayerEntity playerEntity ) {
        if ( playerEntities.Contains( playerEntity ) ) {
            playerEntities.Remove( playerEntity );
            Destroy( playerEntity.gameObject );
        }
    }
}