using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour {

	public bool IsPickUpItem; // Verificar si el objeto debería de agregarse al inventario
	public bool IsConsumable; // Verificar si el objeto es consumible
	public void PickUp(){
		gameObject.SetActive(false);
	}
}
