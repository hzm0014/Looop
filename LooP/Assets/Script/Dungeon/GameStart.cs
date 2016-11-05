using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// ゴールのスクリプト
/// </summary>
public class GameStart : MonoBehaviour {

	// ダンジョンのオブジェクト
	public GameObject dungeon;

	/// <summary>
	/// 当たり判定
	/// プレイヤーが触れるとステージを再構築する
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
			SceneManager.LoadScene ("Game");
		}
	}
}
