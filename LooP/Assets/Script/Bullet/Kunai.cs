using UnityEngine;
using System.Collections;
//相手にダメージを与えるクラスにはIDamageGeneratorを継承させる
public class Kunai : MonoBehaviour ,IDamageGenerator{

	// 移動スピードと移動先
	private float speed;
	private Vector2 vSpeed;

	private Rigidbody rb;
	public GameObject prefab;
	public float power;
	public Vector2 force;
	public float forceSpeed;
	
	// Use this for initialization
	void Awake () {
		this.power = 5.0f;
		speed = 0.5f;
	}
	
	// いろいろセッティング
	public void SetBullet(Vector2 pos, Vector3 v) {
		transform.position = pos;
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, v.z);
		force = new Vector2(Mathf.Cos (Mathf.Deg2Rad * v.z), Mathf.Sin (Mathf.Deg2Rad * v.z));
		forceSpeed = 1.0f;
		
		vSpeed.x = Mathf.Cos (Mathf.Deg2Rad * v.z) * speed;
		vSpeed.y = Mathf.Sin (Mathf.Deg2Rad * v.z) * speed;
		// 5秒後にデストロイ
		Destroy (gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 v = transform.position;
		v.x += vSpeed.x;
		v.y += vSpeed.y;
		transform.position = v;
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			//Debug.Log("Destroy");
		}
		else if(other.tag == "Land") {
			this.vSpeed.x = 0;
			this.vSpeed.y = 0;
			gameObject.layer = LayerMask.NameToLayer("Invisible");
			//Debug.Log("Destroy");
			Destroy(gameObject, 0.2f);
		}
		else if(other.tag == "Enemy") {
			this.vSpeed.x = 0;
			this.vSpeed.y = 0;
			gameObject.layer = LayerMask.NameToLayer("Invisible");
			Destroy (gameObject);
			
			GameObject obj = other.transform.gameObject;
			Enemy e = obj.GetComponent<Enemy>();
			e.Damage(this);
		}
	}
	
	//IDamageGeneratorを継承したら必ず書く部分
	public float GetPower() {
		return this.power;
	}
	public Vector2 GetForce() {
		return this.force;
	}
	public float GetForceSpeed() {
		return this.forceSpeed;
	}
	//
}
