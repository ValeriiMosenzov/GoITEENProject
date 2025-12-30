using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript : MonoBehaviour
{
    //Scripts
    public SpawnEnemyScript SpawnEnemyScript;
    public PlayerScript PlayerScript;


    //Data
    public int currentHP;
    public int maxHP;
    private GameObject hitEffect;
    private BloodPool bloodPool;


    //GameObjects
    public GameObject SwordToCopy;
    public GameObject ModelsToPickUp;


    private void Start()
    {
        currentHP = maxHP;
        SpawnEnemyScript = transform.GetComponent<SpawnEnemyScript>();
        ModelsToPickUp = SwordToCopy.transform.parent.gameObject;
    }

    private void Awake()
    {
        //bloodPoolAI = FindObjectOfType<BloodPoolAI>();
        if (!bloodPool) bloodPool = FindObjectOfType<BloodPool>();
    }

    public void SetHitEffect(GameObject effect)
    {
        hitEffect = effect;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"{name} получил {damage} урона. HP: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
            currentHP = maxHP;
        }
    }

    private void Die()
    {
        var toDespawn = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.name == "BloodSprayFX(Clone)")
            {
                toDespawn.Add(child.gameObject);
            }
        }
        foreach (var go in toDespawn)
        {
            //bloodPoolAI.Despawn(go);
            if (bloodPool != null)
            {
                bloodPool.returnBlood(go);
            }
        }

        int rnd = Random.Range(0, 5);
        if (rnd == 3)
        {
            GameObject droppedSword = Instantiate(SwordToCopy, ModelsToPickUp.transform);
            droppedSword.transform.position = transform.position;
        }

        PlayerScript.playerMoney += 10;

        SpawnEnemyScript.Despawn();
        Debug.Log($"{name} погиб!");
    }
}
