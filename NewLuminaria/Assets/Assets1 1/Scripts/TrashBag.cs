using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBag : MonoBehaviour
{
    public GameObject itemSpawn;

    public bool hasBeenopened = false;

    //Item Prefabs
    public GameObject FusePrefab;
    public GameObject BandagePrefab;
    public GameObject ChocPrefab;

    float singleFrameTime = 0.06f;

    public float timeToOpen;
    public float currentOpeningTime;

    public bool canMoves;

    int randomItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator TimedOpenTrash()
    {
        while(currentOpeningTime < timeToOpen)
        {
            canMoves = false;
            if (Input.GetKey(KeyCode.E))
            {
                yield return new WaitForSeconds(singleFrameTime);
            }
            else
            {
                canMoves = true;
                currentOpeningTime = 0;
                yield break;
            }
            currentOpeningTime += singleFrameTime;
        }
        canMoves = true;
        currentOpeningTime = 0;
        if (!hasBeenopened)
        {
            randomItem = Random.Range(1, 3);
            if(randomItem == 1)
            {
                Instantiate(FusePrefab, itemSpawn.transform.position, Quaternion.identity);
            }
            else if(randomItem == 2)
            {
                Instantiate(ChocPrefab, itemSpawn.transform.position, Quaternion.identity);
            }
            else if(randomItem == 3)
            {
                Instantiate(BandagePrefab, itemSpawn.transform.position, Quaternion.identity);
            }
            hasBeenopened = true;
        }
    }
}
