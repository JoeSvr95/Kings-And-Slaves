using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public LevelChanger lvlChanger;

	public void PlayGame(){
		lvlChanger.FadeToLevel(1);
	}

	public void QuitGame(){
		Debug.Log("Quit!");
		Application.Quit(); 
	}

}
