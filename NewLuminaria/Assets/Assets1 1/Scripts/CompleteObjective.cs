using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompleteObjective : MonoBehaviour
{
    public bool isCar;

    public int MaxParts;
    int currentParts = 0;

    bool isCompleted = false;

    public float timeToAddObj;

    float singleFrameTime = 0.06f;

    public bool isConsumedObj = false;

    public float currentTimeInstalling = 0;

    public bool canMoves1 = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MaxParts == currentParts && !isCompleted)
        {
            isCompleted = true;
            Debug.Log("Objective Complete");
            if(!isCar)
            {
                Destroy(gameObject);
            }
        }
    }
    public void PutPartIn()
    {
        currentParts++;
    }

    public IEnumerator TimeToInstallObj()
    {
        while(currentTimeInstalling < timeToAddObj)
        {
            canMoves1 = false;
            if (Input.GetKey(KeyCode.E))
            {
                yield return new WaitForSeconds(singleFrameTime);
            }
            else
            {
                canMoves1 = true;
                currentTimeInstalling = 0;
                yield break;
            }
            currentTimeInstalling += singleFrameTime;
        }
        canMoves1 = true;
        currentTimeInstalling = 0;
        PutPartIn();
        isConsumedObj = true;
    }
}
