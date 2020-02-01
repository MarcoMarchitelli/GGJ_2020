using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Deirin.EB;

public class PlayerInputHandlerBehaviour : BaseBehaviour {
    [Header("References")]
    public Transform cameraTransform;

    [Header("Events")]
    public UnityVector3Event OnMoveInputChange;

    private Vector3 inputVector;

    protected override void CustomSetup () {
        if ( !cameraTransform )
            SetCamera();
    }

    public void SetMoveInput ( InputAction.CallbackContext callbackContext ) {
        Vector2 inputVector = callbackContext.ReadValue<Vector2>();
        this.inputVector = new Vector3( inputVector.x, 0, inputVector.y );
        if ( !cameraTransform )
            SetCamera();
        this.inputVector = cameraTransform.TransformDirection( inputVector );
        this.inputVector.y = 0;
        OnMoveInputChange.Invoke( this.inputVector.normalized );
    }

    private void SetCamera () {
        cameraTransform = Camera.main.transform;
    }
}

[System.Serializable]
public class UnityVector3Event : UnityEvent<Vector3> { }