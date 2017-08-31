using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReady : MonoBehaviour {

	public bool[] isReady;
	GameObject[] ready_txt;

	void Start () {
		ready_txt = new GameObject[4];
		ready_txt [0] = transform.FindChild ("ready_p1").gameObject;
		ready_txt [1] = transform.FindChild ("ready_p2").gameObject;
		ready_txt [2] = transform.FindChild ("ready_p3").gameObject;
		ready_txt [3] = transform.FindChild ("ready_p4").gameObject;

		isReady = new bool[4];

	}

	void Update () {
		if(Input.GetButtonDown("Activate_1")){
			ReadySlot ("Player_1");
		}
		else if(Input.GetButtonDown("Activate_2")){
			ReadySlot ("Player_2");
		}
		else if(Input.GetButtonDown("Activate_3")){
			ReadySlot ("Player_3");
		}
		else if(Input.GetButtonDown("Activate_4")){
			ReadySlot ("Player_4");
		}
	}

	void ReadySlot(string controllerName){
		for(int i = 0; i < isReady.Length; i++){
			if (isReady [i] == false) {
				isReady [i] = true;
				ready_txt [i].SetActive (true);
				PlayerPrefs.SetInt (controllerName, i);
				return;

			} 

		}

	}




}
