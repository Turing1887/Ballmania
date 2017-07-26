using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpScript : MonoBehaviour {

    public float rotationSpeed = 25f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotationSpeed));
    }

    void OnTriggerEnter(Collider other)
    {
        // Collision mit einem Player
        if (other.tag.Equals("Player"))
        {
            PUManagerScript powerManagerScript = other.gameObject.GetComponent<PUManagerScript>();

            powerManagerScript.isSpeedUp = true;
            Destroy(this.gameObject);

            Debug.Log("SuperSpeed activated");
        }


    }
}
