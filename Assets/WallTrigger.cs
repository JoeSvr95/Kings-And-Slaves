using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour {

	public GameObject boss;

	void Update(){
		if (boss == null){
			Destroy(gameObject);
		}
	}
}
