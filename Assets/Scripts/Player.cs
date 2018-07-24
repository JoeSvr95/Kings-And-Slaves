using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour {

	public float speed = 4f;

	Rigidbody2D rb2d;
	Animator anim;
	Vector2 mov;

	public GameObject initialMap;

	void Awake(){
		Assert.IsNotNull(initialMap);
	}

	void Start () {
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();

		Camera.main.GetComponent<MainCamera>().SetBound(initialMap);
	}
	
	void Update () {

		mov = new Vector2(
			Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Vertical")
		);

		/*
		Vector3 mov = new Vector3(
			Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Vertical"),
			0
		);

		transform.position = Vector3.MoveTowards(
			transform.position,
			transform.position + mov,
			speed * Time.deltaTime
		);
		*/

		if (mov != Vector2.zero){
			anim.SetFloat("movX", mov.x);
			anim.SetFloat("movY", mov.y);
			anim.SetBool("walking", true);
		} else {
			anim.SetBool("walking", false);
		}

		if (Input.GetKeyDown("space")){
			anim.SetTrigger("attacking");
		}
	}

	void FixedUpdate(){
		rb2d.MovePosition(rb2d.position + mov * speed * Time.deltaTime);
	}
}
