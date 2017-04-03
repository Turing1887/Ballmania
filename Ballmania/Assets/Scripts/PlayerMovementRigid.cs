using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRigid : MonoBehaviour
{

    public float acceleration;
    public float maxVel;
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
    void FixedUpdate()
    {
        moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal") * Time.deltaTime;
        moveVector.z = Input.GetAxis("Vertical") * Time.deltaTime;

        if (isGrounded)
        {
            Debug.Log("Grounded");
            if (Input.GetButtonDown("Jump") && Time.time >= nextDash)
            {
                verticalVelocity = jumpForce;
                nextDash = Time.time + cooldown;
            }
        }
        else
        {
            Debug.Log("Not Grounded");
            verticalVelocity = 0;
        }

        

        moveVector.y = 0;
        moveVector.Normalize();
        moveVector *= acceleration;
        moveVector.y = verticalVelocity;

        if (verticalVelocity > 0)
        {
            rb.AddForce(moveVector * Time.deltaTime * acceleration, ForceMode.Impulse);
        }
        else
        {
            if (rb.velocity.magnitude > maxVel && Time.time >= nextDash)
            {
                rb.velocity = rb.velocity.normalized * maxVel;
            }
            else
            {
                rb.AddForce(moveVector * Time.deltaTime * acceleration, ForceMode.Force);
            }
            
        }
        
    }

    private bool isGrounded = false;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }
}
