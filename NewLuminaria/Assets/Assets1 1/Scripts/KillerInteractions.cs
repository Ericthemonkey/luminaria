using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerInteractions : MonoBehaviour
{
    public float interactionRange;

    public GameObject InteractionUI;

    public AttackControls bloodBankScript;

    public float BloodOnDestroyFuse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InteractionUI.SetActive(false);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
        {
            FuseInteractions DestroyFuses = hit.transform.GetComponent<FuseInteractions>();

            if(DestroyFuses != null)
            {
                InteractionUI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E) && DestroyFuses.HasFuse())
                {
                    DestroyFuses.DestroyFuse();
                    bloodBankScript.bloodBank += BloodOnDestroyFuse;
                }
            }           
        }
    }
}
