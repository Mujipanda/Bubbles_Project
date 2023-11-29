using UnityEngine;

public class platform : MonoBehaviour
{
    [SerializeField]
    [Header("drag the script from the button you wish the platform responds from")]
    private pressurePlate button;

    [SerializeField]
    [Header("expand the array to two and drag point 1 and point 2 into the array")]
    private Transform[] points;

    [SerializeField]
    private GameObject platformCentre;

    [SerializeField]
    private GameObject triggerBox;
    private int switchInt;
   
    private bool freeToMove = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            freeToMove = false;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
            freeToMove = true;
    }
    private void FixedUpdate()
    {
        if (button.buttonActive && freeToMove)//button.buttonActive
        {
            //Debug.Log("platform active");
            //Transform childTrn;

            //childTrn = this.gameObject.transform.GetChild(0);
            switch (switchInt)
            {
                case 0:
                    platformCentre.transform.position = Vector3.MoveTowards(platformCentre.transform.position, points[0].transform.position, 0.5f * Time.deltaTime);
                    break;
                case 1:
                    platformCentre.transform.position = Vector3.MoveTowards(platformCentre.transform.position, points[1].transform.position, 0.5f * Time.deltaTime);
                    break;
            }

            if (Vector3.Distance(platformCentre.transform.position, points[1].transform.position) < 0.1f) 
            {
                
                switchInt = 0;  
            }
            else if (Vector3.Distance(platformCentre.transform.position, points[0].transform.position) < 0.1f)
            {
                
                switchInt = 1;
            }
           
            //childTrn.transform.position = Vector3.MoveTowards(transform.position, points[1].transform.position, 0.5f * Time.deltaTime);
        }
    }
}
