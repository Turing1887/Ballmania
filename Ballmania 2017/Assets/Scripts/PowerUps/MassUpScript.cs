using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MassUpScript : NetworkBehaviour
{

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
            PUManagerScript powerManager = other.gameObject.GetComponent<PUManagerScript>();
            powerManager.isMassUp = true;
            Destroy(this.gameObject);

            Debug.Log("Enter");
        }
    }
}
