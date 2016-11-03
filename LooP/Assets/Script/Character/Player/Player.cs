using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Character {
	
	public GameObject bullet;
	public GameObject aim;
	
	public GameObject lifeUI;
	
	// 画像系
	SpriteRenderer MainSpriteRenderer;
	public Sprite left, right;
	
	// 点滅用
	private Renderer renderer;
	// 当たり判定用
	private bool hit;
	
	// Use this for initialization
	void Start () {
		hit = false;
		speed.land = 0.3f;
		speed.sky = 0.2f;
		_life = _maxLife = 10;
		_atk = 100;
		// スプライト
		MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		// UIの更新
		UpdateLifeUI ();
		// 無敵時間の点滅用
		renderer = GetComponent<Renderer>();
	}
	
	// 移動
	public void Move (float horizon, bool grounded) {
		pos = transform.position;
		if (grounded)
		pos.x += horizon * speed.land;
		else
		pos.x += horizon * speed.sky;
		// 向きを設定
		if (horizon > 0) {
			direction = 1;
			MainSpriteRenderer.sprite = right;
		} else if (horizon < 0) {
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
		if( hit ){
			hit = false;
			damage = damageGenerator.GetPower();
			force = damageGenerator.GetForce();
			forceSpeed = damageGenerator.GetForceSpeed();
			
			_life = Mathf.Max (_life - damage, 0);
			
			// ライフのUIをUpdate
			UpdateLifeUI();
			
			// ライフが0でゲームオーバーへ
			if (_life <= 0) GameOver();
			
			// 無敵時間のコルーチンを開始
			StartCoroutine("Invincible");
			
			// ノックバック処理
			GetComponent<Rigidbody2D>().AddForce(force * 5.0f, ForceMode2D.Impulse);
			//Debug.Log(life);
		}
	}
	// 死亡関数
	public void GameOver () {
		Debug.Log ("dead");
	}
	
	// 無敵時間コルーチン内部
	IEnumerator Invincible() {
		
		//レイヤーをPlayerDamageに変更
		gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
		//while文を10回ループ
		int count = 10;
		while(count > 0) {
			//透明にする
			renderer.material.color = new Color (1,1,1,0);
			//0.05秒待つ
			yield return new WaitForSeconds(0.05f);
			//元に戻す
			renderer.material.color = new Color (1,1,1,1);
			//0.05秒待つ
			yield return new WaitForSeconds(0.05f);
			count--;
		}
		//レイヤーをPlayerに戻す
		gameObject.layer = LayerMask.NameToLayer("Player");
	}
	
	/// <summary>
	/// ライフのUIを更新
	/// </summary>
	static Text text = null;
	private void UpdateLifeUI() {
		if(text == null)
		text = lifeUI.GetComponent<Text> ();
		text.text= _life + " / " + _maxLife;
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Enemy") {
			hit = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Enemy") {
			hit = false;
		}
	}
}
