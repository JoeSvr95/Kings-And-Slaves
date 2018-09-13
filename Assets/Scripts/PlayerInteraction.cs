using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	Item currentObject = null; // Objeto
	public InteractiveObject interObjScript = null; // Script del objeto
	public Inventory inventory;
	public Player player;
	public Text msgPlayer;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Interactive"){
			currentObject = other.gameObject.GetComponent<Item>();
			interObjScript = currentObject.GetComponent<InteractiveObject>();

			if (interObjScript.IsPickUpItem) {
				AudioManager.instance.PlayKeyPickUp();
				inventory.AddItem(currentObject);
			} else if (interObjScript.IsConsumable){
				UseConsumable(currentObject);
			} else if (interObjScript.IsOpenable){
				// Verificar si el objeto está bloqueado
				if (interObjScript.locked){
					// Verificar si tenemos el objeto necesario para desbloquearlo
					if (inventory.FindItem(interObjScript.itemNeeded, interObjScript.typeItemNeeded)){
						interObjScript.locked = false;
						interObjScript.Open(player.GetPosX(), player.GetPosY());
						AudioManager.instance.PlayDoorSound();
						StartCoroutine(ShowMsg("La puerta ha sido abierta!"));
					} else {
						AudioManager.instance.PlayDoorLocked();
						StartCoroutine(ShowMsg("Se necesita una llave!"));
					}
				}
			}
		}
	}

	void UseConsumable(Item item){
		if (item.itemType == "health"){
			bool healthIncrease  = player.AddHealth(item.effect);
			if (healthIncrease){
				item.SendMessage("PickUp");
				AudioManager.instance.PlayHealthSound();
				Destroy(item);
			}
		} else if (item.itemType == "speed"){
			bool speedIncrease = player.ChangeSpeed(item.effect);
			if (speedIncrease){
				item.SendMessage("PickUp");
				AudioManager.instance.PlayPotionSound();
				Destroy(item);
			}
		} else if (item.itemType == "damage"){
			bool damageIncrease = player.ChangeDamage(item.effect);
			if (damageIncrease){
				AudioManager.instance.PlayPotionSound();
			} else {
				StartCoroutine(ShowMsg("Minimo daño"));
			}
				item.SendMessage("PickUp");
		} else if (item.itemType == "hpbonus"){
			bool damageIncrease = player.ChangeHP(item.effect);
			if (damageIncrease){
				item.SendMessage("PickUp");
				AudioManager.instance.PlayPotionSound();
				Destroy(item);
			}
		}
	}

	IEnumerator ShowMsg(string text){
		msgPlayer.text = text;
		yield return new WaitForSeconds(5f);
		msgPlayer.text = "";
	}
}
