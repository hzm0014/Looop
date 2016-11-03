using UnityEngine;
using System.Collections;

public class EnemySpawner : Spawner {
	
	Vector2 vec;
	Vector2 startPos;
	Vector2 size;
	
	public void Awake(float x,float y, float width, float height, float intervalS, float intervalE) {
		SetRange(x,y,width,height);
		SetIntervalScale(intervalS, intervalE);
	}
	
	// Use this for initialization
	void Start () {
		StartCoroutine("Spawn");
		SetIsSpawn(true);
	}
	
	IEnumerator Spawn() {
		while(true) {
			while(isSpawn) {
				SetPosition(Random.Range(startPos.x, startPos.x+size.x), Random.Range(startPos.y, startPos.y+size.y));
				GameObject obj;
				obj = (GameObject)Instantiate(spawnObject, vec, Quaternion.identity);
				obj.transform.SetParent (transform);
				SetInterval( DecideInterval() );
				//interval分次の処理を待つ
				yield return new WaitForSeconds( interval );
			}
		}
	}
	
	void SetInterval(float i) {
		//出現頻度の調整
		this.interval = i;
	}
	void SetPosition(float x,float y) {
		vec = new Vector2(x, y);
	}
	void SetRange(float x, float y, float width, float height) {
		startPos = new Vector2(x,y);
		size = new Vector2(width,height);
	}
	void SetIsSpawn(bool b) {
		this.isSpawn = b;
	}
	void Vanish() {
		Destroy(this);
	}
}
