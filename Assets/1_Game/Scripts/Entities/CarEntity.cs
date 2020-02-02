using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Deirin.EB;

public class CarEntity : MonoBehaviour {
    [Header("References")]
    public Team team;

    [Header("Events")]
    public UnityTeamEvent OnRepair;

    public void Repair () {
        OnRepair.Invoke( team );
    }
}

[System.Serializable]
public class UnityTeamEvent : UnityEvent<Team> {}