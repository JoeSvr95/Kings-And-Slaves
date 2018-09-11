using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour {

	public LevelChanger lvlChanger;
	Player player;

	public void Awake(){
		GetComponent<SpriteRenderer>().enabled = false;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player"){
			lvlChanger.FadeToLevel(2);
			player = col.gameObject.GetComponent<Player>();
			GameManager.instance.playerMaxHp = player.maxHp;
			GameManager.instance.playerHp = player.hp;
		}
	}
}
