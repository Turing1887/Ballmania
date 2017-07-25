using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerController2 : MonoBehaviour {

    public float speed;
    public float thrust;
    public float jump;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            print("pressed");
            rb.velocity = new Vector3(thrust, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.S))
        {
            print("pressed");
            rb.velocity = new Vector3(-thrust, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
        {
            print("pressed");
            rb.velocity = new Vector3(0, 0, thrust);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            print("pressed");
            rb.velocity = new Vector3(0, 0, -thrust);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("pressed");
            rb.velocity = new Vector3(0, jump, 0);
        }
    }
}
