using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public float power;
    public bool areLightsOn;

    float DecreaseAmount;
    public float DecreasePowerTickTime;

    public Light[] LampsList;

    public bool isBlackedOut;

    int currentStage = 1;

    public float decreaseStage1;
    public float decreaseStage2;
    public float decreaseStage3;

    public float decreaseStage1Time;
    public float decreaseStage2Time;

    float nextStageChangeTime = 0;

    float nextDecreasePower;

    public float maxPower;
    public float minPower;

    public float initialPowerIncrease;
    public float intalledPowerIncrease;
    public float timeToInstall;

    public float destroyFusePowerDecrease;

    // Start is called before the first frame update
    void Start()
    {
        nextStageChangeTime = Time.time + decreaseStage1Time;
        nextDecreasePower += DecreasePowerTickTime;
        DecreaseAmount = decreaseStage1;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeStageAndAmount();
        TickDescreasePower();

        power = Mathf.Clamp(power, minPower, maxPower);

        if(power >= maxPower / 2 & !isBlackedOut)
        {
            areLightsOn = true;
        }
        else
        {
            areLightsOn = false;
        }
    }
    void TickDescreasePower()
    {
        if(Time.time >= nextDecreasePower)
        {
            nextDecreasePower = Time.time + DecreasePowerTickTime;
            power -= DecreaseAmount;
        }
    }
    void ChangeStageAndAmount()
    {
        if (currentStage == 1)
        {
            DecreaseAmount = decreaseStage1;
            if(Time.time >= nextStageChangeTime)
            {
                nextStageChangeTime = Time.time + decreaseStage2Time;
                currentStage++;
            }
        }
        else if(currentStage == 2)
        {
            DecreaseAmount = decreaseStage2;
            if (Time.time >= nextStageChangeTime)
            {
                currentStage++;
            }
        }
        else if(currentStage == 3)
        {
            DecreaseAmount = decreaseStage3;
        }
    }
    public void ResetStageAndStageTime()
    {
        currentStage = 1;
        nextStageChangeTime = Time.time + decreaseStage1Time;
    }
}
