using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadLobby : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Button> ().onClick.AddListener (Load);
	}
	void Load(){
		if(GameObject.Find("LobbyManager")){
			GameObject.Find ("LobbyManager").GetComponent<CanvasGroup>().alpha = 1;
			GameObject.Find ("LobbyManager").GetComponent<CanvasGroup>().blocksRaycasts = true;
		}
		SceneManager.LoadScene ("Lobby Scene");
	}
	

}
