using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public GameObject hook;
    public GameObject hookHolder;

    public float hookTravelSpeed;
    public float playerTravelSpeed;

    public bool hooked;
    public static bool fired;
    public GameObject hookedObj;

    public float maxDistance;
    private float currentDistance;

    private bool grounded;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !fired)
        {
            fired = true;
        }

        if (fired)
        {
            LineRenderer rope = hook.GetComponent<LineRenderer>();
            rope.SetVertexCount(2);
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }

        if (fired && !hooked)
        {
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDistance >= maxDistance)
            {
                ReturnHook();
            }
        }

        if (hooked && fired)
        {
            hook.transform.parent = hookedObj.transform;

            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

            this.GetComponent<Rigidbody>().useGravity = false;

            if (distanceToHook < 1)
            {
                if (!grounded)
                {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 80f);
                    this.transform.Translate(Vector3.up * Time.deltaTime * 120f);

                }

                StartCoroutine("Climb");
            }
        }

        else
        {
            hook.transform.parent = hookHolder.transform;

            this.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnHook();
    }

    void ReturnHook()
    {
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;

        LineRenderer rope = hook.GetComponent<LineRenderer>();
        rope.SetVertexCount(0);
    }

    void CheckIfGround()
    {
        RaycastHit hit;
        float distance = 1f;

        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            grounded = true;
        }

        else
        {
            grounded = false;
        }
    }
}
