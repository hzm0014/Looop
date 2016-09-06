using UnityEngine;
using System.Collections;

/// <summary>
/// キャラの移動系スクリプト
/// </summary>
public class Move : MonoBehaviour {
	/// <summary>
	/// スピードの構造体
	/// </summary>
	private struct Speed {
		public float x, y;
		public Speed(float x, float y) {
			this.x = x;
			this.y = y;
		}
	}
	private Speed speed = new Speed(0.3f, 0.3f);

	// Use this for initialization
	void Start () {
	}

	void Update () {
		// 左右移動
		Vector2 Position = transform.position;
		/*
		if (Input.GetKey ("left")) {
			Position.x -= speed.x;
		} 
		else if (Input.GetKey ("right")) {
			Position.x += speed.x;
		}*/
		Position.x += Input.GetAxis("Horizontal") * speed.x;
		transform.position = Position;
	}
}