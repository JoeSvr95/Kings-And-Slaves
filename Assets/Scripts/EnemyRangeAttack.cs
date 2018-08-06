using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour {

	[Tooltip("Valocidad del movimiento en unidades del mundo")]
	public float speed;

	GameObject player;
	Rigidbody2D rb2d;
	Vector3 target, dir;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
		rb2d = GetComponent<Rigidbody2D>();

		// Recuperamos la posición del jugador y la dirección normalizada
		if (player != null){
			target = player.transform.position;
			dir = (target - transform.position).normalized;
		}
	}

	void FixedUpdate(){
		if (target != Vector3.zero){
			rb2d.MovePosition(transform.position + (dir * speed) * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		// Si chocamos contra el jugador o un ataque la borramos
		if (col.transform.tag == "Player" || col.transform.tag == "Attack"){
			Destroy(gameObject);
		}		
	}

	void OnBecameInvisible(){
		Destroy(gameObject);
	}

}
