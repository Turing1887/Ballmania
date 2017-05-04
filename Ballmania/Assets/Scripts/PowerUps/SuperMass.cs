using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMass : MonoBehaviour
{
    public float rotationSpeed = 25f;
    public float durationSuperMass;
    public Vector3 scaleVector;
    public float superMass;
    public float superMassAcceleration;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
                GameObject.FindWithTag("Player").GetComponent<Rigidbody>().mass += superMass;
                playerScript.acceleration += superMassAcceleration;
                GameObject.FindWithTag("Player").transform.localScale += scaleVector;
                GameObject.FindGameObjectWithTag("SuperMass").transform.Translate(100, 100, 100);
                StartCoroutine(playerScript.StopSuperMass());
            }

            Debug.Log("Enter");
        }
    }
}

