using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	GameObject currentObject = null; // Objeto
	public InteractiveObject interObjScript = null; // Script del objeto
	public Inventory inventory;
	public Player player;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Interactive"){
			currentObject = other.gameObject;
			interObjScript = currentObject.GetComponent<InteractiveObject>();

			if (interObjScript.IsPickUpItem) {
				inventory.AddItem(currentObject);
			} else if (interObjScript.IsConsumable){
				UseConsumable(currentObject);
			} else if (interObjScript.IsOpenable){
				// Verificar si el objeto está bloqueado
				if (interObjScript.locked){
					// Verificar si tenemos el objeto necesario para desbloquearlo
					if (inventory.FindItem(interObjScript.itemNeeded)){
						interObjScript.locked = false;
						interObjScript.Open();
						Debug.Log("Door was unlocked!");
					} else {
						Debug.Log("Door needs a key!");
					}
				}
			}
		}
	}

	void UseConsumable(GameObject item){
		bool healthIncrease  = player.AddHealth(1);
		if (healthIncrease){
			item.SendMessage("PickUp");
			Destroy(item);
		}
	
	}
}
