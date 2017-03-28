using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float jumpForce = 10.0f;
    public float gravity = 14.0f;

    private float verticalVelocity;
    private Vector3 localVector;
    private Vector3 lastMove;

    private int delay = 0;
    public float dashDistance = 10;

    private CharacterController controller;

	void Start () {
        controller = GetComponent<CharacterController>();
	}
	
    //Movement & Jump
	void Update () {
        localVector = Vector3.zero;
        localVector.z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        localVector.x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        if (controller.isGrounded)
        {
            verticalVelocity = -1;
            if(Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            localVector = lastMove;
        }

        localVector.y = 0;
        localVector.Normalize();
        localVector *= speed;
        localVector.y = verticalVelocity;

        if(Input.GetButtonDown("Dash") && delay > 60)
        {
            controller.Move(new Vector3(localVector.x * dashDistance * Time.deltaTime, localVector.y * Time.deltaTime, localVector.z * dashDistance * Time.deltaTime));
            delay = 0;
        }
        else
        {
            controller.Move(localVector * Time.deltaTime);
        }
        
        lastMove = localVector;
        delay++;
	}

    //Walljump
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!controller.isGrounded && hit.normal.y < 0.1f)
        {
            if(Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
                localVector = hit.normal * speed;
            }
            //Debug.DrawRay(hit.point, hit.normal, Color.red, 1.25f);
        }
       
    }
}
