using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionBar : MonoBehaviour
{
    public Slider InteractionSlide;

    public bool isInteracting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMax(float maxValue)
    {
        InteractionSlide.maxValue = maxValue;
    }
    public void SetValue(float value)
    {
        InteractionSlide.value = value;
    }
    public void DisableBar()
    {
        InteractionSlide.maxValue = 0;
    }
}
