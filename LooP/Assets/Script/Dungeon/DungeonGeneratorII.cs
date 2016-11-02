using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ダンジョンを生成する
/// </summary>
public class DungeonGeneratorII : MonoBehaviour {

	/// <summary>
	/// 部屋の最小サイズ
	/// </summary>
	const int MIN_ROOM = 3;
	/// <summary>
	/// 部屋の最大サイズ
	/// </summary>
	const int MAX_ROOM = 8;

	const int NONE = 0, WALL = 1;

	int width, height;
	const int wallSize = 30;

	public GameObject block, goalObj;

	// Use this for initialization
	void Start () {
		GenerateDungeon ();
	}

	/// <summary>
	/// ダンジョン生成
	/// </summary>
	public void GenerateDungeon() {
		width = (int)Random.Range (10, 30);
		height = (int)Random.Range (5, 15);
		// 層を生成
		List<int> layers = CreateLayer (height);

		// フロアを生成
		Layer2D floor = new Layer2D (width, layers[layers.Count - 1]);
		//floor.Fill (WALL);

		int start = Random.Range (1, width);
		int goal = Random.Range (1, width);
		// タイルを配置
		Setblock (floor, start, goal);

		// 主人公を配置
		GameObject.Find ("Player").transform.position = new Vector2 (start, wallSize*2/3);
	}

	/// <summary>
	/// 層を生成する
	/// </summary>
	private List<int> CreateLayer(int height) {
		List<int> layers = new List<int> ();
		int layer = 0;
		layers.Add (layer);
		// 層を生成
		while (layer < height) {
			layer += Random.Range (MIN_ROOM, MAX_ROOM);
			layers.Add (layer);
		}
		return layers;
	}

	/// <summary>
	/// タイルを設置
	/// </summary>
	/// <param name="floor">Floor.</param>
	private void Setblock(Layer2D floor, int start, int goal) {
		GameObject obj;
		for (int j = 0; j < floor.Height; j++) {
			for (int i = 0; i < floor.Width; i++) {
				if (floor.Get (i, j) == WALL) {
					// 壁生成
					obj = (GameObject)Instantiate (block, new Vector2 (i, j), new Quaternion ());
					obj.transform.parent = gameObject.transform;
				}
			}
		}
		// 大壁の生成
		// 左
		obj = (GameObject)Instantiate (block, new Vector2 (-wallSize/2, height/2), new Quaternion ());
		obj.transform.localScale = new Vector2(wallSize, height+wallSize*2);
		obj.transform.SetParent (transform);
		// 右
		obj = (GameObject)Instantiate (block, new Vector2 (width+wallSize/2-1, height/2), new Quaternion ());
		obj.transform.localScale = new Vector2 (wallSize, height+wallSize * 2);
		obj.transform.SetParent (transform);
		// 下
		obj = (GameObject)Instantiate (block, new Vector2 (((float)goal/2)-2, -wallSize/2), new Quaternion ());
		obj.transform.localScale = new Vector2 (goal+2, wallSize);
		obj.transform.SetParent (transform);
		obj = (GameObject)Instantiate (block, new Vector2 (((float)(goal + width) / 2)+1, -wallSize / 2), new Quaternion ());
		obj.transform.localScale = new Vector2 ((width - goal)+2, wallSize);
		obj.transform.SetParent (transform);
		// 上
		obj = (GameObject)Instantiate (block, new Vector2 (((float)start/2)-2, height+wallSize/2-1), new Quaternion ());
		obj.transform.localScale = new Vector2 (start+2, wallSize);
		obj.transform.SetParent (transform);
		obj = (GameObject)Instantiate (block, new Vector2 (((float)(start+width) / 2)+1, height + wallSize / 2 - 1), new Quaternion ());
		obj.transform.localScale = new Vector2 ((width - start)+2, wallSize);
		obj.transform.SetParent (transform);

		// ゴールの設置
		goalObj.transform.localPosition = new Vector2 (goal, -wallSize*2/3);
	}
}
