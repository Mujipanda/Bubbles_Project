using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlate : MonoBehaviour
{

    [Header(" drag the object called plateCentre here")]
    [SerializeField]
    private GameObject plate;

    private Vector3 platePos;

    [Header("The green line seen in the inspector during play mode is controled by this .\n If you're not sure just set the value to 1.8")]
    [Range(0f, 2f)]
    [SerializeField]
    private float presurelateDetectionDistance;

    //[Header("no need to touch")]
    [System.NonSerialized]
    public bool buttonActive = false;

    [Header("When at default scale set to 0.5f\n lower value if you shrink the scale and vice versa \n A good number to go with would be 0.25f")]
    [Range(0f,0.5f)]
    [SerializeField]
    private float buttonTravelDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        platePos = plate.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(platePos, new Vector3(platePos.x, platePos.y + presurelateDetectionDistance, platePos.z),  Color.green);
        RaycastHit hit;
        int layerMask = 1 << 3;
        Physics.Raycast(platePos, Vector3.up, out hit, presurelateDetectionDistance, layerMask);
        if(hit.collider != null )
        {
           
            //Debug.DrawLine(platePos, plate.transform.up, Color.green);
            
            plate.transform.position = Vector3.MoveTowards(plate.transform.position, new Vector3(platePos.x, platePos.y - buttonTravelDistance, platePos.z), 0.5f * Time.deltaTime);
            buttonActive = true;
        }
        else
        {

            plate.transform.position = Vector3.MoveTowards(plate.transform.position, platePos, buttonTravelDistance * Time.deltaTime);
            buttonActive = false;
        }
    }
           
}
