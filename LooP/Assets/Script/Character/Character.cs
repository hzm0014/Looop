using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour , IDamageGenerator {
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
	protected float life; //命
	protected float power; //攻撃力
	protected float defense; //守備力
	protected float direction; //方向
	protected float damage; //今受けたダメージ
	protected Vector2 force; //吹き飛ばされる
	protected float forceSpeed; //吹き飛ばされるスピード
	
	
	//関数宣言部
	// 初期化
	void Start () {
	}
	
	// 繰り返しを記入
	void Update () {
	}
	
	//set関数群
	public void SetSpeed(float land,float sky) {
		speed.land = land;
		speed.sky = sky;
	}
	public void SetSpeedLand(float land) {
		speed.land = land;
	}
	public void SetSpeedSky(float sky) {
		speed.sky = sky;
	}
	public void SetLife(float life) {
		this.life = life;
	}
	public void SetPower(float power) {
		this.power = power;
	}
	public void SetDefense(float defense) {
		this.defense = defense;
	}
	public void SetDirection(float direction) {
		this.direction = direction;
	}
	public void SetForce(Vector2 force) {
		this.force = force;
	}
	public void SetForceSpeed(float forceSpeed) {
		this.forceSpeed = forceSpeed;
	}
	
	//get関数群
	public float GetSpeedLand() {
		return speed.land;
	}
	public float GetSpeedSky() {
		return speed.sky;
	}
	public float GetLife() {
		return life;
	}
	public float GetPower() {
		return power;
	}
	public float GetDefense() {
		return defense;
	}
	public float GetDirection() {
		return direction;
	}
	public Vector2 GetForce() {
		return force;
	}
	public float GetForceSpeed() {
		return forceSpeed;
	}
	
	//行動関係群
	// 移動
	public virtual void Move() {
	}
	//ジャンプ
	public virtual void Jump() {
	}
	// アクション
	public virtual void Action() {
	}
	// 通常攻撃
	public virtual void Attack() {
	}
	// 特技
	public virtual void Specialty() {
	}
	//向き反転
	public void Reverse() {
		this.direction *= -1;
	}
	//ダメージ
	public void Damage(IDamageGenerator damageGenerator){
		damage = damageGenerator.GetPower();
		force = damageGenerator.GetForce();
		forceSpeed = damageGenerator.GetForceSpeed();
		
		life -= damage;
		if(life < 0) life = 0;
		if(life == 0) Dead();
		
		GetComponent<Rigidbody2D>().AddForce(force * 5.0f, ForceMode2D.Impulse);
		Debug.Log(life);
	}
	public void Dead() {
		Destroy(gameObject);
	}
}
