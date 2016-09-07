using UnityEngine;
using System.Collections;
//相手にダメージを与えるクラスにはIDamageGeneratorを継承させる
public class Kunai : MonoBehaviour ,IDamageGenerator{
	
	float speed;
	float direction = 1;
	Vector2 dropPos;
	private Rigidbody rb;
	public GameObject prefab;
	public float power;
	public Vector2 force;
	public float forceSpeed;
	
	// Use this for initialization
	void Awake () {
		this.power = 5.0f;
		speed = 0.5f;
		//direction = transform.rotate.x / Abs(transform.rotate.x);
		dropPos = transform.position = GameObject.Find ("Player").transform.position;
		//rb = this.GetComponent<Rigidbody>();
		//rb.useGravity = false;
	}
	
	// いろいろセッティング
	public void SetBullet(float direction, Vector2 pos) {
		this.direction = direction;
		pos.x += direction * 1.01f;
		dropPos = transform.position = pos;
		
		GameObject obj = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);
		// 作成したオブジェクトを子として登録
		obj.transform.parent = transform;
		
		// 5秒後にデストロイ
		Destroy (gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 v = transform.position;
		v.x += speed * direction;
		transform.position = v;
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			//Debug.Log("Destroy");
		}
		else if(other.tag == "Land") {
			Destroy(gameObject);
		}
		else if(other.tag == "Enemy") {
			this.speed = 0;
			
			GameObject obj = other.transform.gameObject;
			Enemy e = obj.GetComponent<Enemy>();
			e.Damage(this);
			
			Destroy (gameObject);
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
