using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunner : MonoBehaviour {
    public float time;
    public Collider hitCollider;

    public Team team;
    bool active;

    public void Activate () {
        hitCollider.enabled = true;
        counting = true;
        timer = 0;
        active = true;
    }

    private void OnTriggerEnter ( Collider other ) {
        if ( !active )
            return;
        PlayerEntity p = other.GetComponent<PlayerEntity>();
        if ( p && p.data.team != team ) {
            p.Stun();
        }
    }

    bool counting;
    float timer;
    private void Update () {
        if ( counting ) {
            timer += Time.deltaTime;
            if ( timer >= time ) {
                counting = false;
                timer = 0;
                active = false;
                hitCollider.enabled = false;
            }
        }
    }
}