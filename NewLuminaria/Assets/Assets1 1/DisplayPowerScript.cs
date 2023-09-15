using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;

public class DisplayPowerScript : MonoBehaviour
{
    public TextMeshPro powerDisplay;
    public PowerManager PowerVarible;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        powerDisplay.text = PowerVarible.power.ToString();
    }
}
