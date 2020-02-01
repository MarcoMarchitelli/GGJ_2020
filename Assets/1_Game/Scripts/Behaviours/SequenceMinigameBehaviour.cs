using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Deirin.EB;

public class SequenceMinigameBehaviour : BaseMinigame {
    [Header("Parameters")]
    public InputAction[] actions;
    public float maxPressInterval;

    protected override void CustomStart () {

    }

    private int currentIndex;
    private void StartSequence () {
        currentAction = actions[currentIndex];
    }

    private InputAction currentAction;
    private bool next;
    private IEnumerator Sequence () {
        currentAction = actions[0];

        for ( int i = 0; i < actions.Length; i++ ) {

        }

        while ( !next )
            yield return null;
    }
}