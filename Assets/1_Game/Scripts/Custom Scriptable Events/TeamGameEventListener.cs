using UnityEngine;

public class TeamGameEventListener : MonoBehaviour {
    public TeamGameEvent gameEvent;
    public UnityTeamEvent response;

    private void OnEnable () {
        gameEvent.Subscribe( this );
    }

    private void OnDisable () {
        gameEvent.Unsubscribe( this );
    }

    public void OnInvoke ( Team team ) {
        response.Invoke( team );
    }
}