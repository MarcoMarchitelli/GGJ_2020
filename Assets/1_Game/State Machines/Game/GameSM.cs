using UnityEngine;
using Deirin.StateMachine;
using Deirin.CustomButton;
using Deirin.Tweeners;
using Deirin.Utilities;

public class GameSM : StateMachineBase {
    [Header("Context")]
    public GameContext context;

    private void OnDisable () {
        EventsUnsubscriptions();
    }

    protected override void CustomDataSetup () {
        EventsSubscriptions();
        data = context;
    }

    private void GoToPlayersSelection () {
        animator.SetTrigger( "PlayersSelection" );
    }

    private void GoToOptionSelection () {
        animator.SetTrigger( "OptionSelection" );
    }

    private void EventsSubscriptions () {
        context.GoToPlayersSelection += GoToPlayersSelection;
        context.GoToOptionSelection += GoToOptionSelection;
    }

    private void EventsUnsubscriptions () {
        context.GoToPlayersSelection -= GoToPlayersSelection;
        context.GoToOptionSelection -= GoToOptionSelection;
    }
}

[System.Serializable]
public class GameContext : IStateMachineData {
    public System.Action GoToPlayersSelection, GoToOptionSelection;
}