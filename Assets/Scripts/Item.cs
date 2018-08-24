using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public string itemType;
	
	[HideInInspector]
	public int effect;

	void Start(){
		Debug.Log(itemType);
		if (gameObject.name != "Health"){
			if (Random.value < 0.5f){
				effect = -1;
			} else {
				effect = 1;
			}
			Debug.Log(effect);
		}
	}
}
