using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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
   

    int[,] colourRBGValues = new int[5, 3]{ // [ number of rows, number colums]
        //R    G    B
        { 153, 255, 153 },// green
        { 255, 255, 153 },// yellow
        { 255, 153, 255 },// pink
        { 153, 255, 255 },// blue
        { 255, 204, 102 },// orange
    };


    private void Awake()
    {
        rend = GetComponent<Renderer>();
        colour = new Color(1.0f, 1.0f, 1.0f);
        colour32 = new Color32(255,255, 255, 255);

        controls = new ControlsInputs();
        controls.Enable();


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

        Debug.Log(selectedColour);
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

        Debug.Log(selectedColour);
        yield return new WaitForEndOfFrame();
    }
    void rayCastingGunHit()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(gunTransform.position, cameraTransform.forward, out hit, 20))
        {

            //Debug.Log("hitting Object");
            gizmoPoint = hit.point;
            currentGameObject = hit.transform.gameObject;
            colour32 = currentGameObject.GetComponent<Renderer>().material.color;

            ChangeColour(selectedColour, colour32);
           /* switch (selectedColour)
            {
                case 0:
                    Debug.Log("green");
                    
                    if ((colour.r * 255) < colourRBGValues[0, 0])
                    {
                        colour.r += 0.05f;
                    }
                    if ((colour.g * 255) < colourRBGValues[0,1])
                    {
                        colour.g += 0.05f;
                    }
                    if((colour.b * 255) <= colourRBGValues[0,2])
                    {
                        colour.b += 0.05f;
                    }
                    else if ((colour.r * 255) > colourRBGValues[0, 0])
                    {
                        colour.r -= 0.05f;
                    }
                    else if ((colour.g * 255) > colourRBGValues[0, 1])
                    {
                        colour.g -= 0.05f;
                    }
                    else if ((colour.b * 255) > colourRBGValues[0, 2])
                    {
                        colour.b -= 0.05f;
                    }
                    //Debug.Log((colour.r * 255) + colourRBGValues[0, 0]);
                    //Debug.Log(colour.r * 255);
                    //colour.r += 0.05f;
                    break;
                case 1:
                    Debug.Log("yellow");
                    if ((colour.r * 255) < colourRBGValues[1, 0])
                    {
                        colour.r += 0.05f;
                    }
                    if ((colour.g * 255) < colourRBGValues[1, 1])
                    {
                        colour.g += 0.05f;
                    }
                    if ((colour.b * 255) <= colourRBGValues[1, 2])
                    {
                        colour.b += 0.05f;
                    }
                    else if ((colour.r * 255) > colourRBGValues[1, 0])
                    {
                        colour.r -= 0.05f;
                    }
                    else if ((colour.g * 255) > colourRBGValues[1, 1])
                    {
                        colour.g -= 0.05f;
                    }
                    else if ((colour.b * 255) > colourRBGValues[1, 2])
                    {
                        colour.b -= 0.05f;
                    }

                    break;
                case 2:
                    //Debug.Log("pink");
                    Debug.Log(colour.g + " G");
                    Debug.Log(colour.r + " R");
                    Debug.Log(colour.b + " B");
                    if (colour.g > 0)
                    {
                        colour.g -= 0.1f;
                        colour.r += 0.1f;
                        colour.b += 0.1f;
                    }
                    break;
                case 3:
                    //.Log("blue");
                    if (colour.r > 0)
                    {
                        colour.g += 0.1f;
                        colour.r -= 0.1f;
                        colour.b += 0.1f;

                    }
                    break;
                case 4:
                    Debug.Log("orange");
                    break;

            }
            */
        }
        // Debug.DrawLine(gunTransform.position, transform.TransformDirection(Vector3.forward) * 100, Color.green);
    }

  void ChangeColour( int selectedColour,  Color colour)
    {
        Debug.Log(colour.r);
        if ((colour.r * 255) < colourRBGValues[selectedColour, 0])
        {
            colour.r += 0.05f;
        }
        if ((colour.g * 255) < colourRBGValues[selectedColour, 1])
        {
            colour.g += 0.05f;
        }
        if ((colour.b * 255) <= colourRBGValues[selectedColour, 2])
        {
            colour.b += 0.05f;
        }
        else if ((colour.r * 255) > colourRBGValues[selectedColour, 0])
        {
            colour.r -= 0.05f;
        }
        else if ((colour.g * 255) > colourRBGValues[selectedColour, 1])
        {
            colour.g -= 0.05f;
        }
        else if ((colour.b * 255) > colourRBGValues[selectedColour, 2])
        {
            colour.b -= 0.05f;
        }
        currentGameObject.GetComponent<Renderer>().material.color = colour;
    }


    void OnFire()
    {
        // StartCoroutine(sprayDelay());
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.black;
        // Gizmos.DrawWireSphere(gizmoPoint, radius: 0.2f);
        Gizmos.DrawSphere(gizmoPoint, radius: 0.1f);
    }
    // Update is called once per frame
    void Update()
    {
        if (isShooting)
            rayCastingGunHit();
        //rayCastingGunHit();

        if (controls.Player.Fire.IsPressed())
        {
            isShooting = true;
            Debug.Log("isPressed");
        }
        else
            isShooting = false;

    }

    private void FixedUpdate()
    {
        //gunTransform.localEulerAngles = new Vector3(0, cameraTransform.rotation.y, cameraTransform.rotation.y);
        //gunTransform.transform.position = new Vector3(chaseTransform.transform.position.x, chaseTransform.transform.position.y - 0.3f, chaseTransform.transform.position.z + 0.5f);
        //gunTransform.position = chaseTransform.position;
        //gunTransform.position = new Vector3(chaseTransform.position.x, chaseTransform.position.y, chaseTransform.position.z);
        //gunTransform.position = new Vector3(chaseTransform.position.x, chaseTransform.position.y, chaseTransform.forward.z + 0.5f);
        //gunTransform.rotation = chaseTransform.rotation;
        //gunTransform.position = chaseTransform.position;
        gunTransform.position = gunPos.position;
        gunTransform.rotation = gunPos.rotation;
        
    }
}
