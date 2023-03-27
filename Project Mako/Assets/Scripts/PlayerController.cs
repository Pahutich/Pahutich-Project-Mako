using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float enginePower;
    [SerializeField] private float brakePower;
    [SerializeField] private float steerAngle;
    [SerializeField] private Transform centerOfMass;
    [SerializeField] private TextMeshProUGUI speedText;
    private Rigidbody playerRigidbody;
    private CameraFollow cameraFollow;
    Vector2 inputVector = Vector2.zero;
    private float speed = 0f;
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
    private void Update()
    {
        inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        if (playerRigidbody != null)
        {
            speed = playerRigidbody.velocity.magnitude * 3.6f;
            speedText.text = "Speed: " + speed;
        }
    }
    void FixedUpdate()
    {

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
        if (speed > 0.01f && (verticalInput == 0 && horizontalInput == 0) && Mathf.Abs(playerRigidbody.velocity.z) > 0.01f ||
        (playerRigidbody.transform.InverseTransformDirection(playerRigidbody.velocity).z >= 0.1f && verticalInput < 0 ||
        playerRigidbody.transform.InverseTransformDirection(playerRigidbody.velocity).z <= -0.1f && verticalInput > 0))
        {
            for (int i = 0; i < wheelColliders.Length; i++)
            {
                if (wheelColliders[i] != null)
                    wheelColliders[i].brakeTorque = brakePower;
            }
        }
        else
        {
            for (int i = 0; i < wheelColliders.Length; i++)
            {
                if (wheelColliders[i] != null)
                    wheelColliders[i].brakeTorque = 0;
            }
        }
    }

    private void RotateWheelsFB(float verticalInput)
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            if (wheelColliders[i] != null)
                wheelColliders[i].motorTorque = enginePower * verticalInput;
        }
    }

    private void CheckSteer(float verticalInput, float horizontalInput)
    {
        if (wheelColliders[0] != null && wheelColliders[1] != null)
        {
            wheelColliders[0].steerAngle = steerAngle * horizontalInput;
            wheelColliders[1].steerAngle = steerAngle * horizontalInput;
        }
    }
}
