using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class HoldMinigame : BaseMinigame {
    [Header("Parameters")]
    public float holdTime;

    bool counting;
    float timer;

    protected override void CustomStart () {
        counting = true;
        timer = 0;
    }

    protected override void CustomReset () {
        counting = false;
        timer = 0;
    }

    protected override void CustomComplete () {
        counting = false;
        timer = 0;
    }

    private void Update () {
        if ( counting ) {
            timer += Time.deltaTime;
            if ( timer >= holdTime )
                Complete();
        }
    }
}