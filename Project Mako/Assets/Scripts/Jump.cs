using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private bool canRechargeFuel = true;
    private Rigidbody jumpingRigidbody;
    private PlayerInputActions playerInputActions;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpFuelMax;
    [SerializeField] private float fuelRegenerationAbility;
    [SerializeField] private float jumpFuelCurrent;
    [SerializeField] private List<GameObject> enginesVisuals;
    private void Awake()
    {
        jumpingRigidbody = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        jumpFuelCurrent = jumpFuelMax;
        enginesVisuals.ForEach(e => e.SetActive(false));
    }

    // Update is called once per frame
    void Update()
    {
        bool isJumping = playerInputActions.Player.Jump.ReadValue<float>() > 0.1f;
        bool hasSuffientAmountOfFuel = jumpFuelCurrent > 0;
        if (isJumping && hasSuffientAmountOfFuel)
        {
            jumpingRigidbody.AddForce(transform.InverseTransformDirection(Vector3.up) * jumpForce, ForceMode.Force);
            enginesVisuals.ForEach(e => e.SetActive(true));
            canRechargeFuel = false;
            jumpFuelCurrent -= Time.deltaTime * fuelRegenerationAbility;
        }
        else
        {
            enginesVisuals.ForEach(e => e.SetActive(false));
            canRechargeFuel = true;
            jumpFuelCurrent += Time.deltaTime * fuelRegenerationAbility;
        }
            
        if (jumpFuelCurrent > jumpFuelMax)
            jumpFuelCurrent = jumpFuelMax;
        if (jumpFuelCurrent < 0)
            jumpFuelCurrent = 0;
    }
}
