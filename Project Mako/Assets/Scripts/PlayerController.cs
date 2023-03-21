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
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        cameraFollow = FindObjectOfType<CameraFollow>();
    }

    private void Start()
    {
        playerRigidbody.centerOfMass = centerOfMass.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        var verticalInput = PlayerInput.Instance.VerticalInput;
        var horizontalInput = PlayerInput.Instance.HorizontalInput;
        CheckMotor(verticalInput, horizontalInput);
        CheckSteer(verticalInput, horizontalInput);
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
