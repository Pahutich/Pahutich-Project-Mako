using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance {get; private set;}
    private const string VERTICALINPUTAXIS = "Vertical";
    private const string HORIZONTALINPUTAXIS = "Horizontal";
    [field: SerializeField] public float VerticalInput {get; private set;}
    [field: SerializeField] public float HorizontalInput {get; private set;}
    public bool IsShooting {get; set;} = false;
    // Start is called before the first frame update
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        VerticalInput = Input.GetAxis(VERTICALINPUTAXIS);
        HorizontalInput = Input.GetAxis(HORIZONTALINPUTAXIS);
        if(Input.GetMouseButton(0) == true)
        {
            IsShooting = true;
        }
        else
        {
            IsShooting = false;
        }
    }
}
