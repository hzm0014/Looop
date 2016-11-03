using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour ,IDamageGenerator {
	//構造体宣言部
	protected struct Speed {
		public float land, sky;
		public Speed(float land, float sky) {
			this.land = land;
			this.sky = sky;
		}
	}
	
	//変数宣言部
	protected Speed speed; //素早さ
	public float _maxLife { get; set;}	// 最大命
	public float _life { get; set;} 		// 命
	public float _atk { get; set;} //攻撃力
	protected float defense; //守備力
	protected float direction; //方向
	protected float damage; //今受けたダメージ
	protected Vector2 force; //吹き飛ばされる
	protected float forceSpeed; //吹き飛ばされるスピード
	protected Vector2 pos;
	
	//関数宣言部
	// 初期化
	void Start () {
	}
	
	// 繰り返しを記入
	void Update () {

	}
	
	//set関数群
	// [S1] スピードの設定
	public void SetSpeed(float land,float sky) {
		speed.land = land;
		speed.sky = sky;
	}
	// [S2] 地上スピードの設定
	public void SetSpeedLand(float land) {
		speed.land = land;
	}
	// [S3] 空中スピードの設定
	public void SetSpeedSky(float sky) {
		speed.sky = sky;
	}
	// [S4] ライフの設定
	public void SetLife(float life) {
		this._life = life;
	}
	// [S5] 攻撃力の設定
	public void SetPower(float power) {
		this._atk = power;
	}
	// [S5] 防御力の設定
	public void SetDefense(float defense) {
		this.defense = defense;
	}
	// [S7] 左右の向きの設定
	public void SetDirection(float direction) {
		this.direction = direction;
	}
	// [S8] ノックバック力の設定
	public void SetForce(Vector2 force) {
		this.force = force;
	}
	// [S9] ノックバックスピードの設定
	public void SetForceSpeed(float forceSpeed) {
		this.forceSpeed = forceSpeed;
	}
	
	//get関数群
	// [G1] 地上スピード
	public float GetSpeedLand() {
		return speed.land;
	}
	// [G2] 空中スピード
	public float GetSpeedSky() {
		return speed.sky;
	}
	// [G3] ライフ
	public float GetLife() {
		return _life;
	}
	// [G4] 攻撃力
	public float GetPower() {
		return _atk;
	}
	// [G5] 防御力
	public float GetDefense() {
		return defense;
	}
	// [G6] 左右の向き
	public float GetDirection() {
		return direction;
	}
	// [G7] ノックバック力
	public Vector2 GetForce() {
		return force;
	}
	// [G8] ノックバックスピード
	public float GetForceSpeed() {
		return forceSpeed;
	}
	// [G9] 現在地
	public Vector2 GetPosition(){
		return pos;
	}
	
	//行動関係群
	// 移動
	public virtual void Move() {}
	//ジャンプ
	public virtual void Jump() {}
	// アクション
	public virtual void Action() {}
	// 通常攻撃
	public virtual void Attack() {}
	// 特技
	public virtual void Specialty() {}
	//向き反転
	public void Reverse() {
		transform.Rotate(new Vector2(0f,180f));
		this.direction *= -1;
	}
	//ダメージ
	public virtual void Damage(IDamageGenerator damageGenerator){
		damage = damageGenerator.GetPower();
		force = damageGenerator.GetForce();
		forceSpeed = damageGenerator.GetForceSpeed();
		
		_life = Mathf.Max (_life - damage, 0);
		if (_life <= 0) Dead();
		
		GetComponent<Rigidbody2D>().AddForce(force * 5.0f, ForceMode2D.Impulse);
		Debug.Log(_life);
	}

	/// <summary>
	/// 回復
	/// </summary>
	/// <param name="power">回復量</param>
	public virtual void Heal(float power) {
		_life = Mathf.Min (_life + power, _maxLife);
	}

	//死亡関数
	public void Dead() {
		Destroy(gameObject);
	}

	/// <summary>
	/// ステータスの上昇
	/// </summary>
	public virtual void StatusUp(string target, float power) {
		Debug.Log ("bbbb");
		if (target == "atk")
			_atk += power;
		else if (target == "life")
			Heal (power);
	}
}
