using UnityEngine;
using System.Collections;

public interface IDamageGenerator {
	/// <summary>
	/// ダメージの攻撃力を取得する
	/// </summary>
	float GetPower();
	
	/// <summary>
	/// ダメージ対象を吹き飛ばす方向を取得する
	/// </summary>
	Vector2 GetForce();
	
	/// <summary>
	/// ダメージ対象を吹き飛ばす速度を取得する
	/// </summary>
	float GetForceSpeed();
}
