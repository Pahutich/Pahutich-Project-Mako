using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitro : MonoBehaviour
{
    private bool canRechargeFuel = true;
    private Rigidbody playerRigidbody;
    private AudioSource audioSource;
    private PlayerInputActions playerInputActions;
    [SerializeField] private float nitroForce = 10f;
    [SerializeField] private float nitroFuelMax;
    [SerializeField] private float nitroRegenerationAbility;
    [SerializeField] private float nitroFuelCurrent;
    [SerializeField] private List<ParticleSystem> enginesVisuals;
    private void Awake()
    {
        playerRigidbody = GetComponentInParent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        nitroFuelCurrent = nitroFuelMax;
        enginesVisuals.ForEach(e => e.Stop());
    }

    // Update is called once per frame
    void Update()
    {
        bool isPressingNitro = playerInputActions.Player.Nitro.ReadValue<float>() > 0.1f;
        bool hasSuffientAmountOfFuel = nitroFuelCurrent > 0;
        bool doingNitro = isPressingNitro && hasSuffientAmountOfFuel;
        if (doingNitro)
        {
            Vector3 nitroVector = transform.forward * nitroForce;
            playerRigidbody.AddForce(nitroVector, ForceMode.Acceleration);
            enginesVisuals.ForEach(e => e.Play());
            canRechargeFuel = false;
            nitroFuelCurrent -= Time.deltaTime * nitroRegenerationAbility;
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            enginesVisuals.ForEach(e => e.Stop());
            canRechargeFuel = true;
            nitroFuelCurrent += Time.deltaTime * nitroRegenerationAbility;
            if (audioSource.isPlaying)
                audioSource.Stop();
        }

        if (nitroFuelCurrent > nitroFuelMax)
            nitroFuelCurrent = nitroFuelMax;
        if (nitroFuelCurrent < 0)
            nitroFuelCurrent = 0;
    }
}
