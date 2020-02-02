using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Repairable : MonoBehaviour {
    [Header("Params")]
    public float repairTime;

    [Header("Events")]
    public UnityEvent OnRepair;
    public UnityEvent OnFail;

    [HideInInspector] public bool completed;
    private bool repairing;

    protected List<PlayerEntity> playersInRange = new List<PlayerEntity>();
    PlayerEntity currentPlayer;

    public System.Action OnComplete;
    public Team team;

    bool counting;
    float timer;

    //detect player entrance (more than one)
    private void OnTriggerEnter ( Collider other ) {
        if ( repairing )
            return;
        PlayerEntity p = other.GetComponentInParent<PlayerEntity>();
        if ( p ) {
            if ( p.data.team != team )
                return;
            if ( !playersInRange.Contains( p ) ) {
                playersInRange.Add( p );
                p.canRepair = true;
                p.OnRepairButtonDown += StartMinigame;
                p.OnRepairButtonUp += ResetMinigame;
            }
        }
    }

    private void OnTriggerExit ( Collider other ) {
        PlayerEntity p = other.GetComponentInParent<PlayerEntity>();
        if ( p ) {
            if ( p.data.team != team )
                return;
            if ( playersInRange.Contains( p ) ) {
                playersInRange.Remove( p );
                p.canRepair = false;
                p.OnRepairButtonDown -= StartMinigame;
                p.OnRepairButtonUp -= ResetMinigame;
            }
        }
    }

    protected void Complete ( PlayerEntity p ) {
        Debug.Log( name + " repaired." );
        repairing = false;
        completed = true;
        OnRepair.Invoke();
        counting = false;
        timer = 0;
        OnComplete?.Invoke();
        currentPlayer = p;
        currentPlayer.StopRepair();
    }

    public void ResetMinigame ( PlayerEntity p ) {
        if ( completed )
            return;
        Debug.Log( name + " failed." );
        repairing = false;
        completed = false;
        counting = false;
        timer = 0;
        OnFail.Invoke();
        currentPlayer = p;
        currentPlayer.StopRepair();
    }

    public void StartMinigame (PlayerEntity p) {
        Debug.Log( name + " started." );
        repairing = true;
        completed = false;
        counting = true;
        timer = 0;
        currentPlayer = p;
        currentPlayer.StartRepair();
    }

    private void Update () {
        if ( counting ) {
            timer += Time.deltaTime;
            if ( timer >= repairTime )
                Complete( currentPlayer );
        }
    }
}