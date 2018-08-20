using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public GameObject[] inventory = new GameObject[5];
	public Image[] inventorySprites = new Image[5];

	public void AddItem(GameObject item){
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
}
