using UnityEngine;
using System.Collections;

public class Kunai : MonoBehaviour {

	float speed;
	float direction;
	Vector2 dropPos;

	// Use this for initialization
	void Awake () {
		speed = 0.7f;
		direction = 1;
	}

	// いろいろセッティング
	public void SetBullet(float direction, Vector2 pos) {
		this.direction = direction;
		dropPos = transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 v = transform.position;
		v.x += speed * direction;
		transform.position = v;
		if ((dropPos.x - v.x) * (dropPos.x - v.x) + (dropPos.y - v.y) * (dropPos.y - v.y) > 200)
			Destroy (gameObject);
	}
}
