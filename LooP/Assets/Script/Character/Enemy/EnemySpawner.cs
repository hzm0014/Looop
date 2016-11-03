using UnityEngine;
using System.Collections;

public class EnemySpawner : Spawner {
	
	Vector2 vec;
	
	// Use this for initialization
	void Start () {
		StartCoroutine("Spawn");
		SetIsSpawn(true);
	}
	
	IEnumerator Spawn() {
		while(true) {
			while(isSpawn) {
				SetPosition(Random.Range(-5.0f, 5.0f), 5.0f);
				Instantiate(spawnObject, vec, Quaternion.identity);
				SetInterval();
				//interval分次の処理を待つ
				yield return new WaitForSeconds(interval);
			}
		}
	}
	
	void SetInterval() {
		//出現頻度の調整
		this.interval = Random.Range(0.0f, 10.0f);
	}
	void SetPosition(float x,float y) {
		vec = new Vector2(x, y);
	}
	void SetIsSpawn(bool b) {
		this.isSpawn = b;
	}
}
