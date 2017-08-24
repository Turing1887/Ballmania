﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

namespace Prototype.NetworkLobby
{
    public class GameLobbyHook : LobbyHook {
		public HealthUI healthUI;
        public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
        {
            LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
            MovementManagerScript player = gamePlayer.GetComponent<MovementManagerScript>();
            player.playerName = lobby.playerName;
            player.color = lobby.playerColor;
	

//			HealthUI healthUI = GameObject.Find ("GameManager").GetComponent<HealthUI>();
////			healthUI = NetworkServer.FindLocalObject();
//			healthUI.activePlayers.Add (player.playerName);


        }
    }
}

