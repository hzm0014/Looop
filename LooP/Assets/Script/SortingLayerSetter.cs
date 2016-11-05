/*
* http://qiita.com/HogeTatu/items/41b9fdc1828c1f22a291 より
*/

using UnityEngine;
using System.Collections;

public class SortingLayerSetter : MonoBehaviour {
	
	[SerializeField]
	private string _sortingLayerName = "Default";
	
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().sortingLayerName = _sortingLayerName;
	}
}