using UnityEngine;
using System.Collections;

public class EnemySpawner : Singleton<EnemySpawner> {
	protected EnemySpawner() {}

	/// <summary>
	/// 部屋の情報
	/// </summary>
	public Layer2D _floor = null;

	/// <summary>
	/// 最大試行回数
	/// この回数失敗すると敵は現れない
	/// </summary>
	const int MAX = 12;

	/// <summary>
	/// 生成する敵（現在１種のため直接放り込む）
	/// </summary>
	public GameObject _spawnObj;

	/// <summary>
	/// スポーンするかのフラグ
	/// </summary>
	public bool isSpawn;

	int _floorNum;


	float butaPow = 100.0f;
	float delatabutapow = 10.0f;
	float bairitu = 1.1f;

	/// <summary>
	/// 敵の生成を開始する
	/// </summary>
	/// <param name="floor">フロアの構造情報</param>
	private void _StartSpawn (Layer2D floor, int floorNum) {
		_floor = floor;
		_floorNum = floorNum;
		isSpawn = true;
		int initNum = Mathf.Min (4 + (int)(floorNum*0.1f), 10);
		for (int i = 0; i < initNum; i++)
			Spawn ();
		float interval = Mathf.Max (6 - floorNum * 0.1f, 2);
		StartCoroutine ("SpawnCycle", interval);
		butaPow = (butaPow * bairitu) + delatabutapow;
	}
	public static void StartSpawn(Layer2D floor, int floorNum) {
		Instance._StartSpawn (floor, floorNum);
	}

	/// <summary>
	/// 敵生成のサイクル
	/// </summary>
	/// <returns>The cycle.</returns>
	private IEnumerator SpawnCycle (float interval) {
		while (isSpawn) {
			yield return new WaitForSeconds (interval);
			Spawn ();
		}
	}

	/// <summary>
	/// 敵モンスターを生成
	/// </summary>
	private void Spawn() {
		for (int i = 0; i < MAX; i++) {
			int x = Random.Range (1, _floor.Width);
			int y = Random.Range (1, _floor.Height);
			if (_floor.Get (x, y) == 0) {
				GameObject obj = (GameObject)Instantiate (_spawnObj, new Vector2 (x, y), Quaternion.identity);
				obj.transform.SetParent (GameObject.Find ("Dungeon").transform);
				obj.GetComponent<Butachan> ()._maxLife = butaPow;
				break;
			}
		}
	}
}
