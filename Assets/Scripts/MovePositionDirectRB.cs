using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//[RequireComponent] typeof(Rigidbody);
public class MovePositionDirectRB : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float rotationSpeed = 5f;

    private Rigidbody _rb;

    private Vector3 _target;
    private float _distanceToFloor;

    private Vector3 _moveDirection;

    public void SetTargetPosition(Vector3 target)
    { 
        this._target = target;
        //Get offset for player distance to floor:
        _distanceToFloor = transform.position.y - _target.y;
        _target.y += _distanceToFloor;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        if (Vector3.Distance(transform.position, _target) > 0.1f)
        {

            //Use Rigidbody:
            _moveDirection = (_target - transform.position).normalized;
            _rb.velocity = _moveDirection * moveSpeed;

            //Rotate in move direction:
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_moveDirection), rotationSpeed * Time.deltaTime);
        }

    }
}
