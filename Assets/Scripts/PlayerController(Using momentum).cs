using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

////Enum for movement state of player:
//enum PlayerMovementState
//{ 
//   Walking,
//   Sprinting,
//   Dashing
//}

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private bool momentumEnabled;

    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float rotationSpeed = 720f;
    [Space]
    [SerializeField] private float dashForce = 20f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashCooldownTime = 1f;
    [SerializeField] private float dashSpeedChangeFactor = 50f;

    private PlayerMovementState _currentMovementState;
    private PlayerMovementState _lastUpdateState;
    private float _movementSpeed;
    private Rigidbody _rb;
    private float defaultDrag;
    private Vector3 _movDirection = Vector3.zero;
    private Quaternion _targetRotation;
    private bool _canDash = true;

    private float _targetMoveSpeed;
    private bool _keepMomentum = false;
    private float _speedChangeFactor = 1f;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        defaultDrag = _rb.drag;

        _currentMovementState = PlayerMovementState.Walking;
        _lastUpdateState = _currentMovementState;
        _movementSpeed = walkSpeed;
        _targetMoveSpeed = _movementSpeed;
    }

    private void FixedUpdate()
    {

        _movementSpeed = _rb.velocity.magnitude;

        if (_currentMovementState != _lastUpdateState)
        {
            HandleMovementStateChange();
        }

        if (_movementSpeed != _targetMoveSpeed)
        {
            HandleMomentum();
        }

        HandleMovementAndRotation();
        HandleDrag();

        _lastUpdateState = _currentMovementState;
    }


    //Input system context functions:****************************************************************
    public void GetMovementInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        _movDirection = new Vector3(input.x, 0, input.y).normalized;
    }

    public void Dash(InputAction.CallbackContext context)
    {

        if (!context.started) { return; }

        if (_canDash)
        {
            _currentMovementState = PlayerMovementState.Dashing;
            _rb.AddForce(_movDirection * dashForce, ForceMode.Impulse);

            Invoke(nameof(StopDash), dashTime);

            StartCoroutine(DashCooldownCoroutine());
        }
    }
    //***********************************************************************************************


    private void HandleMovementAndRotation()
    {

        if (_currentMovementState == PlayerMovementState.Dashing) { return; }

        if (_movDirection == Vector3.zero) { return; }

        _rb.AddForce(_movDirection * _movementSpeed, ForceMode.VelocityChange);

        //Set angle for target rotation:
        _targetRotation = Quaternion.LookRotation(_movDirection, Vector3.up);
        //Smoothly transition between current _rb.rotation and _targetRotation: (get desired rotation angle for each FixedUpdate())
        _targetRotation = Quaternion.RotateTowards(_rb.rotation, _targetRotation, rotationSpeed * Time.fixedDeltaTime);
        _rb.MoveRotation(_targetRotation);
    }

    private void HandleDrag()
    {
        if (_currentMovementState == PlayerMovementState.Dashing) { _rb.drag = 0; }
        else { _rb.drag = defaultDrag; }
    }

    private void HandleMomentum()
    {
        if (_keepMomentum && momentumEnabled)
        {
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            _movementSpeed = _targetMoveSpeed;
        }
    }

    private void HandleMovementStateChange()
    {
        if (_currentMovementState == PlayerMovementState.Walking)
        {
            _targetMoveSpeed = walkSpeed;
        }
        if (_currentMovementState == PlayerMovementState.Dashing)
        {
            _speedChangeFactor = dashSpeedChangeFactor;
        }

        if (_lastUpdateState == PlayerMovementState.Dashing)
        {
            _keepMomentum = true;
        }
    }

    private void StopDash()
    {
        _currentMovementState = PlayerMovementState.Walking;
    }


    //Coroutines:***********************************************************************************
    IEnumerator DashCooldownCoroutine()
    {
        _canDash = false;
        yield return new WaitForSeconds(dashCooldownTime);
        _canDash = true;
    }

    float boostFactor;
    //Smooth Lerp of _movementSpeed to targetMoveSpeed
    IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(_rb.velocity.magnitude - _targetMoveSpeed);
        float startValue = _rb.velocity.magnitude;
        Debug.Log("aaaaaa");

        boostFactor = _speedChangeFactor;

        while (time < difference)
        {
            _movementSpeed = Mathf.Lerp(startValue, _targetMoveSpeed, time / difference);

            time += Time.deltaTime * boostFactor;

            yield return null;
        }

        _movementSpeed = _targetMoveSpeed;
        _speedChangeFactor = 1f;
        _keepMomentum = false;
    }
    //*********************************************************************************************

}
