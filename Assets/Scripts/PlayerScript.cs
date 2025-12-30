using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int currentHP;
    public int maxHP;
    public int playerMoney;
    public bool canBePressed = false;
    private GameObject hitEffect;
    private BloodPool bloodPool;


    private void Start()
    {
        maxHP = 100;
        currentHP = maxHP;
        playerMoney = 0;
    }

    private void Awake()
    {
        //bloodPoolAI = FindObjectOfType<BloodPoolAI>();
        if (!bloodPool) bloodPool = FindObjectOfType<BloodPool>();
    }

    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.R) && canBePressed)
        {
            Respawn();
        }
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
        }
    }

    private void Die()
    {
        currentHP = maxHP;
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
        Vector3 spawnPos = new Vector3(112.511f, 1.138f, 193.379f);

        playerMoney -= 50;
        gameObject.transform.position = spawnPos;
    }

    private void Respawn()
    {
        currentHP = maxHP;
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
        Vector3 spawnPos = new Vector3(112.511f, 1.138f, 193.379f);

        gameObject.transform.position = spawnPos;
    }
}
