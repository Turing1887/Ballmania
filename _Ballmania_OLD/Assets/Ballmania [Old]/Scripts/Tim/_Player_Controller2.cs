using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {

	public float speed;
	public float thrust;
	public float jump;
	private bool isDashing;
	private bool isJumping;
	public float dashpower;
	private float maxspeed = 100.0f;
	float moveHorizontal;
	float moveVertical;
	private Rigidbody rb;
	private Vector3 movement;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		isDashing = false;
		isJumping = false;

	}

	private void Update()
	{

		Move ();
		StartCoroutine (Dash());


	}

	IEnumerator Dash(){
//		if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
//		{
//			print("pressed");
//			rb.velocity = new Vector3(thrust, 0, 0);
//		}
//		else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.S))
//		{
//			print("pressed");
//			rb.velocity = new Vector3(-thrust, 0, 0);
//		}
//		else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
//		{
//			print("pressed");
//			rb.velocity = new Vector3(0, 0, thrust);
//		}
//		else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
//		{
//			print("pressed");
//			rb.velocity = new Vector3(0, 0, -thrust);
//		}
		if(Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
		{
			
			rb.AddRelativeForce (transform.forward*20,ForceMode.Impulse);
			isDashing = true;
			yield return new WaitForSeconds (0.5f);
			isDashing = false;
		}	

		if (Input.GetKeyDown(KeyCode.Space))
		{
			print("pressed");
			rb.AddForce(0, jump, 0,ForceMode.Impulse);
			isJumping = true;
			yield return new WaitForSeconds (0.5f);
			isJumping = false;
		}


	}

	void Move(){
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce(movement * speed);
	}

}
