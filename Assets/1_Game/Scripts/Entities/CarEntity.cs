using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Deirin.EB;

public class CarEntity : MonoBehaviour {
    [Header("Parameters")]
    public float minSpeed = 10;
    public float maxSpeed = 20;

    [Header("Car Meshes")]
    public GameObject[] carMeshes;

    [Header("Minigames Prefabs")]
    public Repairable[] minigamesPrefabs;

    [Header("Minigame possible positions")]
    public Transform[] minigamePossiblePositions;

    [Header("References")]
    public Transform graphics;
    public Team team;

    [Header("Events")]
    public UnityTeamEvent OnRepair;

    [HideInInspector] public float speed;
    private List<Repairable> minigames;

    public System.Action<CarEntity> OnCarRepair;

    private List<Transform> minigameSpawns;

    public void Repair () {
        OnRepair.Invoke( team );
        OnCarRepair?.Invoke( this );
    }

    public void Randomize () {
        //randomize speed
        speed = Random.Range( minSpeed, maxSpeed );

        //randomize minigames pos
        int minigamesNumber = Random.Range( 1, minigamePossiblePositions.Length - 1);
        minigameSpawns = minigamePossiblePositions.ToList();
        minigames = new List<Repairable>();
        for ( int i = 0; i < minigamesNumber; i++ ) {
            Repairable minigame = minigamesPrefabs[Random.Range(0, minigamesPrefabs.Length - 1)];

            //rand pos
            Transform randPos = minigameSpawns[Random.Range(0, minigameSpawns.Count - 1)];
            minigameSpawns.Remove( randPos );

            minigame = Instantiate( minigame, randPos );
            minigame.team = team;
            minigame.OnComplete += HandleRepairEnd;
            minigames.Add( minigame );
        }

        //randomize mesh
        GameObject mesh = carMeshes[Random.Range(0, carMeshes.Length - 1)];
        Instantiate( mesh, graphics );
    }

    private int count;
    private void HandleRepairEnd () {
        count++;
        if ( count >= minigames.Count )
            Repair();
    }
}

[System.Serializable]
public class UnityTeamEvent : UnityEvent<Team> { }