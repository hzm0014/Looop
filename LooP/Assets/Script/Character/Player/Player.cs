using UnityEngine;
using System.Collections;

public class Player : Character {
	
	public GameObject bullet;
	public GameObject aim;

	// 画像系
	SpriteRenderer MainSpriteRenderer;
	public Sprite left, right;

	// Use this for initialization
	void Start () {
		speed.land = 0.3f;
		speed.sky = 0.2f;
		this.life = 10;
		this._atk = 100;
		// スプライト
		MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// 移動
	public void Move(float horizon, bool grounded) {
		pos = transform.position;
		if (grounded)
		pos.x += horizon * speed.land;
		else
		pos.x += horizon * speed.sky;
		// 向きを設定
		if (horizon > 0) {
			direction = 1;
			MainSpriteRenderer.sprite = right;
		}
		else if(horizon < 0) {
			direction = -1;
			MainSpriteRenderer.sprite = left;
		}
		
		transform.position = pos;
	}
	
	// ジャンプ
	public void Jump (bool grounded) {
		if (!grounded) return;
		this.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 1500);
	}
	
	// 特技；射撃
	public override void Specialty () {
		GameObject obj = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
		Kunai kunai = obj.GetComponent<Kunai>();
		Debug.Log(kunai.transform.rotation);
		kunai.SetBullet(transform.position, aim.transform.localEulerAngles);
		// 吹っ飛び
		Vector3 aimVec = aim.transform.localEulerAngles;
		Vector2 kunaiVec = new Vector2( -1*Mathf.Cos (Mathf.Deg2Rad * aimVec.z) *5.0f ,  -1*Mathf.Sin (Mathf.Deg2Rad * aimVec.z)*10.0f );
		//Debug.Log(kunaiVec);
		GetComponent<Rigidbody2D>().AddForce(kunaiVec, ForceMode2D.Impulse);
	}
	//ダメージ
	public override void Damage(IDamageGenerator damageGenerator){
		damage = damageGenerator.GetPower();
		force = damageGenerator.GetForce();
		forceSpeed = damageGenerator.GetForceSpeed();
		
		life = Mathf.Max (life - damage, 0);
		if (life <= 0) GameOver ();
		
		// ノックバック処理
		GetComponent<Rigidbody2D>().AddForce(force * 5.0f, ForceMode2D.Impulse);
		//Debug.Log(life);
	}
	//死亡関数
	public void GameOver() {
		Debug.Log("dead");
	}
}
