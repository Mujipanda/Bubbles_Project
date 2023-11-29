using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class door : MonoBehaviour
{
    [Header("drag the script from the button you wish the door responds from")]
    public pressurePlate button;

    [Header("Input the left and right door pivotPoints here \n>left door in 0, right door in 1")]
    [SerializeField]
    private Transform[] doorsPivotPoints;

    [Header("Input the left and right door OpenPoints here \n>left door in 0, right door in 1")]
    [SerializeField]
    private Transform[] doorsOpenPos;

    [SerializeField]
    private GameObject doorCollider;

    private void Start()
    {
        doorCollider.GetComponent<Collider>().enabled = true;
    }
    private void FixedUpdate()
    {
        if(button.buttonActive) 
        {
            doorCollider.GetComponent<Collider>().enabled = false;
            for(int i = 0; i < doorsPivotPoints.Length; i++)
            {

               doorsPivotPoints[i].transform.position = Vector3.MoveTowards(doorsOpenPos[i].transform.position, new Vector3(doorsPivotPoints[i].transform.position.x - 1, doorsPivotPoints[i].transform.position.y, doorsPivotPoints[i].transform.position.z), 2f * Time.deltaTime);
            }
            
        }
    }
}
