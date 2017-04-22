using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{

    public float durationSpeedUp;
    public float accelerationPowerUp;
    public float maxVelPowerUp;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals("Player"))
        {
            PlayerMovementRigid playerScript = other.gameObject.GetComponent<PlayerMovementRigid>();

            if (playerScript)
            {
                playerScript.acceleration += accelerationPowerUp;
                playerScript.maxVel += maxVelPowerUp;
                GameObject.FindGameObjectWithTag("SpeedUp").transform.Translate(100, 100, 100);
                StartCoroutine(playerScript.StopSpeedUp());
            }
            Debug.Log("SuperSpeed activated");
        }


    }
}
