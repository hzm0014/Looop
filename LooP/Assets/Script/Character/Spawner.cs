using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	//発生するObjectを指定する用
	public GameObject spawnObject;
	//発生感覚
	public float interval;
	//スポーンするかどうか
	public bool isSpawn = true;
	
	// Use this for initialization
	void Start () {
		SetInterval(3.0f);
		//コルーチンの開始
		StartCoroutine("Spawn");
	}
	
	IEnumerator Spawn() {
		while(true) {
			Instantiate(spawnObject, transform.position, Quaternion.identity);
			//interval分次の処理を待つ
			yield return new WaitForSeconds(interval);
		}
	}
	
	void SetInterval(float interval) {
		this.interval = interval;
	}
}
