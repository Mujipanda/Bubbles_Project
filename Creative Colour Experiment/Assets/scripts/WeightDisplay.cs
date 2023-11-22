using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeightDisplay : MonoBehaviour
{
    private TMP_Text text;
    private Rigidbody rb;
    private float mass;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        mass = rb.mass;
        text = GetComponent<TMP_Text>();
        text.text = mass.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        mass = rb.mass;

       // text.transform.rotation = Quaternion.Euler(0f, transform.parent.rotation.y, transform.parent.rotation.z);
        //text.transform.position = gameObject.GetComponentInParent<Transform>().position;
        Mathf.RoundToInt(mass);
       // Mathf.FloorToInt(mass);
       int massInt = Mathf.CeilToInt(mass);
        text.text = "Mass " + massInt.ToString() ;
    }
}
