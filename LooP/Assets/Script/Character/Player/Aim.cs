using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("AimH") == 0 && Input.GetAxis ("AimV") == 0)
			return;
		Vector2 v = transform.position;
		v.x = transform.parent.position.x + Input.GetAxis ("AimH");
		v.y = transform.parent.position.y + Input.GetAxis ("AimV");
		transform.position = v;
	
	}
}
