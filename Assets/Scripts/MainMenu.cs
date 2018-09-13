using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public LevelChanger lvlChanger;
	public GameObject controls;

	public void PlayGame(){
		lvlChanger.FadeToLevel(1);
	}

	public void QuitGame(){
		Application.Quit(); 
	}

	public void ShowControls(){
		controls.SetActive(true);
	}

	public void HideControls(){
		controls.SetActive(false);
	}

}
