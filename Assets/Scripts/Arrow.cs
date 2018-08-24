using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	[Tooltip("Esperar X segundos antes de destruir el objeto")]
	public float waitBeforeDestroy;

	[HideInInspector]
	public Vector2 mov;
	public float speed;
	public Player player;

	void Update () {
		transform.position += new Vector3(mov.x, mov.y, 0) * speed * Time.deltaTime;
	}

	void Start(){
		player = FindObjectOfType<Player>();
	}

	IEnumerator OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Object"){
			yield return new WaitForSeconds(waitBeforeDestroy);
			Destroy(gameObject);
		} else if (col.tag != "Player" && col.tag != "Attack"){
			if (col.tag == "Enemy") col.SendMessage("Attacked", player.damage);
			Destroy(gameObject);
		}

	}
}
