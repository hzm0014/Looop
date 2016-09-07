using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour {
	public ArrayList _stage = new ArrayList ();
	//public GameObject[] _stage = new GameObject[5];
	public GameObject _player;

	// エリア数
	int _areaNum;
	// エリアサイズ
	int _areaSize;


	// プレイヤーのいるエリア, プレイヤーのいるエリアの座標
	private int _playerArea, _playerWorld;

	// Use this for initialization
	void Awake () {
		// ステージを生成
		_areaNum = Random.Range(5, 8);
		_areaSize = 20;
		Object[] prefabs = Resources.LoadAll ("Prefavs/Stage/Set1");
		for (int i = 0; i < _areaNum; i++) {
			Object area = prefabs[Random.Range (0, prefabs.Length)];
			Quaternion rotaition = new Quaternion (0.0f, Random.Range(0,2) * 180.0f, 0.0f, 0.0f);
			_stage.Add (Instantiate (area, new Vector2 (i * _areaSize, 0), rotaition));
		}


	// プレイヤーの位置を決める
		_playerArea = _playerWorld = _areaNum/2;
		Vector2 v =_player.transform.position;
		v.x = _playerArea * _areaSize;
		_player.transform.position = v;
	}
	
	// Update is called once per frame
	void Update () {
		float playerPoint = _player.transform.position.x;


		// プレイヤーが右エリアへ移動
		if(playerPoint - _playerWorld * _areaSize > _areaSize/2) {
			// プレイヤー情報の更新
			_playerArea = (_playerArea + 1) % _areaNum;
			_playerWorld++;
			// エリアを移動
			GameObject g = (GameObject)_stage[(_playerArea + 2) % _areaNum];
			Vector2 v = g.transform.position;
			v.x = (_playerWorld + 2)*_areaSize;
			g.transform.position = v;
			_stage[(_playerArea + 2) % _areaNum] = g;
			}
		// プレイヤーが左エリアへ移動
		else if(playerPoint - _playerWorld * _areaSize < -_areaSize/2) {
			// プレイヤー情報の更新
			_playerArea = ((_playerArea - 1) + _areaNum) % _areaNum;
			_playerWorld--;
			// エリアを移動
			GameObject g = (GameObject)_stage[((_playerArea - 2) + _areaNum) % _areaNum];
			Vector2 v = g.transform.position;
			v.x = (_playerWorld - 2) * _areaSize;
			g.transform.position = v;
			_stage[((_playerArea - 2) + _areaNum) % _areaNum] = g;
		}
	}
}
