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
    PlayerInputActions playerInputActions;
    bool canShoot = false;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    void Start()
    {
        cooldownTimer = cooldown;
    }

    void Update()
    {
        float shootInputValue = playerInputActions.Player.Shooting.ReadValue<float>();
        if (shootInputValue == 1)
        {
            mouseWorldPosition = Vector3.zero;
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
            {
                mouseWorldPosition = raycastHit.point;
            }
            Shoot(mouseWorldPosition);
        }
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            canShoot = false;
        }
        else
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
        var spawnedProjectile = Instantiate(projectile, spawnPosition.position, Quaternion.LookRotation(
            transform.TransformDirection(new Vector3(-90, 0, 0)), Vector3.back));
        spawnedProjectile.GetComponentInChildren<Projectile>().target = aimDir;
        spawnedProjectile.GetComponentInChildren<Rigidbody>().AddForce(spawnPosition.right * 50f, ForceMode.Impulse);
        canShoot = false;
        cooldownTimer = cooldown;
    }
}
