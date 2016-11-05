using UnityEngine;
using System.Collections;

public class Kakashi: Enemy {
	
	// Use this for initialization
	void Start () {
		pos = transform.position;
		this.direction = -1;
		this.turn = 10;
		this._life = 10.0f;
		this._atk = 1.0f;
		this.forceSpeed = 0.1f;
		SetSpeed(0.1f, 0.3f);
	}
	
	public void Jump (bool grounded) {
		if (!grounded) return;
		Vector2 pos = transform.position;
		pos.y += 1.5f;
		transform.position = pos;
		this.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 1500);
	}
	
	// Update is called once per frame
	void Update () {
		pos = transform.position;
		Move();
	}
	public override void Move() {

	}
}
