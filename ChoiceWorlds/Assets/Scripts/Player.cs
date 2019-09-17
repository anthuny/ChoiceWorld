using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 jump;
    public float jumpStrength = 2f;
    public float jumpDownWait = .4f;
    public float gravityStr;
    public float gravityNor;
    public bool jumping = false;
    public float hitWait = .3f;
    public float health = 5;
    public bool isShielding;
    public float healthDecAmount = 5f;
    public float healthShieldDecAmount = 2f;

    public bool UtilityOn;
    public bool DefenceOn;
    public bool AttackOn;
    public bool hitting;

    public Image healthBar; 
    public GameObject sword;
    public GameObject shield;

    public GameObject player;
    public Rigidbody rb;
    private Material playerMat;



    // Start is called before the first frame update
    void Start()
    {

        Physics.gravity = new Vector3(0, gravityNor, 0);

        jump = new Vector3(0.0f, jumpStrength, 0.0f);

        playerMat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
        PlayerJump();
        Shielding();
        StanceChange();
        Attacking();
    }

    void Attacking()
    {
        if (Input.GetMouseButton(0) && !hitting && !isShielding)
        {
            //sword.transform.Rotate(90, 0, 0);
            sword.transform.localRotation = Quaternion.Euler(90, 0, 0);
            hitting = true;
        }

        if (!Input.GetMouseButton(0))
        {
            //sword.transform.Rotate(0, 0, 0);
            sword.transform.localRotation = Quaternion.Euler(0, 0, 0);
            hitting = false;
        }
    }
    
    void Shielding()
    {
        if (Input.GetMouseButton(1))
        {
            shield.transform.localRotation = Quaternion.Euler(355, 0, 0);
            shield.transform.localPosition = new Vector3(-.2f, 0, .5f);
            shield.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);

            isShielding = true;
        }

        if (!Input.GetMouseButton(1))
        {
            shield.transform.localRotation = Quaternion.Euler(0, 293, 0);
            shield.transform.localPosition = new Vector3(-.5f, 0, .286f);
            shield.transform.localScale = new Vector3(1.3f,1.3f, 1.3f);

            isShielding = false;
        }
    }

    public void HealthDecrease()
    {
        //if player is shielding
        if (isShielding)
        {
            health -= healthShieldDecAmount;

            //Decrease health bar visual
            healthBar.fillAmount -= healthShieldDecAmount / 10f;
        }

        //if player is NOT shielding
        else
        {
            health -= healthDecAmount;

            //Decrease health bar visual
            healthBar.fillAmount -= healthDecAmount / 10f;
        }
    }

    void StanceChange()
    {
        //Utility on
        if (Input.GetKeyDown("1"))
        {
            UtilityOn = true;
            AttackOn = false;
            DefenceOn = false;
            playerMat.color = Color.green;
        }

        //Attack on
        if (Input.GetKeyDown("2"))
        {
            AttackOn = true;
            UtilityOn = false;
            DefenceOn = false;
            playerMat.color = Color.red;
        }

        //Defence on
        if (Input.GetKeyDown("3"))
        {
            DefenceOn = true;
            UtilityOn = false;
            AttackOn = false;
            playerMat.color = Color.blue;
        }
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
