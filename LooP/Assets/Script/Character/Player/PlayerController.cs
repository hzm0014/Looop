using UnityEngine;
using System.Collections;

/// <summary>
/// キャラの移動系スクリプト
/// </summary>
public class PlayerController : MonoBehaviour {
	// 地上と空中の移動速度, 
	public float _landSpeed = 0.3f, _skySpeed = 0.2f;
	// 瞬間移動する距離、瞬間移動してからの脚力
	public float _jumpA = 1.2f, _jumpB = 1500;
	
	
	//OverlapAreaで絞る為のレイヤーマスクの変数
	public LayerMask whatIsGround;
	bool grounded = false;
	
	// ボタン管理系（連射の制限など）
	bool isJumoButtom, isAtkButtom;
	
	// Use this for initialization
	void Start () {
	}

	void FixedUpdate () {
		//プレイヤー位置
		Vector2 pos = transform.position;
		//あたり判定四角領域の中心点
		Vector2 groundCheck = new Vector2 (pos.x, pos.y - (GetComponent<CircleCollider2D> ().radius) * 2.0f);
		//あたり判定四角領域の範囲の幅
		Vector2 groundArea = new Vector2 (GetComponent<CircleCollider2D> ().radius * 0.49f, 0.05f);

		//あたり判定四角領域の範囲
		grounded = Physics2D.OverlapArea (groundCheck + groundArea, groundCheck - groundArea, whatIsGround);

		// 左右移動
		Player player = GetComponent<Player> ();
		player.Move (Input.GetAxis ("Horizontal"), grounded);


		// ジャンプ
		if (Input.GetAxis ("Jump") >= 1 && isJumoButtom) {
			isJumoButtom = false;
			player.Jump (grounded);
			}
		if (!(Input.GetAxis ("Jump") >= 1) && grounded) {
			isJumoButtom = true;
			}
		// 落下
		if (!grounded) {
			isJumoButtom = false;
		}

		// 攻撃
		if(Input.GetAxis ("Atack") >= 1 && isAtkButtom) {
			player.Specialty ();
			isAtkButtom = false;
		} else if (!(Input.GetAxis ("Atack") >= 1)){
			isAtkButtom = true;
		}
	}
}