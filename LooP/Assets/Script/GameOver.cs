using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {

	int cnt;
	// Use this for initialization
	void Start () {
		cnt = 0;
	}
	
	// Update is called once per frame
	void Update () {
		cnt++;
		if(cnt > 100) {
			transform.Find ("Text").gameObject.SetActive (false);
			transform.Find ("Logo").gameObject.SetActive (true);
		}
		if (Input.anyKey) {
			SceneManager.LoadScene ("Game");
			DungeonGeneratorII.Instance.GenerateDungeon ();
			gameObject.SetActive (false);
		}
	}
}
