using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class waterBoyency : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 objPos = Vector3.zero;

    private Vector3 objDimension;

    [SerializeField]
    private Transform[] cornerTransforms;

    private float[] lastPosition = new float[] { 1, 1, 1, 1 };
    private float[] distance = new float[] { 1, 1, 1, 1 };
    private float[] damp = new float[] { 1, 1, 1, 1 };
    private float[] cornervelocity = new float[] { 1, 1, 1, 1 };
    private float[] boyancyForce = new float[] { 1, 1, 1, 1 };
    private Vector3[] corners = new Vector3[] {Vector3.zero, Vector3.zero , Vector3.zero , Vector3.zero }; // declares the Size of the Vector
    public float boyancyForceMultiplyer;
    public float dampForce;
    private bool inWater = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        objDimension.x = GetComponent<Renderer>().bounds.size.x;
        objDimension.z = GetComponent<Renderer>().bounds.size.z;
        objDimension.y = GetComponent<Renderer>().bounds.size.y;

    }
    void Start()
    {
        objPos = gameObject.GetComponent<Renderer>().bounds.center;

        corners[0].x = objPos.x - objDimension.x / 2; 
        corners[0].z = objPos.z - objDimension.z / 2;
        corners[0].y = objPos.y;

        corners[1].x = objPos.x + objDimension.x / 2;
        corners[1].z = objPos.z - objDimension.z / 2;
        corners[1].y = objPos.y;

        corners[2].x = objPos.x - objDimension.x / 2;
        corners[2].z = objPos.z + objDimension.z / 2;
        corners[2].y = objPos.y;

        corners[3].x = objPos.x + objDimension.x / 2;
        corners[3].z = objPos.z + objDimension.z / 2;
        corners[3].y = objPos.y;
        for(int i = 0; i < corners.Length; i++)
        {
            cornerTransforms[i].position = corners[i];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Water")
        {
            inWater = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Water")
        {
            inWater = false;
        }
    }
    private void FixedUpdate()
    {
        
        int layerMask = 1 << 8;
        
        
        RaycastHit hit;
        
        if(inWater)
        {
            initCorners();
            for (int i = 0; i < corners.Length; i++)
            {
                Physics.Raycast(corners[i], new Vector3(0, -1, 0), out hit, Mathf.Infinity, layerMask);
                Debug.DrawLine(corners[i], new Vector3(corners[i].x, corners[i].y - hit.distance, corners[i].z), Color.blue);
                lastPosition[i] = distance[i];
                distance[i] = hit.distance;
                cornervelocity[i] = (lastPosition[i] - distance[i]); /// Time.fixedDeltaTime;
                boyancyForce[i] = (rb.mass * boyancyForceMultiplyer) / (hit.distance);
                damp[i] = (dampForce * cornervelocity[i]);
                //float boyancyForce = (rb.mass * 10) / (hit.distance * hit.distance);
                float boyancySpeed = boyancyForce[i] + damp[i];// + damp;
                boyancySpeed = Mathf.Clamp(boyancySpeed, 0.1f, 100f);
                rb.AddForceAtPosition(new Vector3(0, 1, 0) * boyancySpeed, corners[i]);
            }
        }
        
       
       
        /*rb.AddForceAtPosition(new Vector3(0, 1, 0) * boyancyForce, leftBottomCorner);
        rb.AddForceAtPosition(new Vector3(0, 1, 0) * boyancyForce, rightBottomCorner);
        rb.AddForceAtPosition(new Vector3(0, 1, 0) * boyancyForce, leftTopCorner);
        rb.AddForceAtPosition(new Vector3(0, 1, 0) * boyancyForce, rightTopCorner);
        */
       
    }

    void initCorners()
    {
        
        for (int i = 0; i < corners.Length; i++)
        {
            objPos = gameObject.GetComponent<Renderer>().bounds.center;// get the models centre because default is not centre
            corners[i] = cornerTransforms[i].position;
        }
        /*corners[0].x = objPos.x - objDimension.x / 2;
        corners[0].z = objPos.z - objDimension.z / 2;
        corners[0].y = objPos.y - objDimension.y;

        corners[1].x = objPos.x + objDimension.x / 2;
        corners[1].z = objPos.z - objDimension.z / 2;
        corners[1].y = objPos.y;

        corners[2].x = objPos.x - objDimension.x / 2;
        corners[2].z = objPos.z + objDimension.z / 2;
        corners[2].y = objPos.y;

        corners[3].x = objPos.x + objDimension.x / 2;
        corners[3].z = objPos.z + objDimension.z / 2;
        corners[3].y = objPos.y;
        */
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector3(corners[0].x, corners[0].y, corners[0].z), 0.5f );
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(corners[1].x, corners[1].y, corners[1].z), 0.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(new Vector3(corners[2].x, corners[2].y, corners[2].z), 0.5f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(corners[3].x, corners[3].y, corners[3].z), 0.5f);
    }
}
