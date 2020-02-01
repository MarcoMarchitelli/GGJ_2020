using UnityEngine;
using Deirin.StateMachine;
using Deirin.CustomButton;
using Deirin.Tweeners;
using Deirin.Utilities;

public class MainMenuSM : StateMachineBase {
    [Header("Context")]
    public MainMenuContext context;

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
public class MainMenuContext : IStateMachineData {
    public System.Action GoToPlayersSelection, GoToOptionSelection;

    [Header("Buttons")]
    public CanvasGroup buttonsCanvasGroup;
    public CustomButton_Canvas playButton;
    public CustomButton_Canvas quitButton;
    [Header("Screen Fader")]
    public CanvasGroupTweener screenFader;
    [Header("Scene Loader")]
    public SceneLoader sceneLoader;
}