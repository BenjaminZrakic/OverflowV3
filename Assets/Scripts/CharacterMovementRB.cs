using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementRB : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float rotationSpeed = 720f;

    private Rigidbody _rb;
    [SerializeField] private Vector3 _moveDirection;
    public void SetMovementDirection(Vector3 moveDirection)
    {
        _moveDirection = moveDirection;
    }
    private Quaternion _targetRotation;

    private void Awake()
    { 
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Move RigidBody:
        if (_rb != null)
        {
            _rb.velocity = _moveDirection * movementSpeed;
        }

       /* //Rotate
        //Set angle for target rotation:
        _targetRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
        //Smoothly transition between current _rb.rotation and _targetRotation: (get desired rotation angle for each FixedUpdate())
        _targetRotation = Quaternion.RotateTowards(_rb.rotation, _targetRotation, rotationSpeed * Time.fixedDeltaTime);
        _rb.MoveRotation(_targetRotation);*/
    }



}
