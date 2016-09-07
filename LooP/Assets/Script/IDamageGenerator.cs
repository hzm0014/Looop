using UnityEngine;
using System.Collections;

public interface IDamageGenerator : MonoBehaviour {
	/// <summary>
	/// ダメージの攻撃力を取得する
	/// </summary>
	float getPower();
	
	/// <summary>
	/// ダメージ対象を吹き飛ばす方向を取得する
	/// </summary>
	Vector3 getForce();
	
	/// <summary>
	/// ダメージ対象を吹き飛ばす速度を取得する
	/// </summary>
	float getForceSpeed();
}
