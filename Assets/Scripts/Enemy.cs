using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float visionRadius;
	public float attackRadius;
	public float speed;

	GameObject player;

	Vector3 initialPosition;

	Animator anim;
	Rigidbody2D rb2d;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");

		initialPosition = transform.position;

		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update(){
		// Por defecto nuestro target será la posición inicial del enemigo
		Vector3 target = initialPosition;

		// Comprobamos de colisiones entre el enemigo y el jugador
		RaycastHit2D hit = Physics2D.Raycast(
			transform.position,
			player.transform.position - transform.position,
			visionRadius,
			1 << LayerMask.NameToLayer("Default")
		);

		Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
		Debug.DrawRay(transform.position, forward, Color.red);

		if (hit.collider != null){
			if (hit.collider.tag == "Player"){
				target = player.transform.position;
			}
		}

		// Distancia entre el target y el enemigo
		float distance = Vector3.Distance(target, transform.position);
		Vector3 dir = (target - transform.position).normalized;

		// Si el target está en rango de ataque, el enemigo para para atacarlo
		if (target != initialPosition && distance < attackRadius){
			anim.SetFloat("movX", dir.x);
			anim.SetFloat("movY", dir.y);
			anim.Play("Enemy_Walk", -1, 0);
		} else {
			rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);

			// Al movernos, reproducimos la animación de caminar
			anim.speed = 1;
			anim.SetFloat("movX", dir.x);
			anim.SetFloat("movY", dir.y);
			anim.SetBool("walking", true);
		}

		// Comprobación para evitar bugs forzando la posición inicial
		if (target == initialPosition && distance < 0.02f){
			Debug.Log("ASDasdasd");
			transform.position = initialPosition;
			anim.SetBool("walking", false);
		}

		Debug.DrawLine(transform.position, target, Color.green);
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, visionRadius);
		Gizmos.DrawWireSphere(transform.position, attackRadius);
	}
}
