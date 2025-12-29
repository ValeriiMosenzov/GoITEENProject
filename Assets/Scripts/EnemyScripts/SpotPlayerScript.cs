using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPlayerScript : MonoBehaviour
{
    //Transform
    public Transform Enemy;
    public Transform Player;

    //Scripts
    public WalkEnemyScript WalkEnemyScript;


    //Animatior
    public Animator root;


    //Data
    public float speed;
    public float rotationSpeed;
    public bool spotted = false;
    public bool once = false;


    private void Start()
    {
        Enemy = transform.parent;
        WalkEnemyScript = Enemy.GetComponent<WalkEnemyScript>();
        speed = WalkEnemyScript.speed;
        rotationSpeed = WalkEnemyScript.rotationSpeed;
        root = transform.parent.GetChild(4).GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        if (spotted)
        {
            Vector3 direction = (Player.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Enemy.rotation = Quaternion.Lerp(
                    Enemy.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            Enemy.position = Vector3.MoveTowards(
                Enemy.position,
                Player.position,
                speed * Time.deltaTime
            );
            root.SetBool("IsRunning", true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WalkEnemyScript.currentPoint = 0;
            WalkEnemyScript.enabled = false;
            Debug.Log("Walk script was turned off");
            spotted = true;
        }
    }
}
