using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class ChangePlayerPrefab : LobbyHook {
	public GameObject redball;
	public GameObject blueball;
	public GameObject lobbyManager;


	void Start(){
		lobbyManager = GameObject.Find ("LobbyManager");
	}



	public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer){
		
		if(lobbyPlayer.gameObject.GetComponent<LobbyPlayer> ().playernumber == 1){
			manager.gameObject.GetComponent<LobbyManager> ().gamePlayerPrefab = redball;
//			Debug.Log (lobbyPlayer.name);
		}
		else {
			manager.gameObject.GetComponent<LobbyManager> ().gamePlayerPrefab = blueball;
//			Debug.Log (lobbyPlayer.name);
		}

	}




}
