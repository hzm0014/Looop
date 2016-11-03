using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("AimH") <= 0 && Input.GetAxis ("AimV") <= 0)
			return;
		transform.rotation = Quaternion.Euler (0.0f, 0.0f, Mathf.Atan2 (Input.GetAxis ("AimV"), Input.GetAxis ("AimH")) * 60);
	}
}
