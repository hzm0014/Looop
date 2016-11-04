using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class MaxRecord : Singleton<MaxRecord> {
	string name = "a";
	protected MaxRecord() {}
	public GameObject ui;
	private int record;

	public bool DontDestroyEnabled = true;

	// Use this for initialization
	void Start () {

		var folderpath = Application.persistentDataPath + "/Database/";
		var filePath = folderpath + name + ".json";
		if (!File.Exists (filePath)) return;

		var jsonText = File.ReadAllText (filePath);
		if (string.IsNullOrEmpty (jsonText)) return;

		/// Json文字列をCharacterDataの配列に変換する
		var json = JsonUtility.FromJson<int> (jsonText);
		record = (int)json;

	}

	public void SetRecord(int entry) {
		record = Mathf.Max (record, entry);
		ui.GetComponent<Text> ().text = "最高記録 B " + record;

	}

	/// <summary>
	/// ファイルに書き出す
	/// </summary>
	public void FileWrite () {
		string json = JsonUtility.ToJson (record);

		// 保存するフォルダー
		var path = Application.persistentDataPath + "/Database/";

		// フォルダーがない場合は作成する
		if (!Directory.Exists (path)) {
			Directory.CreateDirectory (path);
			}

		File.WriteAllText (path + name + ".json", json);
	}

}
