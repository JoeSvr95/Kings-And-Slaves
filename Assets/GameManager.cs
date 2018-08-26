using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int currentSceneIndex;

	public static GameManager instance;
	[HideInInspector]
	public GameObject gameOverScreen;

	void Awake(){
		 if(instance == null){
            instance = this;
        }    
        else
        {
            Destroy(gameObject);
            return;
        }

		DontDestroyOnLoad(gameObject);
	}

	void Start(){
		currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		Debug.Log(currentSceneIndex);
	}
	
	void OnEnable(){
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable(){
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode){
		int buildIndex = SceneManager.GetActiveScene().buildIndex;
		AudioManager.instance.sounds[currentSceneIndex].source.Stop();
		AudioManager.instance.StopSceneMusic(currentSceneIndex);
		currentSceneIndex = buildIndex;
		AudioManager.instance.PlaySceneMusic(currentSceneIndex);
	}

}
