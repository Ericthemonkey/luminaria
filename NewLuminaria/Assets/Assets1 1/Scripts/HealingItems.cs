using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealingItems : MonoBehaviour
{
    public SurvivorInteractions inventoryScript;
    public SurvivorHealth playerHealthScript;
    public InteractionBar InteractionBarScript;

    public float timeToHeal;
    float singleFrameTime = 0.06f;

    public float healthIncreases;

    // Start is called before the first frame update
    void Start()
    {
        if (playerHealthScript.maxHealth < healthIncreases)
        {
            healthIncreases = playerHealthScript.maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2") && !InteractionBarScript.isInteracting && playerHealthScript.health < playerHealthScript.maxHealth) 
        {
            StartCoroutine(TimedHealAction());
        }
    }
    IEnumerator TimedHealAction()
    {
        InteractionBarScript.isInteracting = true;
        for(float i = 0; i < timeToHeal; i += singleFrameTime)
        {
            if (Input.GetButton("Fire2"))
            {
                yield return new WaitForSeconds(singleFrameTime);
                InteractionBarScript.SetMax(timeToHeal);
                InteractionBarScript.SetValue(i);
            }
            else
            {
                InteractionBarScript.isInteracting = false;
                InteractionBarScript.DisableBar();
                yield break;
            }
        }
        InteractionBarScript.isInteracting = false;
        InteractionBarScript.DisableBar();
        playerHealthScript.health += healthIncreases;
        inventoryScript.ConsumeItem();
    }
}
