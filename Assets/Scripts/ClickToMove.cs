using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{

    [SerializeField] private InputAction mouseClickAction;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;

    private Camera _mainCamera;
    private Rigidbody _rb;

    private Coroutine _coroutine;
    private Vector3 _targetPosition;
    private Vector3 _moveDirection;
    private float _playerDistanceToFloor;

    private int _groundLayer;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _groundLayer = LayerMask.NameToLayer("Ground");
    }


    //Called when script is enabled
    private void OnEnable()
    {
        mouseClickAction.Enable();
        //Listen for event:
        mouseClickAction.performed += Move;
    }
    //Called when script is disabled
    private void OnDisable()
    {
        //Unsubscribe listener:
        mouseClickAction.performed -= Move;
        mouseClickAction.Disable();
    }


    private void Move(InputAction.CallbackContext context)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        //Checks if raycast in direction of ray has hit an object:
        //Also check if raycast has hit object in Ground layer:
        if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit) && hit.collider && hit.collider.gameObject.layer.CompareTo(_groundLayer) == 0)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(PlayerMoveTowards(hit.point));
            _targetPosition = hit.point;
            this.GetComponent<MovePositionDirectRB>().SetTargetPosition(hit.point);
        }
    }


    //Coroutine instead of update function (called every frame anyways) 
    private IEnumerator PlayerMoveTowards(Vector3 target)
    {
        //Get offset for player distance to floor:
        _playerDistanceToFloor = transform.position.y - target.y;
        target.y += _playerDistanceToFloor;
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            //Ignore Collisions:
            //Vector3 destination = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            //transform.position = destination;


            //Use Rigidbody:
            _moveDirection = (target - transform.position).normalized;
            _rb.velocity = _moveDirection * moveSpeed;

            //Rotate in move direction:
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_moveDirection), rotationSpeed * Time.deltaTime);



            yield return null;
        }
    }


    //Draw gizmo for debugging:
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_targetPosition, 1);
    }

}
