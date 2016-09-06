using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	//構造体宣言部
	private struct Speed {
		public float x, y;
		public Speed(float x, float y) {
			this.x = x;
			this.y = y;
		}
	}
	
	//変数宣言部
	private Speed speed;
	private float life;
	private float power;
	private float defense;
	
	//関数宣言部
	// 初期化
	void Start () {
	}
	
	// 繰り返しを記入
	void Update () {
	}
	
	// 移動
	private void Move() {
	}
	//ジャンプ
	private void Jump() {
	}
	// アクション
	private void Action() {
	}
	// 通常攻撃
	private void Attack() {
	}
	// 特技
	private void Specialty() {
	}
}
