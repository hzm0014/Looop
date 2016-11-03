﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey) {
			SceneManager.LoadScene ("Game");
			DungeonGeneratorII.Instance.GenerateDungeon ();
			gameObject.SetActive (false);
		}
	}
}
