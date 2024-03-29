﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Item.
/// </summary>
public class Item : MonoBehaviour {
	public string target;
	public float power;
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponent<Player> ().StatusUp(target, power);
			Destroy (gameObject);
		}
	}
}
