using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour {

	public bool IsPickUpItem; // Verificar si el objeto debería de agregarse al inventario
	public bool IsConsumable; // Verificar si el objeto es consumible
	public bool IsOpenable; // Verifica si el objeto se puede abrir
	public bool locked; // Verifica si está bloqueado
	public Item itemNeeded; // Objeto que se necesita para interactuar
	public string typeItemNeeded;

	// Posicion del objeto en el mapa.
	public float PosX;
	public float PosY;

	public Animator anim;

	public void Awake(){
		if (IsOpenable){
			anim.SetFloat("PosX", PosX);
			anim.SetFloat("PosY", PosY);
		}
	}

	public void PickUp(){
		gameObject.SetActive(false);
	}

	public void Open(float posX, float posY){
		if (PosY == 0 && posY == 0){
			anim.SetFloat("PosX", posX);	
		} else if (PosY == 0 && posY == 0){
			anim.SetFloat("PosY", posY);
		}
		anim.SetBool("opened", true);
	}
}
