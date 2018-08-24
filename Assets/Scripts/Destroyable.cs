using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Destroyable : MonoBehaviour {

	public string destroyState;
	public float timeForDisable;
	public GameObject[] itemPrefabs;
	public GameObject itemPrefab;

	Animator anim;

	void Awake(){
		bool hasItem = false;
		if (Random.value > 0.5f){
			hasItem = true;
		}

		if (hasItem){
			itemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
			Debug.Log(itemPrefab.name);
		}
	}

	void Start () {
		anim = GetComponent<Animator>();
	}

	IEnumerator OnTriggerEnter2D(Collider2D col){
		Debug.Log(col.tag);
		// Si ataca se reproduce la animacion del jarron
		if (col.tag == "Attack"){
			anim.Play(destroyState);
			yield return new WaitForSeconds(timeForDisable);

			foreach(Collider2D c in GetComponents<Collider2D>()){
				c.enabled = false;
			}
		}
	}
	
	void Update () {
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

		if (stateInfo.IsName(destroyState) && stateInfo.normalizedTime >= 1){
			if (itemPrefab != null){
				GameObject potionObject = Instantiate(
					itemPrefab, transform.position, transform.rotation);
			}
			Destroy(gameObject);
		}
	}
}
