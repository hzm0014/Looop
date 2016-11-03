using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	//発生するObjectを指定する用
	public GameObject spawnObject;
	//発生感覚
	public float interval;
	public Vector2 intervalScale;
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
	
	// [S1] popの間隔の設定
	void SetInterval(float interval) {
		this.interval = interval;
	}
	// [S2] SpawnObjectの設定
	void SetSpawnObject(GameObject go) {
		spawnObject = go;
	}
	// [S3] popするかの設定
	void SetIsAvailable(bool IA) {
		isSpawn = IA;
	}
	// [S4] IntervalScaleの設定
	public void SetIntervalScale(float start, float end) {
		intervalScale = new Vector2(start, end);
	}
	// [Initialize] 初期化関数 or まとめて変更関数
	void init(float interval, GameObject go, bool IA) {
		SetInterval (interval);
		SetSpawnObject(go);
		SetIsAvailable(IA);
	}
	
	// [G1] pop間隔を返す
	float GetInterval() { return interval; }
	// [G2] spawnする物を返す
	GameObject GetSpawnObject() { return spawnObject; }
	// [G3] popするかを返す
	bool IsAvailable() { return isSpawn; }
	
	public float DecideInterval() {
		return (float)Random.Range(intervalScale.x, intervalScale.y);
	}
}
