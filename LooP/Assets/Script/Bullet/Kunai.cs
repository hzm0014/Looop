using UnityEngine;
using System.Collections;

public class Kunai : MonoBehaviour {
	
	float speed;
	float direction;
	Vector2 dropPos;
	private Rigidbody rb;
	
	// Use this for initialization
	void Awake () {
		speed = 0.7f;
		direction = 1;
		dropPos = transform.position = GameObject.Find ("Player").transform.position;
		rb = this.GetComponent<Rigidbody>();
		rb.useGravity = false;
	}
	
	// いろいろセッティング
	public void SetBullet(float direction, Vector2 pos) {
		this.direction = direction;
		dropPos = transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 v = transform.position;
		v.x += speed * direction;
		transform.position = v;
		if ((dropPos.x - v.x) * (dropPos.x - v.x) + (dropPos.y - v.y) * (dropPos.y - v.y) > 200)
		Destroy (gameObject);
	}
	
	void OnTriggerEnter(Collider other){
		Debug.Log("Destroy");
		speed = 0;
	}
	
}
