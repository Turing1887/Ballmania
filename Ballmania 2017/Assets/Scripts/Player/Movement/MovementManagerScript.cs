using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MovementManagerScript : NetworkBehaviour {

    public float acceleration;
    public float maxVel;

    public float dashForce;
    public float dashDuration;
    [SyncVar]
    public float nextDash;
    public float cooldown;

    public float jumpForce;

    public float deathDepth;
    public int damage;

    private Rigidbody rb;
    private Renderer ren;

    private Vector3 moveVector;
    private float verticalVelocity;

    Health health;

    private bool tempDeath = false;

    [SyncVar]
    public Color color;
    [SyncVar]
    public string playerName;

    void Start () {
        rb = GetComponent<Rigidbody>();
        ren = GetComponent<Renderer>();
        ren.material.color = color;
        this.name = playerName;
        health = gameObject.GetComponent<Health>();
        tempDeath = false;
    }

	void Update () {
        if (isLocalPlayer)
        {
            if (transform.position.y < deathDepth && tempDeath == false)
            {
                tempDeath = true;
                health.CmdTakeDamage(damage);
                rb.velocity = Vector3.zero;
            }
            else if (transform.position.y > deathDepth)
            {
                tempDeath = false;
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
    private void OnCollisionStay(Collision collision)
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Player Check
        if (collision.gameObject.tag == "Player")
        {
			string player_name = collision.gameObject.name;
            if (nextDash <= cooldown && nextDash > 0f)
            {
                collision.gameObject.SendMessage("Hit",rb.velocity);
            }
        }
    }

    public void Hit(Vector3 vel) 
    {
        Debug.Log(vel);
        rb.AddForce(rb.velocity - vel * 10);
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
