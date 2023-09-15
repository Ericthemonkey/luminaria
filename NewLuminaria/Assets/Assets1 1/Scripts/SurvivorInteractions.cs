using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SurvivorInteractions : MonoBehaviour
{
    public float interactionRange;

    public TextMeshProUGUI ItemSlot1UI;
    public TextMeshProUGUI ItemSlot2UI;
    public TextMeshProUGUI ItemSlot3UI;
    public TextMeshProUGUI SelectedItemUI;

    public GameObject CarPartPrefab;
    public GameObject GateKeyPrefab;
    public GameObject FusePrefab;
    public GameObject BandagePrefab;
    public GameObject ChocPrefab;
    public GameObject LampPrefab;

    public GameObject InteractUI;

    public Transform DropLocationT;
    Vector3 DropLocationV;

    public LayerMask InteractableLayerMask;

    int swapedItemIndex;

    //Index of all Items
    int NoneItem = 100;
    int FuseItem = 0;
    int GateKeyItem = 1;
    int CarPartItem = 2;

    static public int currentSlot = 0;

    static public int[] ItemSlots = {100, 100, 100};

    float singleFrameTime = 0.06f;

    bool SurvivorLookingAtObject;

    bool LookedAtInteractable = false;

    bool canDrop = false;
    public float dropRadius;

    public InteractionBar interactionBarScript;

    public SurvivorMovement SurvivorMoveScript;

    bool resetBar = false;

    // Start is called before the first frame update
    void Start()
    {
        ItemSlots[0] = NoneItem;
        ItemSlots[1] = NoneItem;
        ItemSlots[2] = NoneItem;

        
    }

    // Update is called once per frame
    void Update()
    {
        SelectItem();

        InteractUI.SetActive(false);

        SwapSlots();
        ItemSlot1UI.text = ItemSlots[0].ToString();
        ItemSlot2UI.text = ItemSlots[1].ToString();
        ItemSlot3UI.text = ItemSlots[2].ToString();
        SelectedItemUI.text = currentSlot.ToString();

        RaycastHit hit;

        DropLocationV = new Vector3(DropLocationT.position.x, DropLocationT.position.y, DropLocationT.position.z);
        canDrop = Physics.CheckSphere(DropLocationT.position, dropRadius);

        SurvivorLookingAtObject = Physics.Raycast(transform.position, transform.forward, out hit, interactionRange, InteractableLayerMask);

        if(!interactionBarScript.isInteracting && SurvivorMoveScript.isGrounded && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R");
            DropItem();
        }

        if (SurvivorLookingAtObject && !interactionBarScript.isInteracting && SurvivorMoveScript.isGrounded)
        {
            resetBar = false;
            ObjectiveItemPickUp pickUpObjective = hit.transform.GetComponent<ObjectiveItemPickUp>();
            CompleteObjective putInObjective = hit.transform.GetComponent<CompleteObjective>();
            FuseInteractions placeFuse = hit.transform.GetComponent<FuseInteractions>();
            TrashBag trashBagInteractions = hit.transform.GetComponent<TrashBag>();

            if (!LookedAtInteractable)
            {
                if(trashBagInteractions != null)
                {
                    trashBagInteractions.currentOpeningTime = 0;
                }
                else if(putInObjective != null)
                {
                    putInObjective.currentTimeInstalling = 0;
                }
                LookedAtInteractable = true;
            }

            if (pickUpObjective != null)
            {
                InteractUI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!hasObjectiveItem())
                    {
                        swapedItemIndex = ItemSlots[currentSlot];
                        if (isNoneSlot())
                        {
                            ReplaceWithNoneSlot(pickUpObjective.ItemIndex);
                            pickUpObjective.PickedUp();
                        }
                        else
                        {
                            ItemSlots[currentSlot] = pickUpObjective.ItemIndex;
                            pickUpObjective.ItemIndex = swapedItemIndex;
                            pickUpObjective.SwapedItem();
                        }
                    }
                    else
                    {
                        Debug.Log("Has Obj");
                        swapedItemIndex = ItemSlots[currentSlot];
                        if (isNoneSlot() && pickUpObjective.ItemIndex != CarPartItem && pickUpObjective.ItemIndex != GateKeyItem)
                        {
                            ReplaceWithNoneSlot(pickUpObjective.ItemIndex);
                            pickUpObjective.PickedUp();
                        }
                        else if (CurrentSlotIsObj())
                        {
                            ItemSlots[currentSlot] = pickUpObjective.ItemIndex;
                            pickUpObjective.ItemIndex = swapedItemIndex;
                            pickUpObjective.SwapedItem();
                        }
                    }
                }
            }
            else if (putInObjective != null)
            {
                InteractUI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && putInObjective.isCar == false && ItemSlots[currentSlot] == GateKeyItem)
                {
                    interactionBarScript.SetMax(putInObjective.timeToAddObj);
                    StartCoroutine(putInObjective.TimeToInstallObj());
                }
                if (Input.GetKeyDown(KeyCode.E) && putInObjective.isCar == true && ItemSlots[currentSlot] == CarPartItem)
                {
                    interactionBarScript.SetMax(putInObjective.timeToAddObj);
                    StartCoroutine(putInObjective.TimeToInstallObj());
                }
                if (putInObjective.isConsumedObj)
                {
                    ConsumeItem();
                    putInObjective.isConsumedObj = false;
                }
                interactionBarScript.SetValue(putInObjective.currentTimeInstalling);
                SurvivorMoveScript.canMove = putInObjective.canMoves1;
            }
            else if (placeFuse != null)
            {
                InteractUI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) && ItemSlots[currentSlot] == FuseItem && !placeFuse.isContainingFuse)
                {
                    ItemSlots[currentSlot] = NoneItem;
                    placeFuse.PutInFuseI();
                }
            }
            else if (trashBagInteractions != null)
            {
                if (!trashBagInteractions.hasBeenopened)
                {
                    InteractUI.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        interactionBarScript.SetMax(trashBagInteractions.timeToOpen);
                        StartCoroutine(trashBagInteractions.TimedOpenTrash());                        
                    }
                }
                interactionBarScript.SetValue(trashBagInteractions.currentOpeningTime);
                SurvivorMoveScript.canMove = trashBagInteractions.canMoves;
            } 
        }
        else
        {
            StopAllCoroutines();
            SurvivorMoveScript.canMove = true;
            if (!resetBar)
            {
                interactionBarScript.DisableBar();
                resetBar = true;
            }
            LookedAtInteractable = false;
        }
    }
    bool isNoneSlot()
    {
        for (int i = 0; i <= ItemSlots.Length - 1; i++)
        {
            if (ItemSlots[i] == NoneItem)
            {
                return true;
            }
        }
        return false;
    }

    bool CurrentSlotIsObj()
    {
        if (ItemSlots[currentSlot] == CarPartItem || ItemSlots[currentSlot] == GateKeyItem)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }

    void ReplaceWithNoneSlot(int ItemReplacing)
    {
        for (int i = 0; i <= ItemSlots.Length - 1; i++)
        {
            if (ItemSlots[i] == NoneItem)
            {
                ItemSlots[i] = ItemReplacing;
                return;
            }
        }
    }
    void SwapSlots()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentSlot = 0; StopAllCoroutines();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentSlot = 1; StopAllCoroutines();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentSlot = 2; StopAllCoroutines();
        }
    }
    bool hasObjectiveItem()
    {
        for (int i = 0; i <= ItemSlots.Length - 1; i++)
        {
            if (ItemSlots[i] == CarPartItem || ItemSlots[i] == GateKeyItem)
            {
                return true;
            }
        }
        return false;
    }

    void SelectItem()
    {
        int i = 0;
        foreach (Transform item in transform)
        {
            if (i == ItemSlots[currentSlot])
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(false);
            }
            i++;
        }
    }
    public void ConsumeItem()
    {
        ItemSlots[currentSlot] = NoneItem;
    }
    void DropItem()
    {
        if (ItemSlots[currentSlot] != NoneItem && !canDrop)
        {
            Debug.Log("Yes");
            if (ItemSlots[currentSlot] == 0)
            {
                Instantiate(FusePrefab, DropLocationV, Quaternion.Euler(0, 0, 0));
            }
            else if (ItemSlots[currentSlot] == 1)
            {
                Instantiate(GateKeyPrefab, DropLocationV, Quaternion.Euler(0, 0, 0));
            }
            else if (ItemSlots[currentSlot] == 2)
            {
                Instantiate(CarPartPrefab, DropLocationV, Quaternion.Euler(0, 0, 0));
            }
            else if (ItemSlots[currentSlot] == 3)
            {
                Debug.Log("Band");
                Instantiate(BandagePrefab, DropLocationV, Quaternion.Euler(0, 0, 0));
            }
            else if (ItemSlots[currentSlot] == 4)
            {
                Instantiate(ChocPrefab, DropLocationV, Quaternion.Euler(0, 0, 0));
            }
            else if (ItemSlots[currentSlot] == 5)
            {
                Instantiate(LampPrefab, DropLocationV, Quaternion.Euler(0,0,0));
            }
            ConsumeItem();
        }
    }
}
