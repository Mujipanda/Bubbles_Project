using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderFill : MonoBehaviour
{
    public int SliderMax;
    public gameObject filled;

    private int CurrentFill;

    // Start is called before the first frame update
    void Start()
    {
        CurrentFill = SliderMax;
        filled.fillamount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            fill(10);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            reduce(10);
        }
    }

    public void fill(int i)
    {
        CurrentFill += i;
        if (currentfill > SliderMax)
        {
            CurrentFill = SliderMax;
        }

        filled.fillamount = (float)CurrentFill / SliderMax;
    }

    public void reduce(int i)
    {
        CurrentFill -= i;
        if (currentfill < 0)
        {
            CurrentFill = 0;
        }

        filled.fillamount = (float)CurrentFill / SliderMax;
    }
}
