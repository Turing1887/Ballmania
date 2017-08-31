using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Button> ().onClick.AddListener (Back);

	}
	

	void Back () {
		SceneManager.LoadScene ("MainMenu");
		GameObject.Find ("LobbyManager").GetComponent<CanvasGroup>().alpha = 0;
		GameObject.Find ("LobbyManager").GetComponent<CanvasGroup>().blocksRaycasts = false;
//		GameObject.Find ("LobbyManager").SetActive(false);
	}
}
