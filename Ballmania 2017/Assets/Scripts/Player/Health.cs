using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    public const int maxHealth = 5;

	[SyncVar(hook="OnHealthChange")]
    public int currentHealth = maxHealth;

	public GameObject[] lifePoints;
    //public GameObject healthUI;

    //private RectTransform healthBar;

    private NetworkStartPosition[] spawnPoints;

	void Start () {
		lifePoints = new GameObject[3];
		if(isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
			SetHealthPoints ();
            /*
            GameObject playerUI = Instantiate(healthUI) as GameObject;
            playerUI.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            healthBar = (RectTransform)playerUI.gameObject.transform.GetChild(1).GetChild(0);
            */
        }
	}
	
    [Command]
	public void CmdTakeDamage(int amount)
    {
        Debug.Log("damage");
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
                Destroy(gameObject);
        }
        RpcRespawn();
    }

//    void OnChangeHealth (int currentHealth)
//    {
//        //healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
//    }

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

	void SetHealthPoints(){
		GameObject healthHUD = GameObject.Find ("HUDCanvas/" + gameObject.name);
		Debug.Log (healthHUD.name);
		for (int i = 0; i < healthHUD.transform.childCount; i++) {
			Transform child = healthHUD.transform.GetChild (i);
			if (child.gameObject.tag == "Lifepoint") {
				Debug.Log ("Got it");
				lifePoints [i] = child.gameObject;
			}
		}
	}


	void OnHealthChange(int health){
		GameObject.Destroy (lifePoints[health]);
	}


}
