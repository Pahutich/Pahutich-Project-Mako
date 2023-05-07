using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [HideInInspector] public string objectID;

    private void Awake()
    {
        objectID = name + transform.position.ToString() + transform.eulerAngles.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        var DontDestroyObjects = Object.FindObjectsOfType<DontDestroy>();
        for (int i = 0; i < DontDestroyObjects.Length; i++)
        {
            if (DontDestroyObjects[i] != this)
            {
                if (DontDestroyObjects[i].objectID == objectID)
                {
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    private void OnDestroy()
    {
        Debug.Log("taht's how i lose reference");
    }
}
