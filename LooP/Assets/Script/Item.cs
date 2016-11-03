using UnityEngine;
using System.Collections;

/// <summary>
/// Item.
/// </summary>
public class Item : MonoBehaviour {
	public string target;
	public float power;
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			if (target == "life")
				other.gameObject.GetComponent<Player> ().Heal (power);
			else {
				other.gameObject.GetComponent<Player> ().StatusUp (target, power);
				Debug.Log ("power up: atk = " + other.gameObject.GetComponent<Player> ()._atk);
			}
			Destroy (gameObject);
		}
	}
}
