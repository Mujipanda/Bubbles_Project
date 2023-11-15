using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{

    // Start is called before the first frame update

    public GameObject platForm;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            platForm.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
