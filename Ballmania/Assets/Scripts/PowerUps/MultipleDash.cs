using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleDash : MonoBehaviour {

    public float rotationSpeed = 25f;
    public float cooldownPowerUp;
    public float durationSuperDash;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotationSpeed));
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals("Player"))
        {
            PlayerMovementRigid playerScript = other.gameObject.GetComponent<PlayerMovementRigid>();

            if (playerScript && playerScript.canUsePowerUp == true)
            {
                playerScript.canUsePowerUp = false;
                playerScript.cooldown = cooldownPowerUp;
                GameObject.FindGameObjectWithTag("MultipleDash").transform.Translate(100, 100, 100);
                StartCoroutine(playerScript.StopMultipleDash());
            }
            Debug.Log("Super Dash enabled");
        }


    }
}
