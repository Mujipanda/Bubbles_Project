using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private int maxSpeed;

    private int speedx;// don't touch

    private Vector3 vec3, lookVec;
    [SerializeField]
    [Range(0, 1)] private float joyStickDeadZone;// recomended no more than 50%

    [SerializeField]
    [Range(0.0f, 0.1f)]
    private float acceleration; // of seconds / less is faster

    private bool delayBool = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }



    void OnMovement(InputValue direction)
    {

        Vector2 movement = direction.Get<Vector2>();

        //Vector3 dirV3 = new Vector3(movement.x, 0f, movement.y);

        vec3 = new Vector3(movement.x, 0, movement.y);

        //rb.AddForce(dirV3 * speed);

        //  rb.velocity = dirV3 * speed;
        //Debug.Log(vec3.x + " X");
        //Debug.Log(vec3.z + " Z");
    }
    private void FixedUpdate() // runs incync with each frame/ runs 50 calls a second / 25fps runs twice per frame and so on
    {
        //Debug.Log(vec3.x);
        // rb.velocity = vec3 * speedx; // move the player
        var transVec = transform.rotation * (vec3 * speedx);

    
        // rb.AddForce(vec3 * speedx);
        rb.velocity = transVec;

    }

    void movementMath()
    {
        // Debug.Log(speedx + " speedx");

        /* if(speedx < 15)
         {
             if (vec3.x != 0 || vec3.z != 0)
             {
                 speedx += 1;
             }
         }
         else if (vec3.x == 0 || vec3.z == 0)
         {
             if (speedx > 0)
             {
                 speedx -= 1;
             }
         }*/

        if (vec3.x > joyStickDeadZone || vec3.x < -joyStickDeadZone || vec3.z > joyStickDeadZone || vec3.z < -joyStickDeadZone)
        {
            if (speedx < maxSpeed && delayBool) // increase speedx till max speed
            {
                StartCoroutine(speedxDelay());
            }

        }

        else if (vec3.x == 0 || vec3.z == 0) // when still speedz returns to zero
        {
            if (speedx > 0)
            {
                speedx -= 1;
            }
        }

        // Debug.Log(speedx);
        //Debug.Log(vec3);

        /* if (vec3.x != 0 || vec3.z != 0) // when moving increases speedx /less optimal but prevents bugs 
 {
     if (speedx < maxSpeed) // increase speedx till max speed
     {
         speedx += 1;
     }
 }*/
    }

    private IEnumerator speedxDelay()
    {
        delayBool = false;
        speedx += 1;
        yield return new WaitForSeconds(acceleration);
        delayBool = true;
    }

    private void Update()// works every single frame
    {
        movementMath();
    }


    void OnLook(InputValue pos)
    {
        Vector2 vecPos = pos.Get<Vector2>();

        lookVec = new Vector3(vecPos.x, vecPos.y);
        Debug.Log("right analog stick moving");
    }
}
