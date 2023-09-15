using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SurvivorHealth : MonoBehaviour
{
    public float maxHealth;
    public float health;

    public Slider healthBar;
    
    void Start()
    {
        healthBar.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if(health < 0)
        {
            Destroy(gameObject);
        }

        healthBar.value = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
