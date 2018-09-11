using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour {
	// Este script será utilizado para cambiar los niveles
	public Animator anim;
	private int lvlToLoad;
	public Text levelIntro;

	void Start(){
		if (levelIntro != null){
			StartCoroutine(
			levelIntro.GetComponent<LevelIntro>().ShowLevel(
				"Nivel " + GameManager.instance.currentSceneIndex + ": " + GameManager.instance.levelName
				)
			);
		}
	}

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
