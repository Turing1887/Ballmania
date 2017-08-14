using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ChangePlayerPrefabOnJoin : NetworkBehaviour {

	public GameObject lobbyManager;
	public ChangePlayerPrefab changePrefab;
	public GameObject blueball;



//	void Start () {
//		transform.gameObject.GetComponent<Button> ().onClick.AddListener (ChangePrefab);
//		lobbyManager = GameObject.Find ("LobbyManager");
//		changePrefab = lobbyManager.GetComponent<ChangePlayerPrefab> ();
//	}
//	


//	void ChangePrefab(){
////		transform.parent.gameObject.GetComponent<LobbyPlayer> ();
//		changePrefab.SendMessage();
//	}
}
