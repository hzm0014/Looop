using UnityEngine;
using System.Collections;

/// <summary>
/// キャラの移動系スクリプト
/// </summary>
public class PlayerController : MonoBehaviour {
	// 地上と空中の移動速度, 
	public float _landSpeed = 0.3f, _skySpeed = 0.2f;

	// 床、壁判定のためのあれこれ
	public LayerMask whatIsGround;	// 対処になるレイヤ（Land）
	private bool isGround;		// 地に立つ
	private Vector2 groundA;
	private Vector2 groundB;

	private float maxSpeed = 30.0f;
	private Rigidbody2D myRigidbody;


	// ボタン管理系（連射の制限など）
	bool isJumoButtom, isAtkButtom;
	
	// Use this for initialization
	void Start () {
		isWall = 0;
		float r = GetComponent<CircleCollider2D> ().radius;
		groundA = new Vector2 (r/2, 0);
		groundB = new Vector2 (-r/2, -r*2);
		myRigidbody = this.GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		//プレイヤー位置
		Vector2 pos = transform.position;

		// 下方向に床があるか
		isGround = Physics2D.OverlapArea (pos + groundA, pos + groundB, whatIsGround);

		// 左右移動
		Player player = GetComponent<Player> ();
		player.Move (Input.GetAxis ("Horizontal"), isGround);


		// ジャンプ
		if (Input.GetAxis ("Jump") >= 1 && isJumoButtom) {
			isJumoButtom = false;
			player.Jump (isGround);
			}
		if (!(Input.GetAxis ("Jump") >= 1) && isGround) {
			isJumoButtom = true;
			}
		// 落下
		if (!isGround) {
			isJumoButtom = false;
		}
		Debug.Log (myRigidbody.velocity.magnitude + ", " + maxSpeed);
		if (myRigidbody.velocity.magnitude > maxSpeed) {
			Debug.Log ("safety");
			myRigidbody.velocity = Vector3.ClampMagnitude (myRigidbody.velocity, maxSpeed);
		}

		// 攻撃
		if(Input.GetAxis ("Atack") >= 1 && isAtkButtom) {
			isAtkButtom = false;
			player.Specialty ();
		} else if (!(Input.GetAxis ("Atack") >= 1)){
			isAtkButtom = true;
		}
	}
}