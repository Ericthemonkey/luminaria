using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DummyHealth : MonoBehaviour
{
    public float maxHealth;
    public float health;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (IsDead())
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    public bool IsDead()
    {
        if(health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
