using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalHealth : Health
{
    // Start is called before the first frame update
    void Awake()
    {
        base.SetupHealthObject();
    }
}
