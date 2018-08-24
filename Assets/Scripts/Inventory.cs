using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public Item[] inventory = new Item[5];
	public Image[] inventorySprites = new Image[5];

	public void AddItem(Item item){
		bool itemAdded = false;
		
		// Verificar si hay espacio en el inventario
		for (int i = 0; i < inventory.Length; i++){
			if (inventory[i] == null){
				inventory[i] = item;
				// Actualizar Inventario UI
				inventorySprites[i].color = Color.white;
				inventorySprites[i].overrideSprite = item.GetComponent<SpriteRenderer>().sprite;
				inventorySprites[i].preserveAspect = true;
				Debug.Log(item.tag + " was added");
				itemAdded = true;
				item.SendMessage("PickUp");
				break;
			}
		}

		if (!itemAdded){
			Debug.Log("Inventory lleno!");
		}
	}

	public bool FindItem(Item item, string type){
		Debug.Log(item.itemType);
		for (int i = 0; i < inventory.Length; i++){
			if (inventory[i] == item){
				if (inventory[i].itemType == type)
					Debug.Log("No es la llave correcta");
					return true;
			}
		}
		return false;
	}
}
