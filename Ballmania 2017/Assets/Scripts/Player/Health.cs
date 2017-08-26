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
    //public GameObject healthUI;
//	public GameObject[] healthUIs;
//	public List<string> activePlayers = new List<string>();
//	int listLength;
//	public GameObject[] activePlayers_new;

    //private RectTransform healthBar;

    private NetworkStartPosition[] spawnPoints;

	void Start () {
		health_points_set = true;
		lifePoints = new GameObject[3];
		if(isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
//			RpcSetHealthUI();
		
				
            /*
            GameObject playerUI = Instantiate(healthUI) as GameObject;
            playerUI.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            healthBar = (RectTransform)playerUI.gameObject.transform.GetChild(1).GetChild(0);
            */
        }
	}

	void Update(){
		if(ClientScene.ready && health_points_set){
			SetHealthPoints ();
			health_points_set = false;
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
		StartCoroutine (WaitASec());

	}

	IEnumerator WaitASec(){
		yield return new WaitForSeconds (4);
        healthHUD = GameObject.Find("HUDCanvas/" + gameObject.name);
        Debug.Log(healthHUD.name);
        for (int i = 0; i < healthHUD.transform.childCount; i++)
        {
            Transform child = healthHUD.transform.GetChild(i);
            if (child.gameObject.tag == "Lifepoint")
            {
                Debug.Log("Got it");
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




}
