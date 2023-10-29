using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GravGun : MonoBehaviour
{
    ControlsInputs controls;

    private new Rigidbody rb;

    private RigidbodyInterpolation initialInterpolationSetting;

    private const float maxGrabDistance = 30;

    private Vector3 rotationDifferenceEuler;

    private Vector3 hitOffsetLocal;

    private float currentGrabDistance;

    private Vector2 rotationInput;


    private bool canRotate = false;
    private void Awake()
    {
        controls = new ControlsInputs();
        controls.Enable();
    }

    private Ray CenterRay()
    {
        return Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
    }
   
    void Update()
    {
     
        if(controls.Player.Pickup.IsPressed())
        {
            if(rb != null)
            {
                rb.interpolation = initialInterpolationSetting;
                rb = null;
            }
        }
        return;
        if( rb == null )
        {
            Ray ray = CenterRay();
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * maxGrabDistance, Color.blue, 0.01f);

            if (Physics.Raycast(ray, out hit, maxGrabDistance))
            {
                // Don't pick up kinematic rigidbodies (they can't move)
                if (hit.rigidbody != null && !hit.rigidbody.isKinematic)
                {
                    // Track rigidbody's initial information
                    rb = hit.rigidbody;
                    initialInterpolationSetting = rb.interpolation;
                    rotationDifferenceEuler = hit.transform.rotation.eulerAngles - transform.rotation.eulerAngles;

                    hitOffsetLocal = hit.transform.InverseTransformVector(hit.point - hit.transform.position);

                    currentGrabDistance = Vector3.Distance(ray.origin, hit.point);

                    // Set rigidbody's interpolation for proper collision detection when being moved by the player
                    rb.interpolation = RigidbodyInterpolation.Interpolate;
                }
            }
        }
        else
        {
            if(controls.Player.Rotate.IsPressed())
            {
                canRotate = true;
            }
            else 
                canRotate = false;
        }
    }


    void OnLook(InputValue input)
    {
        Vector2 look = input.Get<Vector2>();
        rotationInput += new Vector2(look.x, look.y);
    }
}
