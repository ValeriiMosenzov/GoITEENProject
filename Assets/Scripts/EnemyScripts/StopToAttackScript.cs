using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StopToAttackScript : MonoBehaviour
{
    //Transform
    public Transform Enemy;


    //Animatior
    public Animator root;


    //Scripts
    public SpotPlayerScript SpotPlayerScript;
    public StopHuntingScript StopHuntingScript;


    //Data
    public bool isTriggered = false;


    private void Start()
    {
        Enemy = transform.parent;
        StopHuntingScript = Enemy.GetComponentInChildren<StopHuntingScript>();
        SpotPlayerScript = Enemy.GetComponentInChildren<SpotPlayerScript>();
        root = transform.parent.GetChild(4).GetChild(0).GetComponent<Animator>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true;
            SpotPlayerScript.spotted = false;
            root.SetBool("IsRunning", false);
            StartCoroutine(WaitAndMoveNext());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = false;
        }
    }

    IEnumerator WaitAndMoveNext()
    {
        yield return new WaitForSeconds(1f);
        if (StopHuntingScript.isTriggered && isTriggered == false)
        {
            SpotPlayerScript.spotted = true;
        }
    }
}
