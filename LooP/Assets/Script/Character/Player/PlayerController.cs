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
	public bool isGround { set; get;}		// 地に立つ
	private Vector2 groundA;
	private Vector2 groundB;

	// ボタン管理系（連射の制限など）
	bool isJumoButtom, isAtkButtom;

	// 速度制限
	private float maxSpeed = 30.0f;
	private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
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
		if ((Input.GetAxis ("Jump") >= 1 || Input.GetAxis ("Vertical") >= 0.8f)&& isJumoButtom) {
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
		else
			GetComponent<Player> ().decDash = 0;
		// 速度制限
		if (myRigidbody.velocity.magnitude > maxSpeed)
			myRigidbody.velocity = Vector3.ClampMagnitude (myRigidbody.velocity, maxSpeed);

		// 攻撃
		if(Input.GetAxis ("Atack") >= 1 && isAtkButtom) {
			isAtkButtom = false;
			player.Specialty ();
		} else if (!(Input.GetAxis ("Atack") >= 1)){
			isAtkButtom = true;
		}
	}
}