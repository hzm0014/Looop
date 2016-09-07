using UnityEngine;
using System.Collections;

public class Enemy : Character {
	
	Vector2 pos;
	float turn;
	
	// Use this for initialization
	void Start () {
		pos = transform.position;
		this.direction = -1;
		this.turn = 10;
		
		SetSpeed(0.3f, 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
		pos = transform.position;
		Move();
		//Debug.Log("aaaa");
	}
	
	private void Move() {
		if(turn < 0) direction *= -1;
		else if(turn > 10) direction *= -1;
		
		pos = transform.position;
		pos.x += direction * speed.land;
		turn += 1*direction;
		
		transform.position = pos;
	}
}
