using UnityEngine;
using System.Collections;

public class Boss : Enemy {
	
	public Player player;
	// 上下 : the rise and fall
	private float ra;

	public bool battoleMode;
	public float hitCount;
	
	// Use this for initialization
	void Start () {
		pos = transform.position;
		this.direction = -1;
		this.ra = 1;
		this._life = 10000.0f;
		this._atk = 2.0f;
		this.forceSpeed = 0.5f;
		this.force = new Vector2(0.5f, 0.5f);
		
		SetSpeed(0.05f, 0.02f);
		battoleMode = false;
		hitCount = 0;

		player = GameObject.Find ("Player").GetComponent<Player>();
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

	//ダメージ
	public override void Damage (IDamageGenerator damageGenerator) {
		damage = damageGenerator.GetPower ();
		force = damageGenerator.GetForce ();
		forceSpeed = damageGenerator.GetForceSpeed ();

		if (battoleMode) {
			_life = Mathf.Max (_life - damage, 0);
			if (_life <= 0) Dead ();
		} else {
			hitCount += damage;
		}
		Debug.Log ("boss: " + _life);
		GetComponent<Rigidbody2D> ().AddForce (force * 5.0f, ForceMode2D.Impulse);
	}


	//死亡関数
	public void Dead () {
		DungeonGeneratorII.Instance.ClearBossFloor ();
		Destroy (gameObject);
	}

	/// <summary>
	/// 戦闘モードへ移行
	/// </summary>
	/// <param name="max_life">Max life.</param>
	/// <param name="min_life">Minimum life.</param>
	public void ShiftButtleMode(float max_life, float min_life) {
		_life = Mathf.Max (max_life - hitCount, min_life);
		battoleMode = true;
	}
}
