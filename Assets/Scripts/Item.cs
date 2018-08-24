using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public string itemType;
	
	[HideInInspector]
	public int effect;

	void Start(){
		if (itemType != "health"){
			if (Random.value < 0.5f){
				effect = -1;
			} else {
				effect = 1;
			}
		}
	}
}
