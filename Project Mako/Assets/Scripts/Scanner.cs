using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private Vector3 mouseWorldPosition;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] private HealthBar healthBarOfScannedEnemy;
    [SerializeField] private TextMeshProUGUI enemyName;
    [SerializeField] private GameObject scannerPanel;
    private void Awake()
    {
        scannerPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        mouseWorldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 999f, aimColliderLayerMask))
        {
            var scannedEnemy = hit.collider.gameObject.GetComponentInParent<Health>();
            if (hit.collider.gameObject.GetComponentInParent<Health>() != null)
            {
                enemyName.text = scannedEnemy.GetHealthSystem().GetHolder();
                healthBarOfScannedEnemy.ReconfigureHealthHolder(scannedEnemy);
                scannerPanel.SetActive(true);
            }
            else
            {
                scannerPanel.SetActive(value: false);
            }
        }
    }
}
