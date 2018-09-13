using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public GameObject boss1;
	public GameObject boss2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (boss1 == null && boss2 == null){
			Destroy(gameObject);
		}
	}
}
