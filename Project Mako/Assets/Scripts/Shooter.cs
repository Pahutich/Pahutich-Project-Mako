using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectilesPool))]
public class Shooter : MonoBehaviour
{
    private bool inOverheat = false;
    private bool canShoot = false;
    private float cooldownTimer;
    private Vector3 mouseWorldPosition;
    private Vector3 aimDir = Vector3.zero;
    private Ray ray;
    private AudioSource audioSource;
    private PlayerInputActions playerInputActions;
    private CanonBaseRotator canonBaseRotator;
    private ProjectilesPool projectilesPool;
    [SerializeField] private float cooldown;
    [SerializeField] private float overheatThreshold;
    [SerializeField] private float overheatPerShot;
    [SerializeField] private float coolMultiplier;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private LayerMask aimColliderLayerMask;
    public float currentOverheat = 0;
    public Action OnOverheatChanged;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerInputActions = new PlayerInputActions();
        canonBaseRotator = GetComponent<CanonBaseRotator>();
        projectilesPool = GetComponent<ProjectilesPool>();
        playerInputActions.Player.Enable();
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
        aimDir = (mousePosition - spawnPosition.gameObject.transform.position).normalized;
        var spawnedProjectile = projectilesPool.GetPooledProjectiles();
        if (spawnedProjectile)
        {
            spawnedProjectile.transform.position = spawnPosition.position;
            spawnedProjectile.transform.rotation = Quaternion.identity;
            spawnedProjectile.SetActive(true);
            //workaround for bullet having children
            if (spawnedProjectile.transform.childCount > 0)
            {
                foreach (Transform child in spawnedProjectile.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
            spawnedProjectile.transform.LookAt(aimDir);
            spawnedProjectile.GetComponentInChildren<Projectile>().OnShot(aimDir);
            currentOverheat += overheatPerShot;
            canShoot = false;
            cooldownTimer = cooldown;
        }
    }
}
