using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

    float mass = 3.0f;
    Vector3 impact = Vector3.zero;
    private CharacterController controller;

	void Start () {
        controller = GetComponent<CharacterController>();
	}

	void Update () {
		if(impact.magnitude > 0.2f)
        {
            controller.Move(impact * Time.deltaTime);
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
        }
	}

    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if(dir.y < 0)
        {
            dir.y = -dir.y;
            impact += dir.normalized * force / mass;
        }
    }
}
