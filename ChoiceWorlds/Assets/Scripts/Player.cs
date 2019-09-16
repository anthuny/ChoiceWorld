using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 jump;
    public float jumpStrength = 2f;
    public float jumpDownWait = .4f;
    public float gravityStr;
    public float gravityNor;
    public bool jumping = false;

    public GameObject player;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, gravityNor, 0);

        jump = new Vector3(0.0f, jumpStrength, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Physics.gravity);
        PlayerMovement();
        PlayerJump();
    }

    void PlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(hor, 0f, ver) * speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown("space") && !jumping)
        {
            //Set jumping to true
            jumping = true;

            rb.AddForce(jump * jumpStrength, ForceMode.Impulse);
            Invoke("PlayerJumpDown", jumpDownWait);
        }
    }

    void PlayerJumpDown()
    {
        Physics.gravity = new Vector3(0, gravityStr, 0); ;
        Invoke("ResetJump", .5f);
    }

    void ResetJump()
    {
        Physics.gravity = new Vector3(0, gravityNor, 0);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Floor")
        {
            //Set jumping to false if player collides with the floor
            jumping = false;
        }
    }
}
