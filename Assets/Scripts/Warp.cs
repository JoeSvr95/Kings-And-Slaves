using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Warp : MonoBehaviour {

	// Para almacenar el punto de destino
	public GameObject target;

	// Para almacenar el mapa de destino
	public GameObject targetMap;

	void Awake(){
		// Nos aseguramos de que target se ha establecido o lanzaremos exception
		Assert.IsNotNull(target);

		// Esconder el debug de los Warps
		GetComponent<SpriteRenderer>().enabled = false;
		transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

		Assert.IsNotNull(targetMap);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player"){
			col.transform.position = target.transform.GetChild(0).transform.position;
			Camera.main.GetComponent<MainCamera>().SetBound(targetMap);
		}
	}
}
