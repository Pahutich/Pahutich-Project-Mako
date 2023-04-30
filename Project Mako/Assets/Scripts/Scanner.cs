using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private Vector3 mouseWorldPosition;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] private HealthBar healthBarOfScannedEnemy;
    [SerializeField] private GameObject scannerPanel;
    private void Awake()
    {
        scannerPanel.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

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
            //Debug.Log(hit.collider.gameObject);
            if (hit.collider.gameObject.GetComponentInParent<Health>() != null)
            {
                Debug.Log("scanned enemy");
                healthBarOfScannedEnemy.ReconfigureHealthHolder(scannedEnemy);
                scannerPanel.SetActive(true);
                //Debug.Log("sacnning....");
            }
            else
            {
                scannerPanel.SetActive(value: false);
            }
        }
    }
}
