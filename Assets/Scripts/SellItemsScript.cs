using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SellItemsScript : MonoBehaviour
{
    public PlayerScript PlayerScript;
    public GameObject Inventory;

    public bool isTriggered = false;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            foreach (Transform item in Inventory.transform)
            {
                if (item.GetComponentInChildren<MeshRenderer>().enabled == true && isTriggered)
                {
                    Destroy(item.GetChild(0).gameObject);
                    PlayerScript.playerMoney += 50;
                }
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = false;
        }
    }
}
