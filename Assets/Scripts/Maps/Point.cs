using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {

	// Use this for initialization
	public int row;
	public int column;
	private string type_border = "";
	public string type_cell;
	private string model = "1";
	private string path;
	public static bool asigned = false;

	// PATH -> type_border/type_typecell_model | 'Type'type_cell/type_typecell_model
	/*
	void assign_prefab () {

		if (column == 0){
			type_border = "R";
		}
		if (column == 3){
			type_border = "";
		}

		type_cell = Random.Range(0, 2).ToString();

		if (type_border != ""){
			path = "Type" + type_border + "/type_1" + "_" + model;
		}else{
			path = "Type" + type_cell + "/type_" + type_cell + "_" + model;
		}

		if (row == 0){
			var prefab = Resources.Load<GameObject>(path);
			GameObject.Instantiate(prefab, transform.position, transform.rotation);
			asigned = true;
		}

	}
	 */

	public void assign_tile () {

		string tile_name = "type_" + type_cell + "_1";
		print(tile_name);
		var tile = Resources.Load<GameObject>(tile_name);
		GameObject.Instantiate(tile, transform.position, transform.rotation);

	}

	void Start () {
		// assign_prefab();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
