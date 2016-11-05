using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using System.IO;

public class MaxRecord : Singleton<MaxRecord> {
	protected MaxRecord() {}
	private int record;

	public bool DontDestroyEnabled = true;

	// Use this for initialization
	void Start () {
		StreamReader sr = new StreamReader (
		@"C:\record.txt", Encoding.GetEncoding ("Shift_JIS"));
		SetRecord (int.Parse(sr.ReadLine ()));
		sr.Close ();
	}

	public void SetRecord(int entry) {
		record = Mathf.Max (record, entry);
		GetComponent<Text> ().text = "HIGHT SCORE :B" + record;
		FileWrite ();

	}

	/// <summary>
	/// ファイルに書き出す
	/// </summary>
	public void FileWrite () {
		System.IO.File.Delete (@"C:\record.txt");
		Encoding sjisEnc = Encoding.GetEncoding ("Shift_JIS");
		StreamWriter writer =
		   new StreamWriter (@"C:\record.txt", true, sjisEnc);
		writer.Write (record);
		writer.Close ();
	}

}
