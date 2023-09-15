using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveItemPickUp : MonoBehaviour
{
    public int ItemIndex;

    Vector3 currentLocation;

    public GameObject CarPartPrefab;
    public GameObject GateKeyPrefab;
    public GameObject FusePrefab;
    public GameObject BandagePrefab;
    public GameObject ChocPrefab;
    public GameObject LampPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        currentLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwapedItem()
    {
        if (ItemIndex == 0)
        {
            Instantiate(FusePrefab, currentLocation, transform.rotation);
        }
        else if (ItemIndex == 1)
        {
            Instantiate(GateKeyPrefab, currentLocation, transform.rotation);
        }
        else if (ItemIndex == 2)
        {
            Instantiate(CarPartPrefab, currentLocation, transform.rotation);
        }
        else if(ItemIndex == 3)
        {
            Instantiate(BandagePrefab, currentLocation, transform.rotation);
        }
        else if(ItemIndex == 4)
        {
            Instantiate(ChocPrefab, currentLocation, transform.rotation);
        }
        else if(ItemIndex == 5)
        {
            Instantiate(LampPrefab, currentLocation, transform.rotation);
        }
        Destroy(gameObject);
    }
    public void PickedUp()
    {
        Destroy(gameObject);
    }
}
