using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class ProjectilesPool : MonoBehaviour
{
    private List<GameObject> pooledProjectiles = new List<GameObject>();
    [SerializeField] private int numberToPool = 12;
    [SerializeField] private GameObject projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateProjectiles();
    }

    private void InstantiateProjectiles()
    {
        for (int i = 0; i < numberToPool; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            pooledProjectiles.Add(projectile);
        }
    }

    public GameObject GetPooledProjectiles()
    {
        for (int i = 0; i < pooledProjectiles.Count; i++)
        {
            if (!pooledProjectiles[i].activeInHierarchy)
            {
                return pooledProjectiles[i];
            }
        }
        return null;
    }
}
