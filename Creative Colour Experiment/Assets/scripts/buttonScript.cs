using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour
{

    public bool buttonIsPressed = false;
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonIsPressed )
        {
            //gameObject.transform.position += Vector3.forward * Mathf.Sin(1);
            //gameObject.transform.position += -Vector3.up * Mathf.Sin(lerpedValue) / 5;
        }
    }
}
