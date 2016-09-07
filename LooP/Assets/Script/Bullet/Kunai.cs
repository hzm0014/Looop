using UnityEngine;
using System.Collections;

public class Kunai : MonoBehaviour {
	
	float speed;
	float direction = 1;
	Vector2 dropPos;
	private Rigidbody rb;
	public GameObject prefab;
	
	// Use this for initialization
	void Awake () {
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
			Debug.Log("Destroy");
		}
		else if(other.tag == "Butachan") {
			speed = 0;
			Destroy (other);
			Destroy (gameObject);
			Debug.Log(other.name);
		}
	}
	
}
