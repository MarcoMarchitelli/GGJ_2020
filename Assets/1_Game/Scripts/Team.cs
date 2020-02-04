using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Team : MonoBehaviour {
    [Header("Player Data")]
    public PlayerData[] players;

    [Header("Car References")]
    public Transform carWaitPos;
    public Transform carStartPos;
    public Transform carEndPos;
    public Transform carPos1;
    public Transform carPos2;
    public CarEntity carPrefab;

    [Header("Car Wave Data")]
    public float startInterval;
    public float endInterval;
    public int pileLimit;
    public float pileDistance;

    [Header("Events")]
    public UnityEvent OnLose;

    [HideInInspector] public List<PlayerEntity> playerEntities = new List<PlayerEntity>();
    private float interval;
    private bool started;

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

    public void SetupPlayers () {
        foreach ( var item in playerEntities ) {
            item.playerInput.SwitchCurrentActionMap( "Gameplay" );
        }
    }

    private CarEntity repairSpotCar;
    private Queue<CarEntity> carPile = new Queue<CarEntity>();
    private Vector3 direction;
    public void StartCarWave () {
        direction = ( carStartPos.position - carEndPos.position ).normalized;
        started = true;

        StartCoroutine( FirstCar() );
    }

    IEnumerator FirstCar () {
        repairSpotCar = Instantiate( carPrefab );
        repairSpotCar.team = this;
        repairSpotCar.Randomize();
        repairSpotCar.transform.position = carStartPos.position;
        GoToRepair( repairSpotCar );
        CheckPile();

        yield return new WaitForSeconds( interval );

        StartCoroutine( Wave() );
    }

    IEnumerator Wave () {
        //new car
        CarEntity car = Instantiate( carPrefab );
        car.team = this;
        car.Randomize();
        carPile.Enqueue( car );

        car.transform.position = carStartPos.position;
        car.transform.DOMove( carWaitPos.position + direction * ( carPile.Count - 1 ) * pileDistance, Vector3.Distance( car.transform.position, carWaitPos.position ) / car.speed );

        CheckPile();

        if ( instaGoToRepair )
            GoToRepair( carPile.Dequeue() );

        yield return new WaitForSeconds( interval );

        StartCoroutine( Wave() );
    }

    private void GoToRepair ( CarEntity car ) {
        car.OnCarRepair += OnCarRepairHandler;
        car.transform.DOMove( carPos1.position, Vector3.Distance( car.transform.position, carPos1.position ) / car.speed );
        repairSpotCar = car;
    }

    private void GoAway ( CarEntity car ) {
        car.OnCarRepair -= OnCarRepairHandler;
        car.transform.DOMove( carEndPos.position, Vector3.Distance( car.transform.position, carEndPos.position ) / car.speed ).onComplete += () => Destroy( car.gameObject );
        repairSpotCar = null;
    }

    bool instaGoToRepair;
    private void OnCarRepairHandler ( CarEntity obj ) {
        if ( obj.team == this ) {
            if ( obj == repairSpotCar ) {
                GoAway( obj );
                if ( carPile.Count != 0 )
                    GoToRepair( carPile.Dequeue() );
                else
                    instaGoToRepair = true;
            }
        }
    }

    private void Update () {
        if ( started ) {
            interval -= Time.deltaTime;
            interval = Mathf.Clamp( interval, endInterval, startInterval );
        }
    }

    private void CheckPile () {
        if ( carPile.Count >= pileLimit )
            OnLose.Invoke();
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