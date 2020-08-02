using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FPSInput))]
[RequireComponent(typeof(FPSMotor))]
public class PlayerController : MonoBehaviour
{
    FPSInput _input = null;
    FPSMotor _motor = null;

    //[SerializeField] GameObject crossbow;
    //[SerializeField] ParticleSystem crossbowFX;

    [SerializeField] float _moveSpeed = .1f;
    [SerializeField] float _turnSpeed = 6f;
    //[SerializeField] float _jumpStrength = 10f;
    //[SerializeField] float _sprintSpeed = .15f;
    [SerializeField] float _defaultSpeed = .1f;

    //AudioSource shootSound = null;

    private void Awake()
    {
        _input = GetComponent<FPSInput>();
        _motor = GetComponent<FPSMotor>();

        //shootSound = crossbow.GetComponent<AudioSource>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        _input.MoveInput += OnMove;
        _input.RotateInput += OnRotate;
        //_input.JumpInput += OnJump;
        //_input.SprintPressed += OnSprintPressed;
        //_input.SprintReleased += OnSprintReleased;
        _input.ShootInput += OnShoot;
    }

    private void OnDisable()
    {
        _input.MoveInput -= OnMove;
        _input.RotateInput -= OnRotate;
        //_input.JumpInput -= OnJump;
        //_input.SprintPressed -= OnSprintPressed;
        //_input.SprintReleased -= OnSprintReleased;
        _input.ShootInput -= OnShoot;
    }

    void OnMove(Vector3 movement)
    {
        // movespeed
        _motor.Move(movement * _moveSpeed);
    }

    void OnRotate(Vector3 rotation)
    {
        // camera looks vert, body rotates left/right
        _motor.Turn(rotation.y * _turnSpeed);
        _motor.Look(rotation.x * _turnSpeed);
    }

    /*void OnJump()
    {
        // apply jumpforce to motor
        _motor.Jump(_jumpStrength);
    }*/

    /*void OnSprintPressed()
    {
        // apply sprint speed to motor
        _moveSpeed = _moveSpeed * _sprintSpeed;
    }*/

    /*void OnSprintReleased()
    {
        _moveSpeed = _defaultSpeed;
    }*/

    void OnShoot()
    {
        //shootSound.Play();
        //crossbowFX.Emit(200);
    }
}
