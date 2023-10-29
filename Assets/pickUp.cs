using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class pickUp : MonoBehaviour
{
    [SerializeField] Transform holdzone;
    private GameObject heldobject;
    private Rigidbody heldobjectRB;

    ControlsInputs controls;

    [SerializeField] private float PickupRange = 5.0f;
    [SerializeField] private float PickupForce = 150.0f;
    private bool isPressingButton = false;


    private void Awake()
    {
        controls = new ControlsInputs();
        controls.Enable();

    }


    void OnPickup()
    {
        //Debug.Log("pickUp");

    }
    // Update is called once per frame
    /*private void Update()
    {
        if (input.GetKeyDown(KeyCode.E))
        {
            if (heldobject == null)
            {
                RaycastHit hit;
                if (physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, PickupRange))
                {
                    PickupObject(hit.Transform.GameObject);
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
    }*/



    private void FixedUpdate()
    {
        if (controls.Player.Pickup.IsPressed())// simulates holding the button // keymaped to E on keyboard and B on controler
        {
            isPressingButton = true;
        }
        else
            isPressingButton = false;

        if (isPressingButton)
        {

            if (heldobject == null)
            {
                RaycastHit hit;
                //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, PickupRange))
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)) // removed range 
                {
                    
                    PickupObject(hit.transform.gameObject);
                    Debug.DrawLine(transform.position, hit.transform.position, Color.red);// does not work correctly
                    //Debug.Log(hit.distance);// ignore this

                }
                //else
                //DropObject();
            }
            //if (heldobject != null)
            //MoveObject();
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