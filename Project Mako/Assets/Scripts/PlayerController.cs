using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float enginePower;
    [SerializeField] private float brakePower;
    [SerializeField] private float steerAngle;
    [SerializeField] private Transform centerOfMass;
    private Rigidbody playerRigidbody;
    private CameraFollow cameraFollow;
    [SerializeField] private WheelCollider[] wheelColliders = new WheelCollider[6];
    PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraFollow = FindObjectOfType<CameraFollow>();
    }
    private void Start()
    {
        playerRigidbody.centerOfMass = centerOfMass.transform.localPosition;
    }

    void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        CheckMotor(inputVector.y, inputVector.x);
        CheckSteer(inputVector.y, inputVector.x);
    }

    private void CheckMotor(float verticalInput, float horizontalInput)
    {
        RotateWheelsFB(verticalInput);
        BreakWheels(verticalInput, horizontalInput);
    }

    private void BreakWheels(float verticalInput, float horizontalInput)
    {
        if (verticalInput == 0 && horizontalInput == 0)
        {
            for (int i = 0; i < wheelColliders.Length; i++)
            {
                wheelColliders[i].brakeTorque = brakePower;
            }
        }
        else
        {
            for (int i = 0; i < wheelColliders.Length; i++)
            {
                wheelColliders[i].brakeTorque = 0;
            }
        }
    }

    private void RotateWheelsFB(float verticalInput)
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            wheelColliders[i].motorTorque = enginePower * verticalInput;
        }
    }

    private void CheckSteer(float verticalInput, float horizontalInput)
    {
        wheelColliders[0].steerAngle = steerAngle * horizontalInput;
        wheelColliders[1].steerAngle = steerAngle * horizontalInput;
        if (verticalInput == 0 && horizontalInput != 0)
        {
            RotateWheelsFB(Mathf.Abs(horizontalInput));
        }
    }
}
