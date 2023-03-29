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
    public float currentOverheat = 0;
    [SerializeField] private float coolMultiplier;
    private bool inOverheat = false;
    PlayerInputActions playerInputActions;
    private CanonBaseRotator canonBaseRotator;
    public Vector3 aimDir = Vector3.zero;
    bool canShoot = false;
    private AudioSource audioSource;
    public Action OnOverheatChanged;
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
    public float GetOverheatPercent()
    {
        return (float)currentOverheat / overheatThreshold;
    }
    private void ManageShootingCapability()
    {
        float overheatCooldownMultiplier;

        if (inOverheat && currentOverheat <= 0)
        {
            inOverheat = false;
        }
        currentOverheat -= Time.deltaTime * coolMultiplier;
        if (currentOverheat <= 0)
            currentOverheat = 0;
        if (currentOverheat >= overheatThreshold)
        {
            currentOverheat = overheatThreshold;
            inOverheat = true;
        }
        if (inOverheat)
        {
            overheatCooldownMultiplier = coolMultiplier;
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            overheatCooldownMultiplier = coolMultiplier;
            audioSource.Stop();
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
        OnOverheatChanged?.Invoke();
    }

    private void Shoot(Vector3 mousePosition)
    {
        if (!canShoot)
            return;
        aimDir = (mousePosition - spawnPosition.position).normalized;
        var spawnedProjectile = Instantiate(projectile, spawnPosition.position,
        Quaternion.identity);
        spawnedProjectile.transform.LookAt(mousePosition);
        spawnedProjectile.GetComponentInChildren<Projectile>().OnShot(aimDir);
        currentOverheat += overheatPerShot;
        canShoot = false;
        cooldownTimer = cooldown;
    }
}
