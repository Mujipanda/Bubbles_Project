using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    private Rigidbody rb;

    //##___ints___##
    [SerializeField]
    private int maxSpeed;
    private int speedx;// don't touch

    //##___Vectors___##
    private Vector3 vec3, lookVec;

    //##___floats
    [SerializeField]
    [Range(0, 1)] private float joyStickDeadZone;// recomended no more than 50%

    [SerializeField]
    [Range(0.0f, 0.1f)]
    private float acceleration; // of seconds / less is faster

    [SerializeField]
    [Range(1.0f, 5.0f)]
    private float sensitivity;

    private float camX, camY;// redundant 

    //##___bools___##
    private bool delayBool = true;

    //##___GameObjects___##
    public GameObject camObj;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMovement(InputValue direction)
    {
        Vector2 movement = direction.Get<Vector2>();
        vec3 = new Vector3(movement.x, 0, movement.y);

        //rb.AddForce(dirV3 * speed);
    }
    void movementMath()
    {
        if (vec3.x > joyStickDeadZone || vec3.x < -joyStickDeadZone || vec3.z > joyStickDeadZone || vec3.z < -joyStickDeadZone)// if more than deadZone // I dislike how long it is ;( // need to remove there is a deadzone option in the input manger 
        {
            if (speedx < maxSpeed && delayBool) // increase speedx till max speed
            {
                StartCoroutine(speedxDelay());
            }

        }
        else if (vec3.x == 0 || vec3.z == 0) // when still speedx returns to zero
        {
            if (speedx > 0)
            {
                speedx -= 1;
            }
        }
    }
    private IEnumerator speedxDelay()// delay to the speed increase so its consistant
    {
        delayBool = false; // bool prevents the function from running before the previous attempt has finished
        speedx += 1;
        yield return new WaitForSeconds(acceleration);
        delayBool = true;
    }
    void OnLook(InputValue input)// looking joystick
    {
        Vector2 look = input.Get<Vector2>();
        lookVec = new Vector3(look.x, look.y, 0);
       // Debug.Log(lookVec.x + " X " + lookVec.y + " Y ");
    }

    void camLook()// redundant code 
    {
        /*
        if (lookVec.x > 0)
        {
            camX -= sensitivity;
        }
        else if (lookVec.x < 0)
        {
            camX += sensitivity;
        }
        else if(lookVec.y > 0)
        {
            camY -= sensitivity;
        }
        else if(lookVec.y < 0)
        {
            camY += sensitivity;
        }*/
    }
    private void Update()// works every single frame
    {

        movementMath();
        //camLook();
    }

    private void FixedUpdate() // runs incync with each frame/ runs 50 calls a second / 25fps runs twice per frame and so on
    {

        var transVec = transform.rotation * (vec3 * speedx); // turns vec into a transfrom( transforms are local space) 
        rb.velocity = transVec;// move the player

        camObj.transform.localEulerAngles += new Vector3(-lookVec.y * sensitivity, 0, 0);// rotates the camera up and down
        gameObject.transform.localEulerAngles += new Vector3(0, lookVec.x * sensitivity, 0);// rotates the player left and right
       
        

       
    }


}
