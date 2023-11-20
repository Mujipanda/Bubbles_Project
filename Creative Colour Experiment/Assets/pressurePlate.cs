using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{
    public GameObject plate;
    public bool buttonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        int layerMask = 1 << 3;
        Debug.DrawLine(new Vector3(0,0,0), new Vector3(0,4,0), Color.white);
        if (Physics.Raycast(transform.position, transform.up, out hit, 5, layerMask))
        {

            buttonPressed = true;
            
            //Debug.DrawLine(transform.position, hit.point, Color.green);
            
            Debug.Log("pressureplate pressed");
        }
        else 
            buttonPressed = false;
    }
}
