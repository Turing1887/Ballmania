using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovementRigid : NetworkBehaviour
{
    public float acceleration;
    public float maxVel;
    public float dashForce;
    public float dashDuration;
    public float jumpForce;
    public float nextDash = 0.0f;
    public float cooldown = 5.0f;

    private Rigidbody rb;

    private Vector3 moveVector;
    private float verticalVelocity;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {

            moveVector = Vector3.zero;
            moveVector.x = Input.GetAxis("Horizontal") * Time.deltaTime;
            moveVector.z = Input.GetAxis("Vertical") * Time.deltaTime;

            if (isGrounded)
            {
                //Debug.Log("Grounded");
                if (Input.GetButtonDown("Jump"))
                {
                    verticalVelocity = jumpForce;
                    moveVector.x = 0;
                    moveVector.z = 0;
                }
            }
            else
            {
                //Debug.Log("Not Grounded");
                verticalVelocity = 0;
            }

            moveVector.y = 0;
            moveVector.Normalize();
            moveVector *= acceleration;
            moveVector.y = verticalVelocity;

            if (Input.GetButtonDown("Dash") && nextDash >= 3f)
            {
                //verticalVelocity = dashForce;  
                rb.AddForce(moveVector * Time.deltaTime * dashForce, ForceMode.Impulse);
                nextDash = 0f;
            }
            else
            {
                nextDash += Time.deltaTime;
            }

            if (isGrounded)
            {
                if (rb.velocity.magnitude > maxVel && nextDash >= dashDuration)
                {
                    rb.velocity = rb.velocity.normalized * maxVel;
                }
                else
                {
                    rb.AddForce(moveVector * Time.deltaTime * acceleration, ForceMode.Impulse);
                }

            }

        }

    }

    private bool isGrounded = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (collision.gameObject.tag == "Player")
        {
            if (nextDash >= Time.time)
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(moveVector * 2, ForceMode.Impulse);
            }
        }


    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }

    //initialize things on starting the game but only for the local Player
    public override void OnStartLocalPlayer()
    {
        //GetComponent<MeshRenderer>().material.color = Color.blue;
    }

}
