using UnityEngine;
using System.Collections;
using UnityEngine.UI;  ////ここを追加////

public class DamageText : MonoBehaviour {
	
	public float damage = 0;
	public Text text;
	
	// Use this for initialization
	void Start () {
		Destroy(gameObject, 1);
		text = gameObject.GetComponent<Text>();
	}
	float time = 0;
	void Update () {
		transform.Translate(Vector3.up * Time.deltaTime * 200);
		time += Time.deltaTime;
		//透明にする
		text.color = new Color(text.color.r, text.color.g, text.color.b, 1.5F - time);
	}
	public void SetDamage(float damage) {
		this.damage = damage;
	}
	public void ViewDamage(Vector3 pos, float damage) {
		this.GetComponent<Text>().text = "" + damage;
		Debug.Log(damage);
	}
}
