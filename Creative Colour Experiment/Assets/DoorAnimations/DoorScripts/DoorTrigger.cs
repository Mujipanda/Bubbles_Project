using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject door;

    /*bool isOpened = false;

   void OnTriggerEnter(Collider col)
   {
       if(!isOpened)
       {
           isOpened = true;
	       door.transform.position += new Vector3 (0,4,0);
       }
   }
}*/


    bool isOpened = false;

    public float doorCooldown = 5;



    private void OnTriggerEnter(Collider col)

    {

        if (!isOpened)

        {

            isOpened = true;

            door.transform.position += new Vector3(0, 4, 0);

        }

    }

    void OnTriggerExit(Collider other)

    {

        if (isOpened)

        {

            isOpened = false;

            door.transform.position -= new Vector3(0, 4, 0);

        }

    }

}