using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    GameObject[] healthUIs;

	void Start () {
        StartCoroutine(WaitSec());
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
}
