using Deirin.StateMachine;

public abstract class MainMenuStateBase : StateBase {
    protected MainMenuContext context;

    protected override void CustomSetup () {
        context = data as MainMenuContext;
    }
}