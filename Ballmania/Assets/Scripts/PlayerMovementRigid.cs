using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRigid : MonoBehaviour
{
    public Vector3 scaleVector;
    public float acceleration;
    public float maxVel;
    public float dashForce;
    public float jumpForce;
    public float nextDash = 0.0f;
    public float cooldown = 5.0f;
    public bool canUsePowerUp;

    private Rigidbody rb;

    private Vector3 moveVector;
    private float verticalVelocity;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canUsePowerUp = true;
    }

    // Update is called once per frame
    void FixedUpdate()
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

        if (Input.GetButtonDown("Dash") && Time.time >= nextDash)
        {
            verticalVelocity = dashForce;
            nextDash = Time.time + cooldown;
        }


        moveVector.y = 0;
        moveVector.Normalize();
        moveVector *= acceleration;
        moveVector.y = verticalVelocity;

        if (verticalVelocity > 0)
        {
            Debug.Log("Dash");
            rb.AddForce(moveVector * Time.deltaTime * acceleration, ForceMode.Impulse);
        }
        else if (isGrounded == true)
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
        if (collision.gameObject.tag == "ground")
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

    public IEnumerator StopSpeedUp()
    {
        yield return new WaitForSeconds(GameObject.FindWithTag("SpeedUp").GetComponent<SpeedUp>().durationSpeedUp);
        acceleration -= GameObject.FindWithTag("SpeedUp").GetComponent<SpeedUp>().accelerationPowerUp;
        maxVel -= GameObject.FindWithTag("SpeedUp").GetComponent<SpeedUp>().maxVelPowerUp;
        GameObject.FindGameObjectWithTag("SpeedUp").transform.Translate(100, 100, 100);
        canUsePowerUp = true;
        Destroy(GameObject.FindWithTag("SpeedUp"));

    }

    public IEnumerator StopMultipleDash()
    {
        yield return new WaitForSeconds(GameObject.FindWithTag("MultipleDash").GetComponent<MultipleDash>().durationSuperDash);
        GameObject.FindGameObjectWithTag("MultipleDash").transform.Translate(100, 100, 100);
        cooldown = 5f;
        canUsePowerUp = true;
        Destroy(GameObject.FindWithTag("MultipleDash"));

    }

    public IEnumerator StopSuperMass()
    {
        yield return new WaitForSeconds(GameObject.FindWithTag("SuperMass").GetComponent<SuperMass>().durationSuperMass);
        GameObject.FindWithTag("Player").transform.localScale -= GameObject.FindWithTag("SuperMass").GetComponent<SuperMass>().scaleVector;
        rb.GetComponent<Rigidbody>().mass -= GameObject.FindWithTag("SuperMass").GetComponent<SuperMass>().superMass;
        acceleration -= GameObject.FindWithTag("SuperMass").GetComponent<SuperMass>().superMassAcceleration;
        canUsePowerUp = true;
        GameObject.FindGameObjectWithTag("SuperMass").transform.Translate(100, 100, 100);
        Destroy(GameObject.FindWithTag("SuperMass"));

    }

}
