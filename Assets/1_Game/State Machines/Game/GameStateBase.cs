using Deirin.StateMachine;

public abstract class GameStateBase : StateBase {
    protected GameContext context;

    protected override void CustomSetup () {
        context = data as GameContext;
    }
}