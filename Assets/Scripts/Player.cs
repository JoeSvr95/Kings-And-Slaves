using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour {

	public float speed = 4f;

	Rigidbody2D rb2d;
	Animator anim;
	Vector2 mov;

	// Vida del jugador
	[Tooltip("Puntos de vida")]
	public int maxHp = 3;
	[Tooltip("Puntos de vida")]
	public int hp; // Vida actual

	CircleCollider2D attackCollider;

	public GameObject initialMap;
	public GameObject arrowPrefab;

	bool movePrevent;

	void Awake(){
		Assert.IsNotNull(initialMap);
		Assert.IsNotNull(arrowPrefab);
	}

	void Start () {
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();

		attackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
		attackCollider.enabled = false;

		Camera.main.GetComponent<MainCamera>().SetBound(initialMap);

		hp = maxHp;
	}
	
	void Update () {
		Movements();

		Animations();

		SwordAttack();

		ArrowAttack();

		PreventMovement();
	}

	void FixedUpdate(){
		rb2d.MovePosition(rb2d.position + mov * speed * Time.deltaTime);
	}

	void Movements(){
		mov = new Vector2(
			Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Vertical")
		);
	}

	void Animations(){
		if (mov != Vector2.zero){
			anim.SetFloat("movX", mov.x);
			anim.SetFloat("movY", mov.y);
			anim.SetBool("walking", true);
		} else {
			anim.SetBool("walking", false);
		}
	}

	void SwordAttack(){
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		bool attacking = stateInfo.IsName("Player_Attack");

		if (Input.GetKeyDown("space") && !attacking){
			anim.SetTrigger("attacking");
		}

		if (mov != Vector2.zero) attackCollider.offset = new Vector2(mov.x/2, mov.y/2);

		if (attacking){
			float playBackTime = stateInfo.normalizedTime;
			if (playBackTime > 0.33 && playBackTime < 0.66) attackCollider.enabled = true;
			else attackCollider.enabled = false;
		}
	}

	void ArrowAttack(){
		// Estado actual mirando la información del animador
		AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
		bool charging = stateinfo.IsName("Player_Arrow");

		if (Input.GetKeyDown(KeyCode.LeftShift)){
			anim.SetTrigger("charging");
		} else if (Input.GetKeyUp(KeyCode.LeftShift)){
			anim.SetTrigger("attack_bow");
			// Conseguir la rotación a partir de un vector
			float angle = Mathf.Atan2(
				anim.GetFloat("movY"),
				anim.GetFloat("movX")
			) * Mathf.Rad2Deg;
			// Creamos la instancia de la flecha
			GameObject arrowObj = Instantiate(
				arrowPrefab, transform.position,
				Quaternion.AngleAxis(angle, Vector3.forward)
			);
			// Movimiento inicial
			Arrow arrow = arrowObj.GetComponent<Arrow>();
			arrow.mov.x = anim.GetFloat("movX");
			arrow.mov.y = anim.GetFloat("movY");

			// Esperar un momento para reactivar el movimiento
			StartCoroutine(EnableMovementAfter(0.4f));
		}

		// Parar el movimiento del jugador cuando carga
		if (charging){
			movePrevent = true;
		}
	}

	void PreventMovement(){
		if (movePrevent){
			mov = Vector2.zero;
		}
	}

	IEnumerator EnableMovementAfter(float seconds){
		yield return new WaitForSeconds(seconds);
		movePrevent = false;
	}

	public void Attacked(){
		if (--hp <= 0) Destroy(gameObject);
	}

	void OnGUI(){
		Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

		GUI.Box(
			new Rect(
				pos.x - 20,
				Screen.height - pos.y - 60,
				40,
				24
			), hp + "/" + maxHp
		);
	}

}
