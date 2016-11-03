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
	/// 敵の生成を開始する
	/// </summary>
	/// <param name="floor">フロアの構造情報</param>
	/// <param name="initNum">初期の敵数</param>
	/// <param name="interval">敵出現周期(秒）</param>
	private void _StartSpawn (Layer2D floor, int initNum, float interval) {
		_floor = floor;
		for (int i = 0; i < initNum; i++)
			Spawn ();
		StartCoroutine ("SpawnCycle", interval);
	}
	public static void StartSpawn(Layer2D floor, int initNum, float interval) {
		Instance._StartSpawn (floor, initNum, interval);
	}

	/// <summary>
	/// 敵生成のサイクル
	/// </summary>
	/// <returns>The cycle.</returns>
	private IEnumerator SpawnCycle (float interval) {
		while (true) {
			Spawn ();
			yield return new WaitForSeconds (interval);
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
				break;
			}
		}
	}
}
