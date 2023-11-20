using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class door : MonoBehaviour
{
    [SerializeField]
    private GameObject[] doors;
    [SerializeField]
    private Transform[] openPosition;

    private bool openDoor = false;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag =="Player")
        {
            openDoor = true;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            openDoor= false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (openDoor)
            openDoors();
        else
            closeDoors();
    }

    void openDoors()
    {
        //doors[0].transform.position = Vector3.SmoothDamp(doors[0].transform.position, openPosition[0].transform.position, ref velocity, 10 * Time.deltaTime);
        //doors[1].transform.position = Vector3.SmoothDamp(doors[1].transform.position, openPosition[0].transform.position, ref velocity, 10 * Time.deltaTime);

    }
    void closeDoors()
    {
        
    }
}
