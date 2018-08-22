using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {
	// Este script será utilizado para cambiar los niveles
	public Animator anim;
	private int lvlToLoad;

	public void FadeToLevel(int levelToLoad){
		lvlToLoad = levelToLoad;
		anim.SetTrigger("fade_out");
	}

	public void OnFadeComplete(){
		SceneManager.LoadScene(lvlToLoad);
	}

	public void SetActive(bool state){
		gameObject.SetActive(state);
	}
}
