using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class Team : MonoBehaviour {
    #region Inspector
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
    #endregion

    [HideInInspector] public List<PlayerEntity> playerEntities = new List<PlayerEntity>();
    private float interval;
    private bool started;
    private Queue<CarEntity> carPile = new Queue<CarEntity>();
    private CarEntity[] carPileArray;
    private Vector3 direction;
    private CarEntity car2;
    private CarEntity car1;
    private System.Action OnCar1Repair, OnCar2Repair;

    #region API
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

    public void StartCarWave () {
        direction = ( carStartPos.position - carEndPos.position ).normalized;
        started = true;
        interval = startInterval;

        StartCoroutine( Wave() );
    }
    #endregion

    #region Event Handlers
    private void Car2RepairHanlder () {
        GoAway( car2 );
        car2 = null;
        if ( car1 ) {
            if ( car1.repaired ) {
                GoAway( car1 );
                car1 = null;
                if ( carPile.Count > 0 ) {
                    GoToRepair1( carPile.Dequeue() );
                    UpdatePilePositions();
                }
            }
        }

    }

    private void Car1RepairHandler () {
        if ( car2 == null ) {
            GoAway( car1 );
            car1 = null;
            if ( carPile.Count > 0 ) {
                GoToRepair1( carPile.Dequeue() );
                UpdatePilePositions();
            }
        }
    } 
    #endregion

    #region Privates
    IEnumerator Wave () {
        //new car
        CarEntity car = Instantiate( carPrefab );
        car.team = this;
        car.Randomize();
        carPile.Enqueue( car );

        car.transform.position = carStartPos.position;

        if ( !car2 ) {
            GoToRepair2( carPile.Dequeue() );
            UpdatePilePositions();
        }
        else if ( !car1 ) {
            GoToRepair1( carPile.Dequeue() );
            UpdatePilePositions();
        }
        else {
            GoToPile( car );
            CheckPile();
        }

        yield return new WaitForSeconds( interval );

        StartCoroutine( Wave() );
    }

    private void GoToRepair1 ( CarEntity car ) {
        car.OnCarRepair += OnCarRepairHandler;
        car.transform.DOMove( carPos1.position, Vector3.Distance( car.transform.position, carPos1.position ) / car.speed );
        car1 = car;
    }

    private void GoToRepair2 ( CarEntity car ) {
        car.OnCarRepair += OnCarRepairHandler;
        car.transform.DOMove( carPos2.position, Vector3.Distance( car.transform.position, carPos2.position ) / car.speed );
        car2 = car;
    }

    private void GoAway ( CarEntity car ) {
        car.OnCarRepair -= OnCarRepairHandler;
        car.transform.DOMove(
                carEndPos.position,
                Vector3.Distance( car.transform.position, carEndPos.position ) / car.speed
            ).onComplete += () => Destroy( car.gameObject );
    }

    private void GoToPile ( CarEntity car ) {
        car.transform.DOMove(
                        carWaitPos.position + direction * ( carPile.Count - 1 ) * pileDistance,
                        Vector3.Distance( car.transform.position, carWaitPos.position ) / car.speed
                     ).onComplete += CheckPile;
    }

    private void OnCarRepairHandler ( CarEntity obj ) {
        if ( obj.team == this ) {
            if ( obj == car2 ) {
                OnCar2Repair?.Invoke();
            }
            else if ( obj == car1 ) {
                OnCar1Repair?.Invoke();
            }
        }
    }

    private void CheckPile () {
        if ( carPile.Count >= pileLimit )
            OnLose.Invoke();
    }

    private void UpdatePilePositions () {
        carPileArray = carPile.ToArray();
        int count = carPileArray.Length;
        for ( int i = 0; i < count; i++ ) {
            CarEntity car = carPileArray[i];
            car.transform.DOMove(
                carWaitPos.position + direction * i * pileDistance,
                Vector3.Distance( car.transform.position, carWaitPos.position ) / car.speed
             ).onComplete += CheckPile;
        }
    }
    #endregion

    #region Monos
    private void Update () {
        if ( started ) {
            interval -= Time.deltaTime;
            interval = Mathf.Clamp( interval, endInterval, startInterval );
        }
    }

    private void OnDisable () {
        OnCar1Repair -= Car1RepairHandler;
        OnCar2Repair -= Car2RepairHanlder;
    }

    private void OnEnable () {
        OnCar1Repair += Car1RepairHandler;
        OnCar2Repair += Car2RepairHanlder;
    }
    #endregion
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