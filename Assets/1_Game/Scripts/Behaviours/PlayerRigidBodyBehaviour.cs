using UnityEngine;
using UnityEngine.Events;
using Deirin.EB;

public class PlayerRigidBodyBehaviour : BaseBehaviour {
    [Header("Data")]
    public Vector3Variable moveInput;

    [Header("References")]
    public Rigidbody rb;

    [Header("Parameters")]
    public float acceleration;
    public float maxSpeed;
    public float turnSpeed;

    [Header("Events")]
    public UnityEvent OnMove;
    public UnityEvent OnStop;

    private Vector3 velocity;
    private Vector3 localDir;
    private float magnitude;
    private Quaternion targetRotation;
    private Quaternion currentRotation;
    private bool moving;

    #region Overrides
    public override void OnUpdate () {
        Turn();
    }

    public override void OnLateUpdate () {
        Move();
    }
    #endregion

    #region Privates
    private void Move () {
        CheckMovementStatus();

        velocity = moveInput.Value * acceleration * Time.fixedDeltaTime;
        magnitude = velocity.sqrMagnitude;
        magnitude = Mathf.Clamp( magnitude, 0, maxSpeed * maxSpeed );
        velocity = velocity.normalized * magnitude;
        rb.velocity = new Vector3( velocity.x, rb.velocity.y, velocity.z );
    }

    private void CheckMovementStatus () {
        if ( moving && moveInput.Value == Vector3.zero ) {
            moving = false;
            OnStop.Invoke();
        }
        else if ( !moving && moveInput.Value != Vector3.zero ) {
            moving = true;
            OnMove.Invoke();
        }
    }

    private void Turn () {
        currentRotation = transform.localRotation;

        transform.localRotation = Quaternion.identity;

        if ( moveInput.Value != Vector3.zero )
            localDir = transform.InverseTransformDirection( moveInput.Value );

        targetRotation = Quaternion.LookRotation( localDir, transform.up );

        transform.localRotation = Quaternion.Slerp( currentRotation,
                                                    targetRotation,
                                                    1 - Mathf.Exp( -turnSpeed * Time.deltaTime )
                                                    );
    } 
    #endregion
}