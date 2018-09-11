using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int currentSceneIndex;

	public static GameManager instance;
	[HideInInspector]
	public GameObject gameOverScreen;

	public int playerMaxHp;
	public int playerHp;
	GameObject player;

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
		playerHp = 3;
		playerMaxHp = 3;
		currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		player = GameObject.FindGameObjectWithTag("Player");
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
		if (player != null){
			player.GetComponent<Player>().maxHp = playerMaxHp;
			player.GetComponent<Player>().hp = playerHp;
		}
	}

}
