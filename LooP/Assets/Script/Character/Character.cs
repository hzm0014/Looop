using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
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
	void SetLife(float life) {
		this.life = life;
	}
	void SetPower(float power) {
		this.power = power;
	}
	void SetDefense(float defense) {
		this.defense = defense;
	}
	void SetDirection(float direction) {
		this.direction = direction;
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
	
	//行動関係群
	// 移動
	private void Move() {
	}
	//ジャンプ
	protected void Jump() {
	}
	// アクション
	protected void Action() {
	}
	// 通常攻撃
	protected void Attack() {
	}
	// 特技
	protected void Specialty() {
	}
	//向き反転
	void Reverse() {
		this.direction *= -1;
	}
}
