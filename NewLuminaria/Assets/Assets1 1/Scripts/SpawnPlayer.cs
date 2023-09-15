using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject SurvivorPrefab;
    public GameObject KillerPrefab;

    public GameObject SurvivorSpawnLoc;
    public GameObject KillerSpawnLoc;

    public GameObject StartScreen; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSurvivor()
    {
        Instantiate(SurvivorPrefab, SurvivorSpawnLoc.transform.position, Quaternion.identity);
        StartScreen.SetActive(false);
        Destroy(gameObject);
    }
    public void SpawnKiller()
    {
        Instantiate(KillerPrefab, KillerSpawnLoc.transform.position, Quaternion.identity);
        StartScreen.SetActive(false);
        Destroy(gameObject);
    }
}
