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
	private Speed speed = new Speed(0.3f, 0.4f);

	//OverlapAreaで絞る為のレイヤーマスクの変数
	public LayerMask whatIsGround;
	bool grounded = false;

	// ジャンプ系変数
	bool isJump, isJumoButtom;
	int jumpTime = 0, jumpMax = 15;

	// Use this for initialization
	void Start () {
		jumpTime = 0;
	}

	void FixedUpdate () {
		//プレイヤー位置
		Vector2 pos = transform.position;
		//あたり判定四角領域の中心点
		Vector2 groundCheck = new Vector2 (pos.x, pos.y - (GetComponent<CircleCollider2D> ().radius) * 1.5f);
		//あたり判定四角領域の範囲の幅
		Vector2 groundArea = new Vector2 (GetComponent<CircleCollider2D> ().radius * 0.49f, 0.05f);

		//あたり判定四角領域の範囲
		grounded = Physics2D.OverlapArea (groundCheck + groundArea, groundCheck - groundArea, whatIsGround);

		// 左右移動
		Vector2 Position = transform.position;
		Position.x += Input.GetAxis("Horizontal") * speed.x;

		if (Input.GetAxis ("Jump") == 1 && grounded && isJumoButtom) {
			isJump = true;
			isJumoButtom = false;
		} else if (!(Input.GetAxis ("Jump") == 1)) {
				if (grounded)
					isJumoButtom = true;
		}
		/////
		if(isJump) {
			Position.y += speed.y;
			jumpTime++;
			if (jumpTime == jumpMax) {
				jumpTime = 0;
				isJump = false;
			}
		}
		transform.position = Position;
		Debug.Log (jumpTime + ", " + isJump);



		}
}