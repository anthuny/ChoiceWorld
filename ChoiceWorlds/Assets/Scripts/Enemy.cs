using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool shooting;

    public float shootCooldown = 2f;

    private NavMeshAgent navAgent;
    public GameObject target;
    public GameObject bullet;
    


    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navAgent.SetDestination(target.transform.position);
        StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        if (!shooting)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            shooting = true;
            yield return new WaitForSeconds(shootCooldown);
            shooting = false;
        }
    }

}
