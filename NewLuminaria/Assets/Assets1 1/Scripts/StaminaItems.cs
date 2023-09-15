using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaItems : MonoBehaviour
{
    public SurvivorInteractions inventoryScript;
    public SurvivorMovement staminaScript;

    public float staminaIncrease;

    public float timeToUse;

    public float singleFrameTime = 0.06f;

    public Slider InteractionSlider;

    // Start is called before the first frame update
    void Start()
    {
        if(staminaScript.Maxstamina < staminaIncrease)
        {
            staminaIncrease = staminaScript.Maxstamina;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(TimedEatAction());
        }
    }
    IEnumerator TimedEatAction()
    {
        InteractionSlider.maxValue = timeToUse;
        for (float i = 0; i < timeToUse; i += timeToUse)
        {
            if (Input.GetButton("Fire2"))
            {
                yield return new WaitForSeconds(singleFrameTime);
                InteractionSlider.value = i;
            }
            else
            {
                InteractionSlider.value = 0;
                yield break;
            }
        }
        InteractionSlider.value = 0;
        staminaScript.stamina += staminaIncrease;
        inventoryScript.ConsumeItem();
    }
}
