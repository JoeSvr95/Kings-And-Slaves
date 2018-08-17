using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	GameObject currentObject = null;
	public InteractiveObject interObjScript = null;
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
