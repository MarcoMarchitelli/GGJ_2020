using UnityEngine;
using UnityEngine.Events;
using Deirin.EB;

public class PlayerInputBehaviour : BaseBehaviour {
    [Header("References")]
    public Transform cameraTransform;

    [Header("Data")]
    public Vector3Variable moveInput;

    private Vector3 inputVector;

    public override void OnUpdate () {
        ReadMovementInput();
    }

    private void ReadMovementInput () {
        inputVector = new Vector3( Input.GetAxisRaw( "Horizontal" ), 0, Input.GetAxisRaw( "Vertical" ) );
        inputVector = cameraTransform.TransformDirection( inputVector );
        inputVector.y = 0;
        moveInput.Value = inputVector.normalized;
    }
}