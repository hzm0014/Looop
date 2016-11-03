using UnityEngine;
using System.Collections;

public class Boss : Enemy {
	
	public Player player;
	// 上下 : the rise and fall
	private float ra;
	
	// Use this for initialization
	void Start () {
		pos = transform.position;
		this.direction = -1;
		this.ra = 1;
		this.life = 100.0f;
		this.power = 2.0f;
		this.forceSpeed = 2.0f;
		
		SetSpeed(0.05f, 0.02f);
	}
	
	// Update is called once per frame
	void Update () {
		pos = transform.position;
		Move();
	}
	
	// 移動アルゴリズム
	public override void Move() {
		Vector2 playerPosition = player.GetPosition();
		if(playerPosition.x > pos.x && direction == -1) this.Reverse();
		else if(playerPosition.x < pos.x && direction == 1) this.Reverse();
		
		if(playerPosition.y > pos.y && ra == -1) ra = 1;
		else if(playerPosition.y < pos.y && ra == 1) ra = -1;
		else if(playerPosition.y == pos.y) ra = 0;
		
		pos = transform.position;
		pos.x += direction * speed.sky;
		pos.y += ra * speed.sky;
		
		transform.position = pos;
	}
}
