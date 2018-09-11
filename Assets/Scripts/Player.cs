using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	// Movimientos del jugador
	public float speed = 4f;
	Rigidbody2D rb2d;
	Animator anim;
	Vector2 mov;

	// Vida del jugador
	[Tooltip("Puntos de vida")]
	public int maxHp;
	[Tooltip("Puntos de vida")]
	public int hp; // Vida actual
	public Image[] hearts;
	public Sprite fullHeart;
	public Sprite emptyHeart;

	// Ataque
	CircleCollider2D attackCollider;
	public int damage;

	// Status
	public Text statusTextBox;
	public Text healthText;
	public Text speedText;
	public Text damageText;
	public GameObject statsPanel;

	public GameObject initialMap;
	public GameObject arrowPrefab;
	public GameObject gameOverScreen;

	bool movePrevent;
	float shootCooldown = 0.5f;
	float shootTimer;

	void Awake(){
		Assert.IsNotNull(initialMap);
		Assert.IsNotNull(arrowPrefab);
		damage = 1;
	}

	void Start () {
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();

		attackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
		attackCollider.enabled = false;

		Camera.main.GetComponent<MainCamera>().SetBound(initialMap);

		maxHp = GameManager.instance.playerMaxHp;
		hp = GameManager.instance.playerHp;
		damage = GameManager.instance.playerDamage;
		speed = GameManager.instance.playerSpeed;

		SetStatsPanelValues();

	}
	
	void Update () {
		shootTimer += Time.deltaTime;

		Movements();

		Animations();

		SwordAttack();

		ArrowAttack();

		PreventMovement();

		UpdateHearts();

		if (Input.GetKeyDown(KeyCode.Tab)){
			SetStatsPanelValues();
			statsPanel.SetActive(true);
		} else if (Input.GetKeyUp(KeyCode.Tab)) {
			statsPanel.SetActive(false);
		}
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
			AudioManager.instance.PlaySwordSound();
		}

		if (mov != Vector2.zero) attackCollider.offset = new Vector2(mov.x/2, mov.y/2);

		if (attacking){
			float playBackTime = stateInfo.normalizedTime;
			if (playBackTime > 0.33 && playBackTime < 0.66) attackCollider.enabled = true;
			else attackCollider.enabled = false;
		}
	}

	void ArrowAttack(){
		if (shootTimer < shootCooldown){
			return;
		}

		// Estado actual mirando la información del animador
		AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
		bool charging = stateinfo.IsName("Player_Arrow");

		if (Input.GetKeyDown(KeyCode.LeftShift)){
			anim.SetTrigger("charging");
		} else if (Input.GetKeyUp(KeyCode.LeftShift)){
			shootTimer = 0;
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

			AudioManager.instance.PlayBowShotSound();
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

	void UpdateHearts(){
		for	(int i = 0; i < hearts.Length; i++){
			if (i < hp){
				hearts[i].sprite = fullHeart;
			} else {
				hearts[i].sprite = emptyHeart;
			}

			if (i < maxHp){
				hearts[i].enabled = true;
			} else {
				hearts[i].enabled = false;
			}
		}
	}

	IEnumerator EnableMovementAfter(float seconds){
		yield return new WaitForSeconds(seconds);
		movePrevent = false;
	}

	public void Attacked(){
		AudioManager.instance.PlayOuchPlayerSound();
		if (--hp <= 0){
			movePrevent = true;
			UpdateHearts();
			GameOver();
		}
	}

	public bool AddHealth(int points){
		if ((hp+points) <= maxHp){
			hp += points;
			UpdateHearts();
			return true;
		} else {
			return false;
		}
	}

	public float GetPosX(){
		return mov.x;
	}

	public float GetPosY(){
		return mov.y;
	}

	public void GameOver(){
		gameOverScreen.SetActive(true);
		Time.timeScale = 0f;
		PreventMovement();
		gameObject.SetActive(false);
		AudioManager.instance.PlayGameOverSound();
	}

	public bool ChangeSpeed(int newSpeed){
		if ((speed + newSpeed) > 0){
			speed += newSpeed;
			StartCoroutine(ShowStats(newSpeed, "Velocidad"));
			return true;
		} else {
			return false;
		}
	}

	public bool ChangeDamage(int newDamage){
		if ((damage + newDamage) > 0){
			damage += newDamage;
			StartCoroutine(ShowStats(newDamage, "Daño"));
			return true;
		} else {
			return false;
		}
	}

	public bool ChangeHP(int newhp){
		if ((maxHp + newhp) <= hearts.Length){
			maxHp += newhp;
			if (hp > maxHp){
				hp += newhp;
			}
			if (hp == 0 || maxHp == 0) GameOver();
			UpdateHearts();
			StartCoroutine(ShowStats(newhp, "Vida"));
			return true;
		} else {
			return false;
		}
	}

	IEnumerator ShowStats(int number, string stat){
		string sign = "";
		if (number > 0) sign = "+";
		statusTextBox.text = sign + number + " " + stat;
		yield return new WaitForSeconds(2f);
		statusTextBox.text = "";
	}

	void SetStatsPanelValues(){
		healthText.text = "Vida: " + hp;
		speedText.text = "Velocidad: " + speed;
		damageText.text = "Daño: " + damage;
	}

}
