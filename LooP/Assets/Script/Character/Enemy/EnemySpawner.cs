using UnityEngine;
using System.Collections;

public class EnemySpawner : Spawner {
	
	Vector2 vec;
	Vector2 startPos;
	Vector2 size;
	
	// Use this for initialization
	void Start () {
		StartCoroutine("Spawn");
		SetIsSpawn(true);
	}
	
	IEnumerator Spawn() {
		while(true) {
			while(isSpawn) {
				SetPosition(Random.Range(startPos.x, startPos.y), Random.Range(startPos.x+size.x, startPos.y+size.y));
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
	void SetRange(float x, float y, float z, float w) {
		startPos = new Vector2(x,y);
		size = new Vector2(z,w);
	}
	void SetIsSpawn(bool b) {
		this.isSpawn = b;
	}
}
