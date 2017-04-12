using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReady : MonoBehaviour {

	public bool[] isReady;
	int readyCounter;
	GameObject[] ready_txt;

	void Start () {
		ready_txt = new GameObject[4];
		ready_txt [0] = transform.FindChild ("ready_p1").gameObject;
		ready_txt [1] = transform.FindChild ("ready_p2").gameObject;
		ready_txt [2] = transform.FindChild ("ready_p3").gameObject;
		ready_txt [3] = transform.FindChild ("ready_p4").gameObject;

		isReady = new bool[4];
		readyCounter = 0;
	}

	void Update () {
		if(Input.GetButton("Activate")){
			for(int i = 0;i < isReady.Length;i++){
				if (isReady [i] == false) {
					isReady [i] = true;
					readyCounter++;
					switch (readyCounter) {
					case 1:
						ready_txt [0].SetActive (true);
						break;
					case 2:
						ready_txt [1].SetActive (true);
						break;
					case 3:
						ready_txt [2].SetActive (true);
						break;
					case 4:
						ready_txt [3].SetActive (true);
						break;

					}

				} else {
					continue;
				}
				break;

			}
		}	
	}
}
