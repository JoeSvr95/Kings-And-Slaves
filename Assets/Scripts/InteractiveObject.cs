using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour {

	public bool IsPickUpItem; // Verificar si el objeto debería de agregarse al inventario
	public bool IsConsumable; // Verificar si el objeto es consumible
	public bool IsOpenable; // Verifica si el objeto se puede abrir
	public bool locked; // Verifica si está bloqueado
	public GameObject itemNeeded; // Objeto que se necesita para interactuar

	public Animator anim;

	public void PickUp(){
		gameObject.SetActive(false);
	}

	public void Open(){
		anim.SetBool("opened", true);
	}
}
