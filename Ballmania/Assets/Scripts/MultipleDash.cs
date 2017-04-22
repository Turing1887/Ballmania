using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleDash : MonoBehaviour {

    public float cooldownPowerUp;
    public float durationSuperDash;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals("Player"))
        {
            PlayerMovementRigid playerScript = other.gameObject.GetComponent<PlayerMovementRigid>();

            if (playerScript)
            {
                playerScript.cooldown = cooldownPowerUp;

                StartCoroutine(playerScript.StopMultipleDash());
            }
            Debug.Log("Super Dash enabled");
        }


    }
}
