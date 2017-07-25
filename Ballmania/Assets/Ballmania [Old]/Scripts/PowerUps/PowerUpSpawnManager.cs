using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour {

    public GameObject[] powerUps;
    public float spawnTime = 4f;
    public Transform[] spawnPoints;
    public bool spawning;
    

    // Use this for initialization
    

    void Start () {

        spawning = true;

        if (spawning)
        {
            InvokeRepeating("Spawn", spawnTime, spawnTime);
        }

    }
	
	// Update is called once per frame
	void Spawn () {

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Collider[] hitCollider = Physics.OverlapSphere(spawnPoints[spawnPointIndex].position, 0.1f);

        if (hitCollider.Length > 0.1)
        {
            StartCoroutine(Occupied());
        }


        else
        {
            int powerUpIndex = Random.Range(0, powerUps.Length);
            Instantiate(powerUps[powerUpIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
	}

   
    IEnumerator Occupied()
    {
        yield return new WaitForSeconds(1);
        spawning = false;
    }
}
