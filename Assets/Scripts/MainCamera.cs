using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	Transform target;
	float tLX, tLY, bRX, bRY;

	void Awake(){
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	/*void Start(){
		// Forzar resolucion cuadrada
		Screen.SetResolution(800, 800, true);
	}*/
	void Update () {

		if (Input.GetKey("escape")) Application.Quit();

		// Hace que la camara siga al personaje
		transform.position = new Vector3(
			Mathf.Clamp(target.position.x,tLX,bRX),
			Mathf.Clamp(target.position.y,bRY,tLY),
			transform.position.z
		);	
	}

	public void SetBound(GameObject map){
		// Este metodo mantiene la camara dentro de la escena
		Tiled2Unity.TiledMap config = map.GetComponent<Tiled2Unity.TiledMap>();
		float cameraSize = Camera.main.orthographicSize;

		tLX = map.transform.position.x + cameraSize;
		tLY = map.transform.position.y - cameraSize;
		bRX = map.transform.position.x + config.NumTilesWide - cameraSize;
		bRY = map.transform.position.y - config.NumTilesHigh + cameraSize;
	}
}
