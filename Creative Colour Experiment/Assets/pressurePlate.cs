using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{
    public GameObject plate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        int layerMask = 3 << 0;
        if (Physics.Raycast(plate.transform.position, plate.transform.up, out hit, 0.5f, layerMask))
        {
            Debug.DrawLine(plate.transform.position, plate.transform.up);
            Debug.Log("pressureplate pressed");
        }
    }
}
