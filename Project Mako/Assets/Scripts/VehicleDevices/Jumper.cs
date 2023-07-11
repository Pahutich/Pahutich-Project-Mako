using System.Collections.Generic;
using Mako.Movement;
using UnityEngine;

namespace Mako.VehicleDevices
{
  public class Jumper : MonoBehaviour
  {
    private bool isJumping = false;
    private Rigidbody jumpingRigidbody;
    private AudioSource audioSource;
    private PlayerInputActions playerInputActions;
    private PlayerController playerController;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpFuelMax;
    [SerializeField] private float fuelRegenerationAbility;
    [SerializeField] private float jumpFuelCurrent;
    [SerializeField] private List<ParticleSystem> enginesVisuals;
    private void Awake()
    {
      jumpingRigidbody = GetComponentInParent<Rigidbody>();
      audioSource = GetComponent<AudioSource>();
      playerInputActions = new PlayerInputActions();
      playerController = GetComponentInParent<PlayerController>();
      playerInputActions.Player.Enable();
      jumpFuelCurrent = jumpFuelMax;
      enginesVisuals.ForEach(e => e.Stop());
    }

    // Update is called once per frame
    void Update()
    {
      bool jumpInputActivated = playerInputActions.Player.Jump.ReadValue<float>() > 0.1f;
      bool hasSufficientAmountOfFuel = jumpFuelCurrent > 0;

      if (jumpInputActivated && hasSufficientAmountOfFuel)
      {
        isJumping = true;
      }
      else
      {
        isJumping = false;
      }

      if (jumpFuelCurrent > jumpFuelMax)
        jumpFuelCurrent = jumpFuelMax;
      if (jumpFuelCurrent < 0)
        jumpFuelCurrent = 0;
    }
    private void FixedUpdate()
    {
      if (isJumping)
      {
        ActivateJump();
      }
      else
      {
        DeactivateJump();
      }
    }
    private void ActivateJump()
    {
      Vector3 jumpVector = transform.up * jumpForce;
      jumpingRigidbody.AddForce(jumpVector, ForceMode.Impulse);
      enginesVisuals.ForEach(e => e.Play());
      jumpFuelCurrent -= Time.deltaTime * fuelRegenerationAbility;
      if (!audioSource.isPlaying)
        audioSource.Play();
    }

    private void DeactivateJump()
    {
      isJumping = false;
      enginesVisuals.ForEach(e => e.Stop());
      if (playerController.IsGrounded())
        jumpFuelCurrent += Time.deltaTime * fuelRegenerationAbility;
      if (audioSource.isPlaying)
        audioSource.Stop();
    }
  }
}
