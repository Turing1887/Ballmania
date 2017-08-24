using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HealthUI : NetworkBehaviour {

    GameObject[] healthUIs;
	public List<string> activePlayers = new List<string>();
	int listLength;

	void Start () {
//        StartCoroutine(WaitSec());
		CmdSetHealthUI();
	}
	
	IEnumerator WaitSec()
    {
        yield return new WaitForSeconds(0.5f);
        healthUIs = GameObject.FindGameObjectsWithTag("HealthUI");
        for (int i = 0; i < healthUIs.Length; i++)
        {
            Text childText = this.gameObject.GetComponentInChildren<Text>();
            Vector3 tempPos = healthUIs[i].transform.position;
            tempPos.y -= 30f * i;
            healthUIs[i].transform.position = tempPos;
        }
    }
	[Command]
	void CmdSetHealthUI(){
		listLength = activePlayers.Count;
		healthUIs = GameObject.FindGameObjectsWithTag("HealthUI");
		for(int i = 0;i < listLength;i++){
			healthUIs [i].GetComponent<CanvasGroup> ().alpha = 1;
			healthUIs[i].GetComponentInChildren<Text>().text = activePlayers[i];
			healthUIs[i].name = activePlayers[i];
		}

	}


}
