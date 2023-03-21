using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBaseRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask layerMask;
    private Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit,float.MaxValue, layerMask))
        {
            Vector3 raycastVector = raycastHit.point;
            Vector3 aimPos = raycastVector;
            aimPos.y = transform.position.y;
            Vector3 lookDirection = (aimPos - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, lookDirection, Time.deltaTime * rotationSpeed);
        }
    }
}
