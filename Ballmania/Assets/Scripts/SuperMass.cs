using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMass : MonoBehaviour
{
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

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals("Player"))
        {
            PlayerMovementRigid playerScript = other.gameObject.GetComponent<PlayerMovementRigid>();

            if (playerScript)
            {
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

