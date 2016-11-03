using UnityEngine;
using System.Collections;

public class Player : Character {
	
	public GameObject bullet;
	private Kunai kunai;
	public GameObject aim;


	// Use this for initialization
	void Start () {
		speed.land = 0.3f;
		speed.sky = 0.2f;
		kunai = GetComponent<Kunai> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// 移動
	public void Move(float horizon, bool grounded) {
		Vector2 pos = transform.position;
		if (grounded)
		pos.x += horizon * speed.land;
		else
		pos.x += horizon * speed.sky;
		// 向きを設定
		if (horizon > 0) {
			direction = 1;
			transform.rotation = Quaternion.Euler(0, 180, 0);
		}
		else if(horizon < 0) {
			direction = -1;
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		
		transform.position = pos;
	}
	
	// ジャンプ
	public void Jump (bool grounded) {
		if (!grounded) return;
		this.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 1500);
	}
	
	// 特技；射撃
	public void Specialty () {
		GameObject obj = (GameObject)Instantiate (Resources.Load ("Prefavs/Bullet/Kunai"));
		obj.GetComponent <Kunai> ().SetBullet(transform.position, aim.transform.localEulerAngles);
	}
}
