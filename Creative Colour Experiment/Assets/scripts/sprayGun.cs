using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sprayGun : MonoBehaviour
{
    [Header(" input the model of the sprayGun")]
    [SerializeField]
    private Transform SprayGunModel;

    [Header(" input the Main Camera")]
    [SerializeField]
    private Transform cameraObject;

    [Header("Input Gun Pos from scene")]
    [SerializeField]
    private Transform gunPos;

    [Header("pause script from player")]
    public PauseMenu pauseScript;
    [Header("button Script (not used)")]
    public buttonScript buttonScript;
    [Header("The Colour Image within the canvas")]
    public GameObject colourImage;
    [Header("Colour test from canvas")]
    public TMP_Text colourText;
    [Header("The Colour particles effects ,order:\nG, Y, P, B")]
    public GameObject[] psEffects;
    [Header("The Colour Wheel Sprites ,order:\nG, Y, P, B")]
    public GameObject[] colourWheelSprites;

    private GameObject currentGameObject;

    private Vector3 gizmoPoint = Vector3.zero;

    private Color32 colour32;

    ControlsInputs controls;

    private int selectedColour = 0;

    private bool isShooting = false, PsEffectOn = false, psEffectOff = true;
    private Rigidbody rb;

    private int storePrevColour;

    private bool canStoreColour = true;

    int[,] colourRBGValues = new int[5, 3]{ // [ number of rows, number colums]
        //R    G    B
        { 153, 255, 153 },// green
        { 255, 255, 153 },// yellow
        { 255, 153, 230 },// pink
        { 153, 255, 255 },// blue
        { 255, 165, 50 },// orange
        // no value shall be 0 or colours will not work
    };


    private void Awake()
    {

        //colour = new Color(1.0f, 1.0f, 1.0f);
        colour32 = new Color32(255, 255, 255, 255);

        controls = new ControlsInputs();
        controls.Enable();

        pauseScript = GetComponent<PauseMenu>();

        for (int i = 0; i < psEffects.Length; i++)
        {
            psEffects[i].GetComponent<ParticleSystem>().Stop();
        }
        buttonScript = GetComponent<buttonScript>();
        for (int i = 0; i < colourWheelSprites.Length; i++)
        {
            colourWheelSprites[i].GetComponent<Image>().enabled = false;
        }
    }
    private void Start()
    {
        colourImage.GetComponent<Image>().color = new Color32(153, 255, 153, 255);
        colourText.text = "Green";
        colourWheelSprites[selectedColour].GetComponent<Image>().enabled = true;
    }
   
    void OnGreen()
    {
        selectedColour = 0;
        switchColourIcon();
    }
    void OnYellow()
    {
        selectedColour = 1;
        switchColourIcon();
    }
    void OnBlue()
    {
        selectedColour = 3;
        switchColourIcon();
    }
    void OnPink()
    {
        selectedColour = 2;
        switchColourIcon();
    }
    


    private Ray RayPoint()
    {
        return Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
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

        //Debug.Log(selectedColour);
        switchColourIcon();
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
        switchColourIcon();
        yield return new WaitForEndOfFrame();
    }

    void switchColourIcon()
    {
        for (int i = 0; i < colourWheelSprites.Length; i++)
        {
            colourWheelSprites[i].GetComponent<Image>().enabled = false;
            if (i == selectedColour)
            {
                colourWheelSprites[i].GetComponent<Image>().enabled = true;
            }
        }
      /*  switch (selectedColour)
        {
            case 0:
                
                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;

        }
        switch (selectedColour)
        {
            case 0:
                //colourImage.color = new Color32(153, 255, 153, 255); // green
                colourImage.GetComponent<Image>().color = new Color32(153, 255, 153, 255);
                colourText.text = "Green \n Mass+";
                break;
            case 1:
                //colourImage.color = new Color32(255, 255, 153, 255);// yellow
                colourImage.GetComponent<Image>().color = new Color32(255, 255, 153, 255);
                colourText.text = "Yellow \n Mass-";
                break;
            case 2:
                //colourImage.color = new Color32(255, 153, 230, 255);// pink
                colourImage.GetComponent<Image>().color = new Color32(255, 153, 230, 255);
                colourText.text = "Pink \n Scale+";
                break;
            case 3:
                // colourImage.color = new Color32(255, 165, 50, 255);// blue
                colourImage.GetComponent<Image>().color = new Color32(153, 255, 255, 255);
                colourText.text = "Blue \n Scale-";
                break;
            case 4:
                //colourImage.color = new Color32(255, 165, 50, 255);// orange
                colourImage.GetComponent<Image>().color = new Color32(255, 165, 50, 255);
                colourText.text = "Orange";
                break;
        }*/
    }
    IEnumerator shootingDelay()
    {
        // Profiler.BeginSample("Shooting gun");
        rayCastingGunHit();
        //Profiler.EndSample();
        // yield return new WaitForSeconds(0.01f);
        yield return new WaitForEndOfFrame();
    }

    void rayCastingGunHit()
    {

        RaycastHit hit;
        int layerMask = 1 << 3;
        if (Physics.Raycast(SprayGunModel.position, cameraObject.forward, out hit, 20, layerMask))
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
        // Debug.DrawLine(SprayGunModel.position, transform.TransformDirection(Vector3.forward) * 100, Color.green);
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
        rb.mass += 0.5f;
        yield return new WaitForSeconds(0.01f);
    }
    IEnumerator MassDown()
    {
        rb.mass -= 0.5f;
        yield return new WaitForSeconds(0.01f);
    }

    void increaseScale(GameObject currentGameObject)
    {
        switch (selectedColour)
        {
            case 2:
                currentGameObject.transform.localScale += new Vector3(0.005f, 0.005f, 0.005f);
                break;

            case 3:
                currentGameObject.transform.localScale += new Vector3(-0.005f, -0.005f, -0.005f);
                break;

        }
    }

    void OnActivate() // pressing button 
    {
        RaycastHit hit;
        Ray ray = RayPoint();
        int layerMask = 1 << 6;
        if (Physics.Raycast(ray, out hit, 3, layerMask))
        {
            Debug.Log("Button Pressed");
            buttonScript.buttonIsPressed = true;
        }
    }

    IEnumerator enablePS()
    {
        PsEffectOn = true;
        psEffectOff = false;
        psEffects[selectedColour].GetComponent<ParticleSystem>().Play();
        yield return null;

    }
    IEnumerator disablePS()
    {

        psEffectOff = true;
        PsEffectOn = false;
        psEffects[selectedColour].GetComponent<ParticleSystem>().Stop();
        psEffects[storePrevColour].GetComponent<ParticleSystem>().Stop();
        canStoreColour = true;
        yield return null;
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
                    if (canStoreColour)
                    {
                        canStoreColour = false;
                        storePrevColour = selectedColour;
                    }

                    isShooting = true;
                    if (!PsEffectOn)
                        StartCoroutine(enablePS());

                    //Debug.Log("isPressed");
                }
                else
                {
                    if (!psEffectOff)
                        StartCoroutine(disablePS());

                    isShooting = false;

                }

                break;
        }

    }

    private void FixedUpdate()
    {
        SprayGunModel.position = gunPos.position;
        SprayGunModel.rotation = gunPos.rotation;

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.black;
        // Gizmos.DrawWireSphere(gizmoPoint, radius: 0.2f);
        Gizmos.DrawSphere(gizmoPoint, radius: 0.1f);
    }
}
