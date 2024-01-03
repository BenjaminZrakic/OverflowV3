using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

//Enum for movement state of player:
enum PlayerMovementState
{
    Walking,
    Sprinting,
    Dashing
}

public class PlayerControllerSimple : MonoBehaviour
{

    [SerializeField] private Vector3 currentVelocity = Vector3.zero;

    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float sprintSpeed = 4f;
    [SerializeField] private float rotationSpeed = 720f;
    [Space]
    [SerializeField] private float dashForce = 20f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashCooldownTime = 0.3f;

    [SerializeField] private Animator playerAnimator;

    private PlayerMovementState _currentMovementState;
    private PlayerMovementState _previousMovementState;
    private Rigidbody _rb;

    private Vector3 _moveDirection;
    private float _moveSpeed;
    private float _defaultDrag;
    private Quaternion _targetRotation;

    private bool _canDash = true;
    private bool _isSprinting = false;

    private void Awake()
    { 
        _rb = GetComponent<Rigidbody>();
        _defaultDrag = _rb.drag;

        //Start Player in Walking state:
        _currentMovementState = PlayerMovementState.Walking;
        _moveSpeed = walkSpeed;

    }

    private void FixedUpdate()
    {
        currentVelocity = _rb.velocity;

        if (_currentMovementState != _previousMovementState)
        {
            HandleMovementStateChange();
        }

        HandleMovementAndRotation();

        _previousMovementState = _currentMovementState;
    }


    //Input system context functions:****************************************************************
    public void GetMovementInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        _moveDirection = new Vector3(input.x, 0, input.y).normalized;
    }

    public void Dash(InputAction.CallbackContext context)
    {

        if (!context.started) { return; }

        if (_canDash)
        {
            _currentMovementState = PlayerMovementState.Dashing;
            _rb.AddForce(_moveDirection * dashForce, ForceMode.Impulse);

            Invoke(nameof(StopDash), dashTime);

            StartCoroutine(DashCooldownCoroutine());
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.started && _currentMovementState == PlayerMovementState.Walking)
        {
            _currentMovementState = PlayerMovementState.Sprinting;
            _isSprinting = true;
        }
        if (context.canceled)
        {
            _currentMovementState = PlayerMovementState.Walking;
            _isSprinting = false;
        }
    }
    //***********************************************************************************************


    //Coroutines:************************************************************************************

    IEnumerator DashCooldownCoroutine()
    {
        _canDash = false;
        yield return new WaitForSeconds(dashCooldownTime);
        _canDash = true;
    }

    //***********************************************************************************************


    private void StopDash()
    {
        //Set movementState for after dash has finished:
        if (_isSprinting)
        {
            _currentMovementState = PlayerMovementState.Sprinting;
        }
        else
        {
            _currentMovementState = PlayerMovementState.Walking;
        }
    }

    private void HandleMovementAndRotation()
    {

        if (_currentMovementState == PlayerMovementState.Dashing) { return; }

        if (_moveDirection == Vector3.zero) { return; }

        _rb.AddForce(_moveDirection * _moveSpeed, ForceMode.VelocityChange);

        //Set angle for target rotation:
        _targetRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
        //Smoothly transition between current _rb.rotation and _targetRotation: (get desired rotation angle for each FixedUpdate())
        _targetRotation = Quaternion.RotateTowards(_rb.rotation, _targetRotation, rotationSpeed * Time.fixedDeltaTime);
        _rb.MoveRotation(_targetRotation);

    }

    private void HandleMovementStateChange()
    {
        if (_currentMovementState == PlayerMovementState.Walking)
        {
            _moveSpeed = walkSpeed;
        }
        if (_currentMovementState == PlayerMovementState.Sprinting)
        {
            _moveSpeed = sprintSpeed;
        }

        //Set drag to 0 if dashing:
        if (_currentMovementState == PlayerMovementState.Dashing)
        {
            _rb.drag = 0f;
        }
        else
        {
            _rb.drag = _defaultDrag;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (!playerAnimator.GetBool("isAttacking")) playerAnimator.SetTrigger("AttackPerformed");


        Debug.Log("Attack");
    }



}
