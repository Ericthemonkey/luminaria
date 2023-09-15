using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageTrigger : MonoBehaviour
{
    public float M1Damage;

    public float BloodIncreaseM1;
    public float BloodIncreaseOnDeath;

    public AttackControls bloodBankScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Survivor")
        {
            bloodBankScript.bloodBank += BloodIncreaseM1;
            other.GetComponent<SurvivorHealth>().TakeDamage(M1Damage);
        }
        else if (other.gameObject.tag == "Dummy")
        {
            bloodBankScript.bloodBank += BloodIncreaseM1;
            DummyHealth dummyHealth = other.GetComponent<DummyHealth>();
            dummyHealth.TakeDamage(M1Damage);
            if (dummyHealth.IsDead())
            {
                bloodBankScript.bloodBank += BloodIncreaseOnDeath;
            }
        }
    }
}
