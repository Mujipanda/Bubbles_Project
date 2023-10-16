using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprayGun : MonoBehaviour
{
    [SerializeField]
    private Transform gunTransform, chaseTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void rayCastingGunHit()
    {
        RaycastHit hit;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        gunTransform.transform.position = new Vector3(chaseTransform.transform.position.x, chaseTransform.transform.position.y, chaseTransform.transform.position.z + 1);
    }
}
