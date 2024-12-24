using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector3 _targetDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection() {
        if (_playerAwarenessController.AwareOfPlayer) {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
        else {
            _targetDirection = Vector3.zero;
        }
    }

    private void RotateTowardsTarget() {
        if (_targetDirection == Vector3.zero) {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(_targetDirection, Vector3.up);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime); //rotate at same speed regardless of frame rate
        
        _rigidbody.MoveRotation(rotation);
    }

    private void SetVelocity() {
        if (_targetDirection == Vector3.zero) {
            _rigidbody.velocity = Vector3.zero;
        }
        else {
            _rigidbody.velocity = transform.forward * _speed;
        }
    }
}
