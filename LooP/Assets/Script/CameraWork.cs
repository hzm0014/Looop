﻿using UnityEngine;
using System.Collections;

public class CameraWork : MonoBehaviour {

	// 変数の定義
	private Transform target;

	// シーン開始時に一度だけ呼ばれる関数
	void Start () {
		// 変数にPlayerオブジェクトのtransformコンポーネントを代入
		target = GameObject.Find ("Player").transform;
	}

	// シーン中にフレーム毎に呼ばれる関数
	void Update () {
		// カメラのx座標をPlayerオブジェクトのx座標から取得y座標とz座標は現在の状態を維持
		transform.position = new Vector3 (target.position.x, target.position.y + 2.0f, -20);
	}
}