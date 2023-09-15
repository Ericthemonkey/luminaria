using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampScript : MonoBehaviour
{
    public Light lightComp;

    public float lightOnIntensity;
    public float lightOnRange;

    PowerManager powerManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        powerManagerScript = GameObject.FindObjectOfType<PowerManager>();
        lightComp.intensity = lightOnIntensity;
        lightComp.range = lightOnRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (powerManagerScript.areLightsOn)
        {
            lightComp.enabled = true;
        }
        else
        {
            lightComp.enabled = false;
        }
    }
}
