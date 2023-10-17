using System.Collections;
using UnityEngine;

public class sprayGun : MonoBehaviour
{
    [SerializeField]
    private Transform gunTransform, chaseTransform, cameraTransform, gunPos;
    private GameObject currentGameObject;
    private Renderer rend;
    private Vector3 gizmoPoint = Vector3.zero;

    private int selectedColour = 0;
    Color colour;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        colour = new Color(1.0f, 1.0f, 1.0f);

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
            colour = currentGameObject.GetComponent<Renderer>().material.color;
            switch (selectedColour)
            {
                case 0:
                    Debug.Log("green");
                    break;
                case 1:
                    Debug.Log("yellow");

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

            currentGameObject.GetComponent<Renderer>().material.color = colour;
        }
        // Debug.DrawLine(gunTransform.position, transform.TransformDirection(Vector3.forward) * 100, Color.green);
    }

    void OnFire()
    {
        rayCastingGunHit();
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

        //rayCastingGunHit();
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
