using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
	public string targetName;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == targetName) col.SendMessage("Attacked");
	}
}
