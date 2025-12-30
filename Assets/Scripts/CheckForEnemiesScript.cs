using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForEnemiesScript : MonoBehaviour
{
    public PlayerScript PlayerScript;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerScript.canBePressed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            PlayerScript.canBePressed = true;
        }
    }
}
