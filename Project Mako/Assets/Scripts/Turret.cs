using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private const string PLAYERTAG = "Player";

    [Header("Attributes")]

    [SerializeField] private float range = 15f;
    [SerializeField] private float fireRate = 1f;

    [Header("Unity setup fields")]

    [SerializeField] private float turnSpeed = 10f;
    private float fireCountdown = 0f;
    private float distanceToPlayer;
    private GameObject player;
    private bool playerInRange = false;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(PLAYERTAG);
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }
    private void UpdateTarget()
    {
        if(player == null)
        return;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= range)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;

        if (playerInRange)
        {
            Vector3 dir = player.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0, rotation.y, 0);

            if(fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f/fireRate;
                
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = bulletGO.GetComponent<Projectile>();

        if(projectile != null)
        projectile.OnShot(player.transform.position - transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
