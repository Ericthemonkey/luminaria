using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampItem : MonoBehaviour
{
    public Light lightComp;

    public float lightOnIntensity;
    public float lightOnRange;

    bool isLightOn = false;

    // Start is called before the first frame update
    void Start()
    {
        lightComp.enabled = false;
        lightComp.intensity = lightOnIntensity;
        lightComp.range = lightOnRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            SwapLampOnOff();
        }

        if(isLightOn)
        {
            lightComp.enabled = true;
        }
        else
        {
            lightComp.enabled = false;
        }
    }
    void SwapLampOnOff()
    {
        if(isLightOn)
        {
            isLightOn = false;
        }
        else
        {
            isLightOn = true;
        }
    }
}
