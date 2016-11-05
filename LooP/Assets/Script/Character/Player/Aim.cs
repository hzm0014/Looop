using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {
	private bool aimRevise;
	private int aimReviseCount;
	// ARC = Aim Revise Countの略
	private const int ARC_LIMIT_NUM = 20;
	
	// Use this for initialization
	void Start () {
		aimRevise = false;
		aimReviseCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float axisH = Input.GetAxis("AimH")*100;
		float axisV = Input.GetAxis("AimV")*100;
		Debug.Log(axisV);
		if ((int)Input.GetAxis ("AimH") == 0 && (int)Input.GetAxis ("AimV") == 0) {
			aimRevise = false;
			aimReviseCount = 0;
			return;
		}
		else if( !aimRevise && -99.0f >= axisH ) {
			transform.rotation = Quaternion.Euler (0.0f, 0.0f, 180.0f);
			aimReviseCount++;
			if(aimReviseCount >= ARC_LIMIT_NUM) aimRevise = true;
		}
		/*else if( !aimRevise && -99.0f >= axisV ) {
			transform.rotation = Quaternion.Euler (0.0f, 0.0f, -90.0f);
			aimReviseCount++;
			if(aimReviseCount >= ARC_LIMIT_NUM) aimRevise = true;
		}*/
		else {
			transform.rotation = Quaternion.Euler (0.0f, 0.0f, Mathf.Atan2 (Input.GetAxis ("AimV"), Input.GetAxis ("AimH")) * 60);
			if( !(0.0f >= axisH && 0.0f >= axisV)) {
				aimRevise = false;
				aimReviseCount = 0;
			}
		}
	}
}
