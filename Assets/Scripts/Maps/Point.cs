using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {

	// Use this for initialization
	public int row;
	public int column;
	private string type_border = "";
	private string type_cell;
	private string model = "1";
	private string path;
	public static bool asigned = false;

	// PATH -> type_border/type_typecell_model | 'Type'type_cell/type_typecell_model

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

	void Start () {
		// assign_prefab();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
