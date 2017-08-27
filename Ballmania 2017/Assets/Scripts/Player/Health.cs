using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    public const int maxHealth = 5;

	[SyncVar(hook="OnHealthChange")]
    public int currentHealth = maxHealth;
	public GameObject healthHUD;
	public GameObject[] lifePoints;
	bool health_points_set;

    private NetworkStartPosition[] spawnPoints;
    private GameObject gameOverPanel;

	void Start () {
		health_points_set = true;
		lifePoints = new GameObject[3];
		if(isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
	}

	void Update(){
		if(ClientScene.ready && health_points_set){
            SetHealthPoints();
            health_points_set = false;
		}
			
	}

	public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        Debug.Log("damage");
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            if(isLocalPlayer)
            {
                DeathScreen();
            }
            
            Destroy(gameObject);
        }
        RpcRespawn();
    }


    [ClientRpc]
    void RpcRespawn()
    {
        Debug.Log("respawn");
        if (isLocalPlayer)
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

	void SetHealthPoints(){
		StartCoroutine (WaitASec());

	}

	IEnumerator WaitASec(){
		yield return new WaitForSeconds (4);
        healthHUD = GameObject.Find("HUDCanvas/" + gameObject.name);
        for (int i = 0; i < healthHUD.transform.childCount; i++)
        {
            Transform child = healthHUD.transform.GetChild(i);
            if (child.gameObject.tag == "Lifepoint")
            {
                lifePoints[i] = child.gameObject;
            }
        }
    }

	void OnHealthChange(int health){
		CmdDestroyHealthpoint (health);
	}


	[Command]
	void CmdDestroyHealthpoint(int health){
		GameObject.Destroy (lifePoints[health]);
	}

    void DeathScreen()
    {
            gameOverPanel = GameObject.Find("HUDCanvas/GameOverPanel");
            Debug.Log(gameOverPanel);
            Color tempCol = gameOverPanel.GetComponent<Image>().color;
            tempCol.a = 0.5f;
            gameOverPanel.GetComponent<Image>().color = tempCol;
            gameOverPanel.transform.Find("Defeat").gameObject.SetActive(true);
    }
}
