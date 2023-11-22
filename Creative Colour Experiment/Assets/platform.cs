using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    [Header("drag the script from the button you wish the platform responds from")]
    public pressurePlate button;

    [Header("expand the array to two and drag point 1 and point 2 into the array")]
    public Transform[] points;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (button.buttonActive)
        {
            Debug.Log("platform active");
            Transform childTrn;

            childTrn = this.gameObject.transform.GetChild(0);

             childTrn.transform.position = Vector3.MoveTowards(transform.position, points[1].transform.position, 0.5f * Time.deltaTime);
        }
    }
}
