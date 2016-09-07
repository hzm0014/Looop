using UnityEngine;
using System.Collections;

/// <summary>
/// キャラの移動系スクリプト
/// </summary>
public class Move : MonoBehaviour {
	// 地上と空中の移動速度, 
	public float _landSpeed = 0.3f, _skySpeed = 0.2f;
	// 瞬間移動する距離、瞬間移動してからの脚力
	public float _jumpA = 1.2f, _jumpB = 1500;


	//OverlapAreaで絞る為のレイヤーマスクの変数
	public LayerMask whatIsGround;
	bool grounded = false;

	// ジャンプ系変数
	bool isJump, isJumoButtom;

	// Use this for initialization
	void Start () {
	}

	void FixedUpdate () {
		//プレイヤー位置
		Vector2 pos = transform.position;
		//あたり判定四角領域の中心点
		Vector2 groundCheck = new Vector2 (pos.x, pos.y - (GetComponent<CircleCollider2D> ().radius) * 2.0f);
		//あたり判定四角領域の範囲の幅
		Vector2 groundArea = new Vector2 (GetComponent<CircleCollider2D> ().radius * 0.49f, 0.5f);

		//あたり判定四角領域の範囲
		grounded = Physics2D.OverlapArea (groundCheck + groundArea, groundCheck - groundArea, whatIsGround);

		// 左右移動
		Vector2 Position = transform.position;
		if(grounded)
			Position.x += Input.GetAxis("Horizontal") * _landSpeed;
		else
			Position.x += Input.GetAxis ("Horizontal") * _skySpeed;


		// ジャンプ
		Debug.Log (grounded);
		if (Input.GetAxis ("Jump") >= 1 && grounded && isJumoButtom) {
			isJump = true;
			isJumoButtom = false;
			Position.y += _jumpA;
		} else if (!(Input.GetAxis ("Jump") >= 1) && grounded) {
			isJumoButtom = true;
		}
		if(isJump) {
			this.GetComponent<Rigidbody2D>().AddForce (Vector2.up * _jumpB);
			isJump = false;
		}

		// 落下
		if (!isJump && !grounded)
			isJumoButtom = false;
		transform.position = Position;
	}
}