using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    public const int maxHealth = 5;

    public int currentHealth = maxHealth;

    private NetworkStartPosition[] spawnPoints;

	void Start () {
		if(isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
	}
	
	public void TakeDamage(int amount)
    {
        if(!isServer)
        {
            return;
        }

        currentHealth -= amount;
        if(currentHealth <= 0)
        {
                Destroy(gameObject);
        }
        RpcRespawn();
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if(isLocalPlayer)
        {
            // default Respawn
            Vector3 spawnPoint = Vector3.zero;

            //Wenn ein Spawnpoint Array existiert das nicht leer ist
            if(spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            transform.position = spawnPoint;
        }
    }
}
