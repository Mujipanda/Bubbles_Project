using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup2 : MonoBehaviour
{
    [SerializeField] Transform holdzone;
    private Gameobject heldobject;
    private RigidBody heldobjectRB;

    [SerializeField] private float PickupRange = 5.0f;
    [SerializeField] private float PickupForce = 150.0f;
    // Update is called once per frame
    private void Update()
    {
        if (input.GetKeyDown(KeyCode.E))
        {
            if (heldobject == null)
            {
                RaycastHit hit;
                if (physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, PickupRange))
                {
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }

        if (heldobject != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldobject.transform.position, holdzone.position) < 0.1f)
        {
            Vector3 moveDirection = (holdzone.position - heldobject.transform.position);
            heldobjectRB.AddForce(moveDirection * PickupForce);
        }
    }

    void PickupObject(GameObject PickObject)
    {
        if (PickObject.GetComponent<Rigidbody>())
        {
            heldobjectRB = PickObject.GetComponent<Rigidbody>();
            heldobjectRB.useGravity = false;
            heldobjectRB.drag = 10;
            heldobjectRB.constraints = RigidbodyConstraints.FreezeRotation;

            heldobjectRB.transform.parent = holdzone;
            heldobject = PickObject;
        }
    }

    void DropObject()
    {
        heldobjectRB.useGravity = true;
        heldobjectRB.drag = 1;
        heldobjectRB.constraints = RigidbodyConstraints.None;

        heldobject.transform.parent = null;
        heldobject = null;
    }
}
