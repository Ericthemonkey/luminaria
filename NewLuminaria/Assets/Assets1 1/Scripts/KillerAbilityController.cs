using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerAbilityController : MonoBehaviour
{
    public AttackControls attackControllsScript;

    public PowerManager PowerManagerScript;

    public float blackOutCost;
    public float bloodTrapCost;
    public float revealScreechCost;

    public float blackoutCD;
    public float blackOutDuration;
    float endOfBlackout;
    float nextBlackOut;

    // Start is called before the first frame update
    void Start()
    {
        PowerManagerScript = FindObjectOfType<PowerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attackControllsScript.doingAction)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {

            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && !PowerManagerScript.isBlackedOut && Time.time > nextBlackOut)
            {
                nextBlackOut = Time.time + blackoutCD;
                BlackoutAbility();
            }
        }

        if(Time.time > endOfBlackout)
        {
            PowerManagerScript.isBlackedOut = false;
        }
    }
    void BlackoutAbility()
    {
        PowerManagerScript.isBlackedOut = true;
        endOfBlackout = Time.time + blackOutDuration;
        attackControllsScript.bloodBank -= blackOutCost;
    }
}
