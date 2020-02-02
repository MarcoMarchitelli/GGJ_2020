using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public Stunner stunner;

    List<PlayerEntity> playersInRange = new List<PlayerEntity>();
    PlayerEntity currentPlayer;

    public Team team;

    private void OnTriggerEnter ( Collider other ) {
        if ( currentPlayer )
            return;
        PlayerEntity p = other.GetComponentInParent<PlayerEntity>();
        if ( p ) {
            if ( p.hasItem )
                return;
            if ( !playersInRange.Contains( p ) ) {
                playersInRange.Add( p );
                p.OnItemButtonPress += ItemButtonPressHandler;
            }
        }
    }

    private void ItemButtonPressHandler ( PlayerEntity obj ) {
        if ( !currentPlayer ) {
            currentPlayer = obj;
            currentPlayer.EquipItem( this );
        }
    }

    private void OnTriggerExit ( Collider other ) {
        if ( currentPlayer )
            return;
        PlayerEntity p = other.GetComponentInParent<PlayerEntity>();
        if ( p ) {
            if ( p.hasItem )
                return;
            if ( playersInRange.Contains( p ) ) {
                playersInRange.Remove( p );
                p.OnItemButtonPress -= ItemButtonPressHandler;
            }
        }
    }

    public void Use () {
        stunner.team = team;
        stunner.Activate();
    }
}