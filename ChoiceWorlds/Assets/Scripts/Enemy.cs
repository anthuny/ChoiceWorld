using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public bool shooting;
    public float shootRange;
    public float e_Health = 15f;
    public float recievedDam = 5f;
    private float recievedHitTimer = 0f;
    public float recievedHitCD = 1f;
    public bool gettingHit;
    public Image healthBar;

    public float shootCooldown = 2f;

    private NavMeshAgent navAgent;
    private GameObject target;
    public GameObject bullet;
    
    private void Awake()
    {
        target = GameObject.FindWithTag("Player");

        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        StartCoroutine("Shoot");
        EnemyHitTimer();
    }

    void EnemyHitTimer()
    {
        if (gettingHit)
        {
            recievedHitTimer += Time.deltaTime;

            if (recievedHitTimer >= recievedHitCD)
            {
                gettingHit = false;

                recievedHitTimer = 0;
            }
        }   
    }
    private void OnTriggerStay(Collider other)
    {
        gettingHit = true;

        Player playerScript = target.GetComponent<Player>();

        if (other.gameObject.tag == "Sword" && recievedHitTimer == 0 && playerScript.hitting)
        {
            e_Health -= recievedDam;
            healthBar.fillAmount -= recievedDam / 100;
            Debug.Log("hit ");

            if (healthBar.fillAmount <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    void Walk()
    {
        float dist = Vector3.Distance(target.transform.position, transform.position);

        //If enemy is in shooting range, stop walking towards player
        if (dist > shootRange)
        {
            navAgent.SetDestination(target.transform.position);
        }

        else
        {
            //Look at the player
            transform.LookAt(target.transform.position);

            //Stop moving
            navAgent.SetDestination(transform.position);
        }
    }

    IEnumerator Shoot()
    {
        if (!shooting)
        {
            float dist = Vector3.Distance(target.transform.position, transform.position);

            //If enemy is in range of player, shoot
            if (shootRange > dist)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                shooting = true;
                yield return new WaitForSeconds(shootCooldown);
                shooting = false;
            }
        }
    }

}
