using UnityEngine;
using System.Collections;

public class Enemy : Character {
	
	public float turn;
	
	// Use this for initialization
	void Start () {
		pos = transform.position;
		this.direction = -1;
		this.turn = 10;
		this._atk = 1;
		this.forceSpeed = 5.0f;
		
		SetSpeed(0.3f, 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
		pos = transform.position;
		Move();
		//Debug.Log("aaaa");
	}
	
	public override void Move() {
		if(turn < 0) direction *= -1;
		else if(turn > 10) direction *= -1;
		
		pos = transform.position;
		pos.x += direction * speed.land;
		turn += 1*direction;
		
		transform.position = pos;
	}
	
	//接触判定
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player") {
			GameObject obj = other.transform.gameObject;
			Player p = obj.GetComponent<Player>();
			this.force = new Vector2((p.GetPosition().x - this.pos.x)*0.5f, 1.0f);

			p.Damage(this);
		}
	}
}
