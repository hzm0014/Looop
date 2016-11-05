using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Title : MonoBehaviour {

	int cnt;
	// Use this for initialization
	void Start () {
		cnt = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (cnt > 30) {
			if (Input.anyKey) {
				SceneManager.LoadScene ("Tutrial");
			}
		} else
			cnt++;
	}
}
