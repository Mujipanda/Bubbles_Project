using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;
public class sprayGun : MonoBehaviour
{
    [SerializeField]
    private Transform gunTransform, chaseTransform, cameraTransform, gunPos;
    private GameObject currentGameObject;
    private Renderer rend;
    private Vector3 gizmoPoint = Vector3.zero;
    private Color colour;
    private Color32 colour32;
    ControlsInputs controls;
    private int selectedColour = 0;
    private bool isShooting = false;
    public PauseMenu pauseScript;
    private Rigidbody rb;

    int[,] colourRBGValues = new int[5, 3]{ // [ number of rows, number colums]
        //R    G    B
        { 153, 255, 153 },// green
        { 255, 255, 153 },// yellow
        { 255, 153, 230 },// pink
        { 153, 255, 255 },// blue
        { 255, 204, 102 },// orange
    };


    private void Awake()
    {
        rend = GetComponent<Renderer>();
        //colour = new Color(1.0f, 1.0f, 1.0f);
        colour32 = new Color32(255, 255, 255, 255);

        controls = new ControlsInputs();
        controls.Enable();

        pauseScript = GetComponent<PauseMenu>();
    }

    void OnColourUp()
    {
        StartCoroutine(switchUp());
    }
    IEnumerator switchUp()
    {
        if (selectedColour > 3)
            selectedColour = 0;
        else
            selectedColour += 1;

        // Debug.Log(selectedColour);
        yield return new WaitForEndOfFrame();
    }

    void OnColourDown()
    {

        StartCoroutine(switchDown());

    }

    IEnumerator switchDown()
    {
        if (selectedColour < 1)
            selectedColour = 4;

        else
            selectedColour -= 1;

        //Debug.Log(selectedColour);
        yield return new WaitForEndOfFrame();
    }
    IEnumerator shootingDelay()
    {
        Profiler.BeginSample("Shooting gun");
        rayCastingGunHit();
        Profiler.EndSample();
        yield return new WaitForSeconds(0.01f);
    }
    void rayCastingGunHit()
    {

        RaycastHit hit;
        int layerMask = 1 << 3;
        if (Physics.Raycast(gunTransform.position, cameraTransform.forward, out hit, 20, layerMask))
        {

            //Debug.Log("hitting Object");
            gizmoPoint = hit.point;
            currentGameObject = hit.transform.gameObject;
            colour32 = currentGameObject.GetComponent<Renderer>().material.color;
            if (currentGameObject.GetComponent<Rigidbody>())
            {
                rb = currentGameObject.GetComponent<Rigidbody>();
                increaseMass();
                increaseScale(currentGameObject);
                ChangeColour(selectedColour, colour32);
            }

            
            
            

        }
        // Debug.DrawLine(gunTransform.position, transform.TransformDirection(Vector3.forward) * 100, Color.green);
    }

    void ChangeColour(int selectedColour, Color32 colour)
    {
        //Debug.Log(colour.r);

        colour.r = red(selectedColour, colour).r;
        colour.g = green(selectedColour, colour).g;
        colour.b = blue(selectedColour, colour).b;
        /// green(selectedColour);

        currentGameObject.GetComponent<Renderer>().material.color = colour;
    }
    void red2(int selectedColour, Color32 colour)
    {
        if ((colour.r) < colourRBGValues[selectedColour, 0])
        {
            colour.r += 1;
        }
        else if ((colour.r) > colourRBGValues[selectedColour, 0])
        {
            colour.r -= 1;
        }
    }

    Color32 red(int selectedColour, Color32 colour)
    {
        if ((colour.r) < colourRBGValues[selectedColour, 0])
        {
            colour.r += 1;
        }
        else if ((colour.r) > colourRBGValues[selectedColour, 0])
        {
            colour.r -= 1;
        }
        return colour;
    }

    Color32 green(int selectedColour, Color32 colour)
    {
        if ((colour.g) < colourRBGValues[selectedColour, 1])
        {
            colour.g += 1;
        }
        else if ((colour.g) > colourRBGValues[selectedColour, 1])
        {
            colour.g -= 1;
        }
        return colour;
    }
    Color32 blue(int selectedColour, Color32 colour)
    {
        if ((colour.b) < colourRBGValues[selectedColour, 2])
        {
            colour.b += 1;
        }
        else if ((colour.g) > colourRBGValues[selectedColour, 2])
        {
            colour.b -= 1;
        }
        return colour;
    }

    void increaseMass()
    {
        switch (selectedColour)
        {
            case 0:
                StartCoroutine(MassUp());
                
                rb.useGravity = true;
                break;

            case 1:
                if (rb.mass > 1)
                {
                    StartCoroutine(MassDown());
                   
                }
                else if (rb.mass < 1)
                    rb.useGravity = false;
                break;
        }

    }

    IEnumerator MassUp()
    {
        rb.mass += 1;
        yield return new WaitForSeconds(0.01f);
    }
    IEnumerator MassDown()
    {
        rb.mass -= 1;
        yield return new WaitForSeconds(0.01f);
    }

    void increaseScale(GameObject currentGameObject)
    {
        switch (selectedColour)
        {
            case 2:
                currentGameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
                break;

            case 3:
                currentGameObject.transform.localScale += new Vector3(-0.01f, -0.01f, -0.01f);
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (pauseScript.gamePaused)
        {
            case false:
                if (isShooting)
                    StartCoroutine(shootingDelay());

                //rayCastingGunHit();

                if (controls.Player.Fire.IsPressed())
                {
                    isShooting = true;
                    //Debug.Log("isPressed");
                }
                else
                    isShooting = false;
                break;
        }


    }

    private void FixedUpdate()
    {
        gunTransform.position = gunPos.position;
        gunTransform.rotation = gunPos.rotation;

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.black;
        // Gizmos.DrawWireSphere(gizmoPoint, radius: 0.2f);
        Gizmos.DrawSphere(gizmoPoint, radius: 0.1f);
    }
}
