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
    [SerializeField] private List<GameObject> enginesVisuals;
    private void Awake()
    {
        playerRigidbody = GetComponentInParent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        nitroFuelCurrent = nitroFuelMax;
        enginesVisuals.ForEach(e => e.SetActive(false));
    }

    // Update is called once per frame
    void Update()
    {
        bool isNitroing = playerInputActions.Player.Nitro.ReadValue<float>() > 0.1f;
        bool hasSuffientAmountOfFuel = nitroFuelCurrent > 0;
        if (isNitroing && hasSuffientAmountOfFuel)
        {
            Vector3 nitroVector = transform.forward * nitroForce;
            playerRigidbody.AddForce(nitroVector, ForceMode.Acceleration);
            enginesVisuals.ForEach(e => e.SetActive(true));
            canRechargeFuel = false;
            nitroFuelCurrent -= Time.deltaTime * nitroRegenerationAbility;
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            enginesVisuals.ForEach(e => e.SetActive(false));
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
