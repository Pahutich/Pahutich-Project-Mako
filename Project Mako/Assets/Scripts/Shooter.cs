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
    // Start is called before the first frame update
    void Start()
    {
        cooldownTimer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            cooldownTimer = 0f;
        }
        if (PlayerInput.Instance.IsShooting)
        {
            if(cooldownTimer > 0)
            return;
            mouseWorldPosition = Vector3.zero;
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
            {
                mouseWorldPosition = raycastHit.point;
            }
            Shoot(mouseWorldPosition);
        }
    }

    private void Shoot(Vector3 mp)
    {
        Vector3 aimDir = (mp - spawnPosition.position).normalized;
        var p = Instantiate(projectile, spawnPosition.position, Quaternion.LookRotation(
            transform.TransformDirection(new Vector3(-90, 0, 0)), Vector3.back));
        p.GetComponentInChildren<Projectile>().target = aimDir;
        p.GetComponentInChildren<Rigidbody>().AddForce(spawnPosition.right * 50f, ForceMode.Impulse);
        PlayerInput.Instance.IsShooting = false;
        cooldownTimer = cooldown;
    }
}
