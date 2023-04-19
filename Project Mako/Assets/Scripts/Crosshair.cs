using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Crosshair : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        // Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        transform.position = Mouse.current.position.ReadValue();
    }
}
