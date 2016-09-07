using UnityEngine;
using System.Collections;

public class Butachan : Enemy {
	
	// Use this for initialization
	void Start () {
		pos = transform.position;
		this.direction = -1;
		this.turn = 10;
		this.life = 10.0f;
		
		SetSpeed(0.1f, 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
		pos = transform.position;
		Move();
	}
	public override void Move() {
		if(turn < 0) this.Reverse();
		else if(turn > 10) this.Reverse();
		
		pos = transform.position;
		pos.x += direction * speed.land;
		turn += direction * speed.land;
		
		transform.position = pos;
	}
}
