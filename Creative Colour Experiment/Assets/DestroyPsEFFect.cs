using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPsEFFect : MonoBehaviour
{
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        Debug.Log(ps.main.duration);
        float duration = ps.main.duration;
        StartCoroutine(Destroy(duration));
       
    }

    IEnumerator Destroy(float duration)
    {
        
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
