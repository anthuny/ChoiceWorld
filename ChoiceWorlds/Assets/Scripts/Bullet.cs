using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 4f;
    public float hitRadius = 4f;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        BulletDie();
    }

    void Shoot()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
    }

    void BulletDie()
    {
        float dist = Vector3.Distance(target.transform.position, transform.position);

        if (dist < hitRadius)
        {
            Player playerScript = target.GetComponent<Player>();
            playerScript.HealthDecrease();

            Destroy(gameObject);
        }
    }
}
