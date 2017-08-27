using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HealthUI : NetworkBehaviour {

	public GameObject[] healthUIs;
	public List<string> activePlayers = new List<string>();
	int listLength;
	public GameObject[] activePlayers_new;

	void Start () {
		StartCoroutine(Wait());
	}

	//	IEnumerator WaitSec()
	//    {
	//        yield return new WaitForSeconds(0.5f);
	//        healthUIs = GameObject.FindGameObjectsWithTag("HealthUI");
	//        for (int i = 0; i < healthUIs.Length; i++)
	//        {
	//            Text childText = this.gameObject.GetComponentInChildren<Text>();
	//            Vector3 tempPos = healthUIs[i].transform.position;
	//            tempPos.y -= 30f * i;
	//            healthUIs[i].transform.position = tempPos;
	//        }
	//    }
	//	[ClientRpc]
	//	void RpcSetHealthUI(){
	////		activePlayers.Add ();
	//		activePlayers_new = GameObject.FindGameObjectsWithTag("Player"); 
	//		for(int i = 0;i < activePlayers_new.Length;i++){
	//			activePlayers.Add (activePlayers_new[i].name);
	//		}
	//		listLength = activePlayers.Count;
	//		healthUIs = GameObject.FindGameObjectsWithTag("HealthUI");
	//		for(int i = 0;i < listLength;i++){
	//			healthUIs [i].GetComponent<CanvasGroup> ().alpha = 1;
	//			healthUIs[i].GetComponentInChildren<Text>().text = activePlayers[i];
	//			healthUIs[i].name = activePlayers[i];
	//		}
	//
	//	}
	[ClientRpc]
	void RpcSetHealthUI(){
		//		activePlayers.Add ();
		activePlayers_new = GameObject.FindGameObjectsWithTag("Player"); 
		for(int i = 0;i < activePlayers_new.Length;i++){
			activePlayers.Add (activePlayers_new[i].name);
		}
		listLength = activePlayers.Count;
		healthUIs = GameObject.FindGameObjectsWithTag("HealthUI");
		for(int i = 0;i < listLength;i++){
			healthUIs [i].GetComponent<CanvasGroup> ().alpha = 1;
			healthUIs[i].GetComponentInChildren<Text>().text = activePlayers[i];
			healthUIs[i].name = activePlayers[i];
		}

	}

	public IEnumerator Wait()
	{
		yield return new WaitForSeconds(2f);
        if (isServer)
        {
            RpcSetHealthUI();
        }
	}


}
