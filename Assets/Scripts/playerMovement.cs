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


    [SerializeField]
    [Range(1, 5)]
    private int followDelay;
    //##___Vectors___##
    private Vector3 vec3, lookVec, jumpVec3;

    //##___floats
    [SerializeField]
    [Range(0, 1)] private float joyStickDeadZone;// recomended no more than 50%

    [SerializeField]
    [Range(0.0f, 0.1f)]
    private float acceleration; // of seconds / less is faster

    [SerializeField]
    [Range(1.0f, 5.0f)]
    private float sensitivity;

    [SerializeField]
    [Range(0, 10)]
    private int gravity, jumpHighet;

    [SerializeField]
    [Range(0, 3)]
    private float playerHight;// should be set to be just a bit more than the player (use the green debug option in the raycast method to see how big the raycast is)

    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float sinX, sinY, lerpedValue, duration;// redundant 

    //private float timeElapsed = 0;

    //##___bools___##
    private bool delayBool = true, coroBool = true;
    public bool allowjump = false, isFalling = false, isJumping = false, cancelJump = false;

    //##___GameObjects___##
    public GameObject camObj;


    public Transform target;
    public Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Application.targetFrameRate = 60;
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

    void OnJump()
    {
        //Debug.Log("Jump");

        if (allowjump)
            StartCoroutine(lerpJump(-1, 1));

        /*switch (allowjump)
        {
            case true:
                for (int i = 0; i < 3; i++)
                {
                    gameObject.transform.position += Vector3.up * 1;
                }
                //gameObject.transform.position += Vector3.up * jumpHighet; // move player up in the y axis
                break;
        }*/
    }

    IEnumerator lerpJump(float start, float end)
    {

        isJumping = true;
        allowjump = false;
        isFalling = false;
        cancelJump = false;
        float timeElapsed = 0;

        while (timeElapsed < duration && !cancelJump)
        {
            float t = timeElapsed / duration;
            lerpedValue = Mathf.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;
            jumpVec3 = new Vector3(0, Mathf.Sin(lerpedValue) / 10, 0);
            gameObject.transform.position += -Vector3.up * Mathf.Sin(lerpedValue) / 10;
            yield return new WaitForEndOfFrame();
        }
        cancelJump = false;
        // isFalling = true;
        isJumping = false;
        lerpedValue = end;
    }

    void SinGraphJump()
    {
        /* if (timeElapsed < duration)
         {
             float t = timeElapsed / duration;
             lerpedValue = Mathf.Lerp(0, 1, t);
             timeElapsed += Time.deltaTime;
             Debug.Log(lerpedValue);

         }
         else if (timeElapsed > duration)
         {
             float t = timeElapsed / duration;
             lerpedValue = Mathf.Lerp(1, 0, t);
             timeElapsed -= Time.deltaTime;
             gameObject.transform.position += -Vector3.up * Mathf.Sin(lerpedValue) / 25;

             //lerpedValue = 1;
         }
         else
             lerpedValue = 1;
        */
    }
    void fixedGravity()
    {
        switch (isFalling)// makes sure the player is just about hovering over the ground to avoid friction/ collision with the group
        {
            case true:
                gameObject.transform.position += -Vector3.up * 0.1f;// constant gravity downwards
                break;
        }
    }

    void rayCasting()
    {
        //if(coroBool)
        //  StartCoroutine(rayCastDelay());
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, playerHight))
        {
            // Debug.Log("true");

            //cancelJump = true;
            allowjump = true;

            isFalling = false;
            // StartCoroutine(cancelJumpDelay());

        }
        else if (!isJumping)
        {
            isFalling = true;
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * playerHight, Color.green);
    }

    /*IEnumerator rayCastDelay()
    {
        coroBool = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, playerHight))
        {
            // Debug.Log("true");
            cancelJump = true;
            allowjump = true;

            isFalling = false;
            // StartCoroutine(cancelJumpDelay());

        }
        else if (!isJumping)
        {
            isFalling = true;
        }

        yield return new WaitForSeconds(0.1f);
        
        coroBool = true;
    }*/

    private void Update()// works every single frame
    {
        //movementMath();// calculates SpeedX
        rayCasting();


        //SinGraphJump();
    }

    private void FixedUpdate() // runs incync with each frame/ runs 50 calls a second / 25fps runs twice per frame and so on
    {
        var transVec = transform.rotation * (vec3 * 10); // turns vec into a transform( transforms are local space) Replace 10 with speedx to use movementMath method
        //Debug.Log(vec3);
        //rb.velocity = transVec;

        //rb.AddForce(transVec, ForceMode.VelocityChange);// moves the player( applys the transform to the rigidbody) 
        gameObject.transform.position += transVec * Time.deltaTime;

        camObj.transform.localEulerAngles += new Vector3(-lookVec.y * sensitivity, lookVec.x * sensitivity, 0);// rotates the camera up and down***
        gameObject.transform.localEulerAngles += new Vector3(0, lookVec.x * sensitivity, 0);// rotates the player left and right**
        camObj.transform.position = target.transform.position;// camera follow chase object

        target.transform.position = Vector3.SmoothDamp(target.transform.position, gameObject.transform.position, ref velocity, followDelay * Time.deltaTime);// creates a damped interpolrant between the actual player and what the camerea follows

        
        fixedGravity();

    }


}
