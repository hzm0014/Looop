using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// ダンジョンを生成する
/// </summary>
public class DungeonGeneratorII : Singleton<DungeonGeneratorII> {
	protected DungeonGeneratorII () { }

	/// <summary>
	/// ダンジョン情報
	/// </summary>
	Layer2D _floor = null;
	/// <summary>
	/// 区画リスト
	/// </summary>
	List<DgDivision> _divList = null;

	/// <summary>
	/// 区画と部屋の余白サイズ
	/// </summary>
	const int OUTER_MERGIN = 1;
	/// <summary>
	/// 部屋配置の余白サイズ
	/// </summary>
	const int POS_MERGIN = 1;
	/// <summary>
	/// 最小の部屋サイズ
	/// </summary>
	const int MIN_ROOM = 3;
	/// <summary>
	/// 最大の部屋サイズ
	/// </summary>
	const int MAX_ROOM = 8;

	const int NONE = 0, WALL = 1;

	int width, height;
	const int wallSize = 30;

	/// <summary>
	/// ボス戦の周期
	/// </summary>
	const int EntryCycle = 3;
	const int BattleCycle = 10;
	public GameObject Boss;

	/// <summary>
	/// ダンジョンを生成するオブジェクト
	/// ブロック，ゴール．敵のスポーン
	/// </summary>
	public GameObject block, goalObj;

	public int _floorNum;
	public GameObject floorText;

	// Use this for initialization
	void Start () {
		_floorNum = 0;
		GenerateDungeon ();
		}

	/// <summary>
	/// ダンジョン生成
	/// </summary>
	public void GenerateDungeon () {
		_floorNum++;
		floorText.GetComponent<Text> ().text = "B" + _floorNum;

		if (_floorNum % BattleCycle == EntryCycle)
			Instantiate (Boss, new Vector2 (0, 0), new Quaternion ());

		if(_floorNum % BattleCycle == 0) {
			GenerateBossFloor ();
			return;
		}

		width = (int)Random.Range (20, 50);
		height = (int)Random.Range (10, 25);

		_floor = new Layer2D (width, height);
		_divList = new List<DgDivision> ();

		// すべてを壁に
		_floor.Fill (WALL);

		// 一つ目の区画
		CreateDivision (0, 0, width - 1, height - 1);
		_divList[0].start = _divList[0].goal = true;

		// 区画を分割
		SplitDivision (false);

		// 部屋を作る
		CreateRoom ();

		// 部屋を繋ぐ
		ConnectRooms ();
		// スタートとゴールを決める
		DgDivision startRoom = _divList[Random.Range (0, _divList.Count - 1)];
		int start = Random.Range (startRoom.Room.Left + 1, startRoom.Room.Right);
		DgDivision goalRoom = _divList[Random.Range (0, _divList.Count - 1)];
		int goal = Random.Range (goalRoom.Room.Left + 1, goalRoom.Room.Right);


		// スタートとゴールの穴を開ける
		BitFloor (start, goal);


		// タイルを配置
		Setblock (_floor, start, goal);
		SetBigWall (start, goal);

		// 主人公を配置
		GameObject.Find ("Player").transform.position = new Vector2 (start, wallSize * 2 / 3);
		// spawnerの設定
		EnemySpawner.StartSpawn (_floor, _floorNum);
		// アイテムを設置
		ItemGenerator.RandomDeploy (_floor, 5);
	}

	/// <summary>
	/// ボスフロアを生成
	/// </summary>
	public GameObject BossStage;
	private GameObject stage;
	public void GenerateBossFloor () {
		Debug.Log ("Bossstage");
		width = 15;
		height = 15;
		_floor = new Layer2D (width, height);
		_floor.Fill (NONE);
		_divList = new List<DgDivision> ();
		int start = 5;
		int goal = 5;
		SetBigWall (start, goal);
		// ボスを戦闘モードに
		GameObject.Find ("Boss(Clone)").GetComponent<Boss>().ShiftButtleMode(500+(int)(_floorNum * 100), 300+ (int)(_floorNum * 100), _floorNum);
		// ステージ生成
		stage = (GameObject)Instantiate (BossStage, new Vector2((float)width/2, (float)height/2), new Quaternion ());
		stage.transform.parent = gameObject.transform;
		// 主人公を配置
		GameObject.Find ("Player").transform.position = new Vector2 (start, wallSize * 2 / 3);
	}

	/// <summary>
	/// ボスフロアクリア
	/// </summary>
	public void ClearBossFloor() {
		Destroy (stage);
	}

	/// <summary>
	/// 区画の生成
	/// </summary>
	private void CreateDivision (int left, int top, int right, int bottom) {
		DgDivision div = new DgDivision ();
		div.Outer.Set (left, top, right, bottom);
		_divList.Add (div);
	}


	private void SplitDivision (bool bVertical) {
		// 末尾(親区画)の取り出し
		DgDivision parent = _divList[_divList.Count - 1];
		_divList.Remove (parent);
		// 子区画の生成
		DgDivision child = new DgDivision ();
		// 縦方向に分割
		if (bVertical) {
			// サイズが足りないなら戻して終わり
			if (CheckDivisionSize (parent.Outer.Height) == false) {
				_divList.Add (parent);
				return;
			}
			// 分割ポイントを求める
			int a = parent.Outer.Top + (MIN_ROOM + OUTER_MERGIN);
			int b = parent.Outer.Bottom - (MIN_ROOM + OUTER_MERGIN);
			//AB間の距離
			int ab = b - a;
			// 最大サイズを超えてたら抑える
			ab = Mathf.Min (ab, MAX_ROOM);
			// 分割点を決める
			int p = a + Random.Range (0, ab + 1);
			// 子区画を設定
			child.Outer.Set (parent.Outer.Left, p, parent.Outer.Right, parent.Outer.Bottom);
			// 親の下側を縮める
			parent.Outer.Bottom = child.Outer.Top;
		}
		// 横方向に分割
		else {
			// サイズが足りないなら戻して終わり
			if (CheckDivisionSize (parent.Outer.Width) == false) {
				_divList.Add (parent);
				return;
				}

			// 分割ポイントを求める
			int a = parent.Outer.Left + (MIN_ROOM + OUTER_MERGIN);
			int b = parent.Outer.Right - (MIN_ROOM + OUTER_MERGIN);
			// AB間の距離を求める
			int ab = b - a;
			// 最大サイズを超えてたら抑える
			ab = Mathf.Min (ab, MAX_ROOM);

			// 分割点を求める
			int p = a + Random.Range (0, ab + 1);

			// 子区画を設定
			child.Outer.Set (p, parent.Outer.Top, parent.Outer.Right, parent.Outer.Bottom);

			// 親の右側を縮める
			parent.Outer.Right = child.Outer.Left;
		}
		// 次に分割する区画を決める
		if (Random.Range (0, 2) == 0) {
			_divList.Add (parent);
			_divList.Add (child);
		} else {
			_divList.Add (child);
			_divList.Add (parent);
		}

		// 再帰呼び出し
		SplitDivision (!bVertical);
		}

	/// <summary>
	/// 指定のサイズを持つ区画を分割できるかどうか
	/// </summary>
	/// <param name="size">チェックする区画のサイズ</param>
	/// <returns>分割できればtrue</returns>
	private bool CheckDivisionSize (int size) {
		int min = (MIN_ROOM + OUTER_MERGIN) * 2 + 1;
		return size >= min;
		}

	/// <summary>
	/// 部屋を作る
	/// </summary>
	private void CreateRoom () {
		foreach (DgDivision div in _divList) {
			// マージンを埋める
			int dw = div.Outer.Width - OUTER_MERGIN;
			int dh = div.Outer.Height - OUTER_MERGIN;

			// 大きさを決める
			int sw = Random.Range (MIN_ROOM, dw);
			int sh = Random.Range (MIN_ROOM, dh);
			sw = dw;

			// 最大サイズを超えないように
			sw = Mathf.Min (sw, MAX_ROOM);
			sh = Mathf.Min (sh, MAX_ROOM);

			// 空きサイズを計算
			int rw = dw - sw;
			int rh = dh - sh;

			// 部屋の左上の位置
			int rx = Random.Range (0, rw) + POS_MERGIN;
			int ry = Random.Range (0, rh) + POS_MERGIN;


			int left = div.Outer.Left + rx;
			int right = left + sw;
			int top = div.Outer.Top + ry;
			int bottom = top + sh;

			// 部屋サイズを設定
			div.Room.Set (left, top, right, bottom);

			// 部屋を作る
			FillDgRect (div.Room);
			}
		}

	/// <summary>
	/// DgRectの範囲を塗りつぶす
	/// </summary>
	/// <param name="rect">矩形情報</param>
	private void FillDgRect (DgDivision.DgRect r) {
		_floor.FillRectLTRB (r.Left, r.Top, r.Right, r.Bottom, NONE);
		}

	/// <summary>
	/// 部屋を繋ぐ
	/// </summary>
	private void ConnectRooms () {
		for (int i = 0; i < _divList.Count - 1; i++) {
			DgDivision a = _divList[i];
			DgDivision b = _divList[i + 1];

			// 2つの部屋を繋ぐ
			CreateRoad (a, b);

			for (int j = i + 2; j < _divList.Count; j++) {
				DgDivision c = _divList[j];
				if (CreateRoad (a, c, true)) {
					break;
					}
				}
			}
		}

	/// <summary>
	/// 通路を生成
	/// </summary>
	/// <returns><c>true</c>, if road was created, <c>false</c> otherwise.</returns>
	/// <param name="divA">Div a.</param>
	/// <param name="divB">Div b.</param>
	/// <param name="bGrandChild">If set to <c>true</c> b grand child.</param>
	private bool CreateRoad (DgDivision divA, DgDivision divB, bool bGrandChild = false) {
		// 上下の接続
		if (divA.Outer.Bottom == divB.Outer.Top || divA.Outer.Top == divB.Outer.Bottom) {
			int x1 = Random.Range (divA.Room.Left, divA.Room.Right);
			int x2 = Random.Range (divB.Room.Left, divB.Room.Right);
			int y = 0;

			// すでに通路があるなら、それを使用
			if (bGrandChild) {
				if (divA.HasRoad ()) { x1 = divA.Road.Left; }
				if (divB.HasRoad ()) { x2 = divB.Road.Left; }
				}

			// Bが上側
			if (divA.Outer.Top > divB.Outer.Top) {
				y = divA.Outer.Top;
				// 通路を生成
				divA.CreateRoad (x1, y + 1, x1 + 1, divA.Room.Top);
				divB.CreateRoad (x2, divB.Room.Bottom, x2 + 1, y);
				}
			// Aが上側
			else {
				y = divB.Outer.Top;
				// 通路を生成
				divA.CreateRoad (x1, divA.Room.Bottom, x1 + 1, y);
				divB.CreateRoad (x2, y, x2 + 1, divB.Room.Top);
				}
			FillDgRect (divA.Road);
			FillDgRect (divB.Road);

			// 通路同士を接続する
			FillHLine (x1, x2, y);

			// 通路を作れた
			return true;
		}
		if (divA.Outer.Left == divB.Outer.Right || divA.Outer.Right == divB.Outer.Left) {
			// 左右でつながっている
			// 部屋から伸ばす通路の開始位置を決める
			int y1 = Random.Range (divA.Room.Top, divA.Room.Bottom);
			int y2 = Random.Range (divB.Room.Top, divB.Room.Bottom);
			int x = 0;

			if (bGrandChild) {
				// すでに通路が存在していたらその情報を使う
				if (divA.HasRoad ()) { y1 = divA.Road.Top; }
				if (divB.HasRoad ()) { y2 = divB.Road.Top; }
				}

			if (divA.Outer.Left > divB.Outer.Left) {
				// B - A (Bが左側)
				x = divA.Outer.Left;
				// 通路を作成
				divB.CreateRoad (divB.Room.Right, y2, x, y2 + 1);
				divA.CreateRoad (x + 1, y1, divA.Room.Left, y1 + 1);
				} else {
				// A - B (Aが左側)
				x = divB.Outer.Left;
				divA.CreateRoad (divA.Room.Right, y1, x, y1 + 1);
				divB.CreateRoad (x, y2, divB.Room.Left, y2 + 1);
				}
			FillDgRect (divA.Road);
			FillDgRect (divB.Road);

			// 通路同士を接続する
			FillVLine (y1, y2, x);

			// 通路を作れた
			return true;
		}
		return false;
	}

	/// <summary>
	/// 水平な通路
	/// </summary>
	/// <param name="left">Left.</param>
	/// <param name="right">Right.</param>
	/// <param name="y">The y coordinate.</param>
	void FillHLine (int left, int right, int y) {
		if (left > right) {
			// 左右の位置関係が逆なので値をスワップする
			int tmp = left;
			left = right;
			right = tmp;
			}
		_floor.FillRectLTRB (left, y, right + 1, y + 1, NONE);
		}

	/// <summary>
	/// 垂直な通路
	/// </summary>
	/// <param name="top">Top.</param>
	/// <param name="bottom">Bottom.</param>
	/// <param name="x">The x coordinate.</param>
	void FillVLine (int top, int bottom, int x) {
		if (top > bottom) {
			// 上下の位置関係が逆なので値をスワップする
			int tmp = top;
			top = bottom;
			bottom = tmp;
			}
		_floor.FillRectLTRB (x, top, x + 1, bottom + 1, NONE);
	}

	/// <summary>
	/// スタートとゴールの穴掘り
	/// </summary>
	void BitFloor(int start, int goal) {
		// スタートの穴掘り
		int y = height;
		while (y > 0) {
			_floor.Set (start, y, NONE);
			if (_floor.Get (start, y-1) == NONE)
				break;
			y--;
		}

		y = 0;
		while(y < height) {
			_floor.Set (goal, y, NONE);
			if (_floor.Get (goal, y + 1) == NONE)
				break;
			y++;
		}
	}

	/// <summary>
	/// タイルを設置
	/// </summary>
	/// <param name="floor">Floor.</param>
	private void Setblock (Layer2D floor, int start, int goal) {
		GameObject obj;
		float stock = 0;
		for (int i = 0; i < floor.Width; i++) {
			for (int j = 0; j < floor.Height; j++) {
				if (floor.Get (i, j) == WALL) {
					stock++;
					// 壁生成
					if (floor.Get (i, j + 1) <= NONE || j == floor.Height - 2) {
						obj = (GameObject)Instantiate (block, new Vector2 (i, j - stock / 2 + 0.5f), new Quaternion ());
						obj.transform.localScale = new Vector2 (1, stock);
						obj.transform.parent = gameObject.transform;
						stock = 0;
						obj.transform.SetAsFirstSibling();
						}

					// 左右のつなぎ目を埋める
					int right = 0;
					int left = 0;
					if (floor.Get (i + 1, j) != NONE)
						right = 1;
					if (floor.Get (i - 1, j) != NONE)
						left = 1;
					if (right == 0 && left == 0) break;
					obj = (GameObject)Instantiate (block, new Vector2 (i + (right - left) * 0.5f, j), new Quaternion ());
					obj.transform.localScale = new Vector2 (right + left, 1);
					obj.transform.parent = gameObject.transform;
					obj.transform.SetAsFirstSibling();
				}
			}
		}
	}

	/// <summary>
	/// 大壁の生成
	/// </summary>
	private void SetBigWall(int start, int goal) {
		GameObject obj;
		// 左
		obj = (GameObject)Instantiate (block, new Vector2 (-wallSize/2, height/2), new Quaternion ());
		obj.transform.localScale = new Vector2(wallSize, height+wallSize*2);
		obj.transform.SetParent (transform);
		// 右
		obj = (GameObject)Instantiate (block, new Vector2 (width+wallSize/2-1, height/2), new Quaternion ());
		obj.transform.localScale = new Vector2 (wallSize, height+wallSize * 2);
		obj.transform.SetParent (transform);
		// 下
		obj = (GameObject)Instantiate (block, new Vector2 (((float)goal/2)-1.5f, -wallSize/2), new Quaternion ());
		obj.transform.localScale = new Vector2 (goal+2, wallSize);
		obj.transform.SetParent (transform);
		obj = (GameObject)Instantiate (block, new Vector2 (((float)(goal + width) / 2)+1.5f, -wallSize / 2), new Quaternion ());
		obj.transform.localScale = new Vector2 ((width - goal)+2, wallSize);
		obj.transform.SetParent (transform);
		// 上
		obj = (GameObject)Instantiate (block, new Vector2 (((float)start/2)-1.5f, height+wallSize/2-1), new Quaternion ());
		obj.transform.localScale = new Vector2 (start+2, wallSize);
		obj.transform.SetParent (transform);
		obj = (GameObject)Instantiate (block, new Vector2 (((float)(start+width) / 2)+1.5f, height + wallSize / 2 - 1), new Quaternion ());
		obj.transform.localScale = new Vector2 ((width - start)+2, wallSize);
		obj.transform.SetParent (transform);

		// ゴールの設置
		goalObj.transform.localPosition = new Vector2 (goal, -wallSize*2/3);
	}
}
