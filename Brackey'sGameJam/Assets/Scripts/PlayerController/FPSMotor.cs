using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class FPSMotor : MonoBehaviour
{
    [SerializeField] Camera _camera = null;
    [SerializeField] float _cameraAngleLimit = 70f;
    [SerializeField] GroundDetector _groundDetector = null;

    Rigidbody _rigidbody = null;

    public event Action Land = delegate { };

    Vector3 _movementThisFrame = Vector3.zero;
    float _turnAmountThisFrame = 0;
    float _lookAmountThisFrame = 0;
    bool _isGrounded = false;
    // tracking camera angle, avoid 0-360 angle conversion
    private float _currentCameraRotationX = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 reqMovement)
    {
        // store movement for fixedUpdate
        _movementThisFrame = reqMovement;
    }

    public void Turn(float turnAmount)
    {
        // store movement for fixed
        _turnAmountThisFrame = turnAmount;
    }

    public void Look(float lookAmount)
    {
        // store movement for fixed
        _lookAmountThisFrame = lookAmount;
    }

    /*public void Jump(float jumpForce)
    {
        // only allow us to jump if we are on the ground
        if (_isGrounded == false)
        {
            return;
        }

        _rigidbody.AddForce(Vector3.up * jumpForce);
    }*/

    private void FixedUpdate()
    {
        ApplyMovement(_movementThisFrame);
        ApplyTurn(_turnAmountThisFrame);
        ApplyLook(_lookAmountThisFrame);
    }

    void ApplyMovement(Vector3 moveVector)
    {
        // confirm we have movement, exit early if we dont
        if (moveVector == Vector3.zero)
        {
            return;
        }

        // move rigidbody
        _rigidbody.MovePosition(_rigidbody.position + moveVector);
        // clear out movement until we get new move request
        _movementThisFrame = Vector3.zero;
    }

    void ApplyTurn(float rotateAmount)
    {
        // confirm we have rotation, if not exit early
        if (rotateAmount == 0)
        {
            return;
        }

        // rotate body, convert xyz to Quat for moveRotation
        Quaternion newRotation = Quaternion.Euler(0, rotateAmount, 0);
        _rigidbody.MoveRotation(_rigidbody.rotation * newRotation);
        // clear out turn amount for next request
        _turnAmountThisFrame = 0;
    }

    void ApplyLook(float lookAmount)
    {
        // confirm we have rotation, exit early if not
        if (lookAmount == 0)
        {
            return;
        }

        // calculate and clamp new camera rotation before we apply it
        _currentCameraRotationX -= lookAmount;
        _currentCameraRotationX = Mathf.Clamp(_currentCameraRotationX, -_cameraAngleLimit, _cameraAngleLimit);
        _camera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0, 0);
        // clear out x movement until we get new requests
        _lookAmountThisFrame = 0;
    }

    private void OnEnable()
    {
        _groundDetector.GroundDetected += OnGroundDetected;
        _groundDetector.GroundVanished += OnGroundVanished;
    }

    private void OnDisable()
    {
        _groundDetector.GroundDetected -= OnGroundDetected;
        _groundDetector.GroundVanished -= OnGroundVanished;
    }

    void OnGroundDetected()
    {
        _isGrounded = true;
        // notify others we have landed
        Land?.Invoke();
    }

    void OnGroundVanished()
    {
        _isGrounded = false;
    }
}
