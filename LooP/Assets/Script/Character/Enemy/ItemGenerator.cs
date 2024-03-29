﻿using UnityEngine;
using System.Collections;

public class ItemGenerator : Singleton<ItemGenerator> {
	protected ItemGenerator(){}

	// アイテム
	public GameObject[] items;

	/// <summary>
	/// マップにアイテムを設置する
	/// </summary>
	/// <param name="floor">フロアの地形情報</param>
	/// <param name="num">アイテム数</param>
	private void _RandomDeploy (Layer2D floor, int num) {
		for (int i = 0; i < num; ){
			int x = Random.Range (1, floor.Width);
			int y = Random.Range (1, floor.Height);
			if (floor.Get (x, y) == 0) {
				int index = Random.Range (0, items.Length);
				GameObject obj = (GameObject)Instantiate (items[index], new Vector2 (x, y), Quaternion.identity);
				obj.transform.SetParent (GameObject.Find ("Dungeon").transform);
				i++;
			}
		}
	}
	public static void RandomDeploy(Layer2D floor, int num) {
		Instance._RandomDeploy (floor, num);
	}
}
