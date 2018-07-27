using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour {

	public string destroyState;
	public float timeForDisable;

	Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}

	IEnumerator OnTriggerEnter2D(Collider2D col){
		Debug.Log(col.tag);
		// Si ataca se reproduce la animacion del jarron
		if (col.tag == "Attack"){
			anim.Play(destroyState);
			yield return new WaitForSeconds(timeForDisable);

			foreach(Collider2D c in GetComponents<Collider2D>()){
				c.enabled = false;
			}
		}
	}
	
	void Update () {
		
	}
}
