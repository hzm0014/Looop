using UnityEngine;
using System.Collections;

public class Player : Character {
	
	// Use this for initialization
	void Start () {
		speed.land = 0.3f;
		speed.sky = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Move(float horizon, bool grounded) {
		Vector2 pos = transform.position;
		if (grounded)
			pos.x += Input.GetAxis ("Horizontal") * speed.land;
		else
			pos.x += Input.GetAxis ("Horizontal") * speed.sky;
		transform.position = pos;			
	}
	public void Jump (bool grounded) {
		if (!grounded) return;
		Vector2 pos = transform.position;
		pos.y += 1.5f;
		transform.position = pos;
		this.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 1500);
	}
}
