using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUSpawnManagerScript : MonoBehaviour {

    public GameObject[] powerUps;
    public float spawnTime = 4f;
    public Transform[] spawnPoints;
    public bool spawning;


    void Start () {
        spawning = true;

        if (spawning)
        {
            InvokeRepeating("Spawn", spawnTime, spawnTime);
        }
    }

    // Spawn Funktion
    void Spawn()
    {
        // Zufälliger Spawnpunkt
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Collider[] hitCollider = Physics.OverlapSphere(spawnPoints[spawnPointIndex].position, 0.1f);

        // Platz besetzt
        if (hitCollider.Length > 0.1)
        {
            StartCoroutine(Occupied());
        }

        // nicht besetzt
        else
        {
            int powerUpIndex = Random.Range(0, powerUps.Length);
            Instantiate(powerUps[powerUpIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }

    // Warte 1 Sekunde
    IEnumerator Occupied()
    {
        yield return new WaitForSeconds(1);
        spawning = false;
    }
}
