using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class playerMovement : MonoBehaviour
{

    private Rigidbody rb;

    //##___ints___##
    [Header("Movement Vars")]
    [SerializeField]
    private int maxSpeed;
    // private int speedx;// don't touch

    [SerializeField]
    [Range(1, 10)]
    private int followDelay, camFollowDelay;
    //##___Vectors___##
    private Vector3 vec3, lookVec;

    //##___floats
    //[SerializeField]
    // [Range(0, 1)] private float joyStickDeadZone;// recomended no more than 50%

    // [SerializeField]
    //  [Range(0.0f, 0.1f)]
    // private float acceleration; // of seconds / less is faster

    [SerializeField]
    [Range(50, 200)]
    private int sensitivity;

    //[SerializeField]
    // [Range(0, 10)]
    //private int gravity, jumpHighet;

    [SerializeField]
    [Range(0, 3)]
    private float playerHight;// should be set to be just a bit more than the player (use the green debug option in the raycast method to see how big the raycast is)

    // [SerializeField]
    // private float jumpSpeed;
    // [SerializeField]
    //private float sinX, sinY, lerpedValue2;// redundant 

    private float duration = 1.0f, lerpedValue;

    //private bool boostJumpIsActive = false;
    //private float timeElapsed = 0;

    //##___bools___##
    // private bool delayBool = true;//, coroBool = true;

    private bool allowjump = false, isFalling = false, isJumping = false;

    private float camX = 0f;
    private float camY = 0f;
    private float camAngleY = 0f;
    private float camAngleX = 0f;   
    //##___GameObjects___##

    public GameObject camObj;
    public PauseMenu pauseScript;

    public Transform target;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Application.targetFrameRate = 60; // changing fps changes jump hight
        pauseScript = GetComponent<PauseMenu>();
    }

    void OnMovement(InputValue direction)
    {
        Vector2 movement = direction.Get<Vector2>();
        vec3 = new Vector3(movement.x, 0, movement.y);

        //rb.AddForce(dirV3 * speed);
    }
    /*void movementMath()
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
     }*/

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


    }

    IEnumerator lerpJump(float start, float end)
    {

        isJumping = true;
        allowjump = false;
        isFalling = false;
       
        float timeElapsed = 0;
        RaycastHit hit;
        while (timeElapsed < duration)
        {

            float t = timeElapsed / duration;
            lerpedValue = Mathf.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;
            //jumpVec3 = new Vector3(0, Mathf.Sin(lerpedValue) / 10, 0);
            if (!pauseScript.gamePaused)
                gameObject.transform.position += -Vector3.up * Mathf.Sin(lerpedValue) /5;

            yield return new WaitForEndOfFrame();

            //if (lerpedValue > 0 && cancelJump)
            //{
            //lerpedValue = -1;


            //}
            // have to do another ray cast because method is running in paralell 
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, playerHight) && lerpedValue > 0.2f)
            {
                timeElapsed = 1; // stops the while loop

            }
        }

        isJumping = false;
        // isFalling = true;

        lerpedValue = end;
    }

    void fixedGravity()
    {
        switch (isFalling)// makes sure the player is just about hovering over the ground to avoid friction/ collision with the group
        {
            case true:
                gameObject.transform.position += -Vector3.up * 0.3f;// constant gravity downwards
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

            // cancelJump = true;
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

    private void Update()// works every single frame
    {
        switch (pauseScript.gamePaused)
        {
            case false:
                rayCasting();
                break;
        }



        //SinGraphJump();
    }

    private void FixedUpdate() // runs incync with each frame/ runs 50 calls a second / 25fps runs twice per frame and so on
    {
        switch (pauseScript.gamePaused)
        {
            case false:
                var transVec = transform.rotation * (vec3 * maxSpeed); // turns vec into a transform( transforms are local space) Replace 10 with speedx to use movementMath method
                                                                       //Debug.Log(vec3);
                                                                       //rb.velocity = transVec;

                //rb.AddForce(transVec, ForceMode.VelocityChange);// moves the player( applys the transform to the rigidbody) 

                // gameObject.transform.position += transVec * Time.deltaTime; //causes collision bugs 
                rb.velocity = transVec; // does not create collision bugs

                GameObject camObj1 = camObj;
                //float camAngle = Quaternion.Angle(transform.rotation, camObj1.transform.rotation);
                camX = lookVec.x * sensitivity * Time.deltaTime;// left and right
                camY = lookVec.y * sensitivity * Time.deltaTime;// up and down 



                camAngleY += -camY;
                camAngleX += camX;
                //camObj1.transform.Rotate(camVec);

                camAngleY = Mathf.Clamp(camAngleY, -90f, 90f);// clamps the value of the y value so it can't go above the set amount
                //Debug.Log(camAngleY);
                camObj1.transform.localRotation =Quaternion.Euler(camAngleY, camAngleX,0f);// rotates the camera on the x and y axis
                // if(camAngle < 60)
                //camObj1.transform.localEulerAngles += new Vector3(0f, campY, 0);
                
                //camObj1.transform.localEulerAngles += new Vector3(-campY, lookVec.x * sensitivity, 0f);// rotates the camera up and down***

                //gameObject.transform.localEulerAngles += new Vector3(0, lookVec.x * sensitivity, 0);// rotates the player left and right**

                gameObject.transform.localRotation = Quaternion.Euler(0f,camAngleX, 0); // rotates the camera left and right inline with the camera
                
                //camObj1.transform.localRotation = Quaternion.Euler(0f, camAngleX, 0);

                target.transform.position = Vector3.SmoothDamp(target.transform.position, gameObject.transform.position, ref velocity, followDelay * Time.deltaTime);// creates a damped interpolrant between the actual player and what the camerea follows
                camObj.transform.position = Vector3.SmoothDamp(camObj.transform.position, gameObject.transform.position, ref velocity, camFollowDelay * Time.deltaTime); // adds a slight delay to the main camera making the game feel so much better 
                target.transform.rotation = camObj.transform.rotation;//chase objects rotation to the camera 

                //camObj.transform.position = target.transform.position;// camera follow chase object

                fixedGravity();
                break;
        }

    }


}
