using UnityEngine;
using System.Collections;

/// <summary>
/// Item.
/// </summary>
public class Item : MonoBehaviour {
	public float power;
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponent<Player> ()._atk += power;
			Debug.Log ("power up: atk = " + other.gameObject.GetComponent<Player> ()._atk);
			Destroy (gameObject);
		}
	}
}
