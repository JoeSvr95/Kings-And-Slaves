using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour {

	public LevelChanger lvlChanger;
	public Player player;

	public GameObject WinScreen;

	public void Awake(){
		GetComponent<SpriteRenderer>().enabled = false;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player"){
			player = col.gameObject.GetComponent<Player>();
			
			if (GameManager.instance.currentSceneIndex == 2){
				AudioManager.instance.StopSceneMusic(1);
				AudioManager.instance.PlaySceneMusic(0);
				player.Freeze();
				WinScreen.SetActive(true);
			} else {
				lvlChanger.FadeToLevel(2);

				// Conservando los stats del jugador para el próximo nivel
				GameManager.instance.playerMaxHp = player.maxHp;
				GameManager.instance.playerHp = player.hp;
				GameManager.instance.playerDamage = player.damage;
				GameManager.instance.playerSpeed = player.speed;
			}
		}
	}
}
