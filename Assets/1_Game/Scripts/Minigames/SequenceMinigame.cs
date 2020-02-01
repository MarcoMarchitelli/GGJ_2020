using UnityEngine;
using UnityEngine.InputSystem;

public class SequenceMinigame : BaseMinigame {
    [Header("Parameters")]
    public int actionsNumber = 4;

    private InputAction[] actions;
    private InputAction currentAction;

    protected override void CustomStart () {
        StartSequence();
    }

    protected override void CustomReset () {
        currentIndex = 0;
    }

    private int currentIndex;
    private void StartSequence () {
        currentPlayer.SwitchCurrentActionMap( "Sequence Minigame" );

        actions = new InputAction[actionsNumber];
        int aviableActions = currentPlayer.currentActionMap.actions.Count;
        for ( int i = 0; i < actionsNumber; i++ ) {
            actions[i] = currentPlayer.currentActionMap.actions[UnityEngine.Random.Range( 0, aviableActions )];
        }

        GoToNextAction();
    }

    private void GoToNextAction () {
        currentPlayer.currentActionMap.actionTriggered -= ActionTriggeredHandler;
        currentAction = actions[currentIndex];
        currentPlayer.currentActionMap.actionTriggered += ActionTriggeredHandler;
    }

    private void ActionTriggeredHandler ( InputAction.CallbackContext obj ) {
        if ( obj.action == currentAction )
            GoToNextAction();
        else {
            ResetMinigame();
            currentPlayer.currentActionMap.actionTriggered -= ActionTriggeredHandler;
        }
    }
}