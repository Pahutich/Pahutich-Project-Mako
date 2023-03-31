using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    private Rigidbody jumpingRigidbody;
    private PlayerInputActions playerInputActions;
    private void Awake() {
        jumpingRigidbody = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //bool isJumping = playerInputActions.Player.Jump.triggered;
        bool isJumping = playerInputActions.Player.Jump.ReadValue<float>() > 0.1f;
        Debug.Log(isJumping);
        if(isJumping)
        {
            jumpingRigidbody.AddForce(transform.InverseTransformDirection(Vector3.up) * jumpForce, ForceMode.Force);
        }
    }
}
