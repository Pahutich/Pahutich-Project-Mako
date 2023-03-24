using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform spawnPosition;
    private Ray ray;
    Vector3 mouseWorldPosition;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] private float cooldown;
    private float cooldownTimer;
    [SerializeField] private float overheatThreshold;
    [SerializeField] private float overheatPerShot;
    [SerializeField] private float currentOverheat = 0;
    [SerializeField] private float coolMultiplier;
    private bool inOverheat = false;
    PlayerInputActions playerInputActions;
    private CanonBaseRotator canonBaseRotator;
    bool canShoot = false;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        canonBaseRotator = GetComponent<CanonBaseRotator>();
    }

    void Start()
    {
        cooldownTimer = cooldown;
    }

    void Update()
    {
        ManageShootingCapability();
        float shootInputValue = playerInputActions.Player.Shooting.ReadValue<float>();
            
        if (shootInputValue == 1)
        {
            mouseWorldPosition = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999f, aimColliderLayerMask))
            {
                mouseWorldPosition = hit.point;
                Shoot(mouseWorldPosition);
            }
        }
    }

    private void ManageShootingCapability()
    {
        currentOverheat -= Time.deltaTime * coolMultiplier;
        if (currentOverheat <= 0)
            currentOverheat = 0;
        if (inOverheat)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
        if (inOverheat && currentOverheat <= 0)
        {
            inOverheat = false;
        }
        if (currentOverheat >= overheatThreshold)
        {
            currentOverheat = overheatThreshold;
            inOverheat = true;
        }
        if (cooldownTimer > 0 || currentOverheat >= overheatThreshold || inOverheat)
        {
            cooldownTimer -= Time.deltaTime;
            canShoot = false;
        }
        if (cooldownTimer <= 0 && currentOverheat < overheatThreshold && !inOverheat)
        {
            cooldownTimer = 0f;
            canShoot = true;
        }
    }

    private void Shoot(Vector3 mousePosition)
    {
        if (!canShoot)
            return;
        Vector3 aimDir = (mousePosition - spawnPosition.position).normalized;
        var spawnedProjectile = Instantiate(projectile, spawnPosition.position,
        Quaternion.identity);
        spawnedProjectile.transform.LookAt(mousePosition);
        spawnedProjectile.GetComponentInParent<Rigidbody>().AddForce(aimDir * 50f, ForceMode.Impulse);
        currentOverheat += overheatPerShot;
        canShoot = false;
        cooldownTimer = cooldown;
    }
}
