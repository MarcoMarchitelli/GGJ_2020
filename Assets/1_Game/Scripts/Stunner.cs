using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunner : MonoBehaviour {
    public float time;
    public Collider hitCollider;

    public Team team;

    public void Activate () {
        counting = true;
        timer = 0;
    }

    private void OnTriggerEnter ( Collider other ) {
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
            }
        }
    }
}