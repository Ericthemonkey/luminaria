using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseInteractions : MonoBehaviour
{
    public bool isContainingFuse;

    public PowerManager powerManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator increasePower()
    {
        isContainingFuse = true;
        powerManagerScript.power += powerManagerScript.initialPowerIncrease;
        for (int i = 0; i < powerManagerScript.timeToInstall; i++)
        {
            yield return new WaitForSeconds(1); 
        }
        if(isContainingFuse)
        {
            powerManagerScript.power += powerManagerScript.intalledPowerIncrease;
            isContainingFuse = false;
        }
    }

    public void PutInFuseI()
    {
        StartCoroutine(increasePower());
        powerManagerScript.ResetStageAndStageTime();
    }

    public void DestroyFuse()
    {
        if(HasFuse())
        {
            powerManagerScript.power -= powerManagerScript.destroyFusePowerDecrease;
            isContainingFuse = false;
        }
    }

    public bool HasFuse()
    {
        if(isContainingFuse)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
