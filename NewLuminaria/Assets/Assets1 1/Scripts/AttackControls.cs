using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AttackControls : MonoBehaviour
{
    public bool doingAction = false;

    public float bloodBank;

    public TextMeshProUGUI bloodAmountUI;

    float nextAction;
    
    public float M1Delay;
    public float M1Duration;
    public float M1Cooldown;
    
    public GameObject M1HitBoxObj;

    public float NormalBloodIncrease;
    public float BloodTickTime;
    float nextBloodTick;

    bool isM1 = false;
    // Start is called before the first frame update
    void Start()
    {
        nextBloodTick = Time.time + BloodTickTime;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBloodTicks();

        if (Input.GetButtonDown("Fire1") && Time.time > nextAction)
        {
            StartCoroutine(M1Swipe());
            nextAction = Time.time + M1Cooldown;
        }

        M1HitBoxObj.SetActive(isM1);
        bloodAmountUI.text = bloodBank.ToString();
    }
    IEnumerator M1Swipe()
    {
        yield return new WaitForSeconds(M1Delay);
        isM1 = true;
        doingAction = true;

        yield return new WaitForSeconds(M1Duration);
        isM1 = false;
        doingAction = false;
    }
    void CheckBloodTicks()
    {
        if (Time.time >= nextBloodTick)
        {
            nextBloodTick = Time.time + BloodTickTime;
            bloodBank += NormalBloodIncrease;
        }
    }
}
