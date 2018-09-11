using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
	public string targetName;
	public Player player;
	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log(col.tag);
		if (col.tag == targetName){
			if (col.tag == "Player"){
				col.SendMessage("Attacked");
			} else {
				col.SendMessage("Attacked", player.damage);
			}
		} 
	}
}
