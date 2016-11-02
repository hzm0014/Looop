using UnityEngine;
using System.Collections;

/// <summary>
/// ゴールのスクリプト
/// </summary>
public class Goal : MonoBehaviour {

	// ダンジョンのオブジェクト
	public GameObject dungeon;

	/// <summary>
	/// 当たり判定
	/// プレイヤーが触れるとステージを再構築する
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
			Transform parent_trans = dungeon.transform;
			for (int i = parent_trans.childCount - 1; i >= 0; --i) {
				Destroy (parent_trans.GetChild (i).gameObject);
			}
			DungeonGeneratorII d = dungeon.GetComponent<DungeonGeneratorII> ();
			d.GenerateDungeon ();
		}
	}
}
