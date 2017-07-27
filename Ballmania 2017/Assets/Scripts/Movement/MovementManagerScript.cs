﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MovementManagerScript : NetworkBehaviour {

    public float acceleration;
    public float maxVel;

    public float dashForce;
    public float dashDuration;
    public float nextDash = 0.0f;
    public float cooldown = 5.0f;

    public float jumpForce;

    public float deathDepth = 5f;

    private Rigidbody rb;

    private Vector3 moveVector;
    private float verticalVelocity;

    LifeManagement lM;

    void Start () {
        rb = GetComponent<Rigidbody>();
        lM = GameObject.Find("Game Manager").GetComponent<LifeManagement>();
    }

	void Update () {
        if (isLocalPlayer)
        {
            if(transform.position.y < deathDepth)
            {
                //lM.LifeDown();
                Destroy(gameObject);
            }
            // Tasten auslesen um Bewegungsvektor herauszufinden
            moveVector = Vector3.zero;
            moveVector.x = Input.GetAxis("Horizontal") * Time.deltaTime;
            moveVector.z = Input.GetAxis("Vertical") * Time.deltaTime;

            // Jump
            if (isGrounded)
            {
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
            // Jump Ende

            // Bewegungsvektor normalisieren und mit der Beschleunigung multiplizieren
            moveVector.y = 0;
            moveVector.Normalize();
            moveVector *= acceleration;
            moveVector.y = verticalVelocity;


            // Dash
            if (Input.GetButtonDown("Dash") && nextDash >= cooldown)
            {  
                rb.AddForce(moveVector * Time.deltaTime * dashForce, ForceMode.Impulse);
                nextDash = 0f;
            }
            else
            {
                nextDash += Time.deltaTime;
            }
            // Dash Ende

            // Bewegung anwenden
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
            // Bewegung Ende

        }
    }

    // Collision Detection
    private bool isGrounded = false;
    private void OnCollisionEnter(Collision collision)
    {
        // Ground Check
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // Player Check
        if (collision.gameObject.tag == "Player")
        {
            MovementManagerScript mvManSc = collision.gameObject.GetComponent<MovementManagerScript>();
            // Dashed der andere Spieler gerade?
            if (mvManSc.nextDash <= 3f)
            {
                Debug.Log("Collision");
            }
        }


    }
    // Collision Detection Ende

    // Collision Exit
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }
}
