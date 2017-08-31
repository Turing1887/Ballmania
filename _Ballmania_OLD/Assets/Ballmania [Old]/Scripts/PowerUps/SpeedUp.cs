using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{

    public float rotationSpeed = 25f;
    
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
            PowerUpManager powerManagerScript = other.gameObject.GetComponent<PowerUpManager>();

            powerManagerScript.isSpeedUp = true;
            Destroy(this.gameObject);
           
            Debug.Log("SuperSpeed activated");
        }


    }
}
