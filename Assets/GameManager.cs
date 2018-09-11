using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Datos de las escenas
	public int currentSceneIndex;
	public string levelName;

	public static GameManager instance;
	[HideInInspector]
	public GameObject gameOverScreen;

	// Stats iniciales del jugador
	public int playerMaxHp;
	public int playerHp;
	public float playerSpeed;
	public int playerDamage;
	GameObject player;
	Text levelIntro; 

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
		
		// Parar el atual audio y reproducir el nuevo audio
		AudioManager.instance.sounds[currentSceneIndex].source.Stop();
		AudioManager.instance.StopSceneMusic(currentSceneIndex);
		currentSceneIndex = buildIndex;
		AudioManager.instance.PlaySceneMusic(currentSceneIndex);

		// Setear el nombre del nivel
		SetLevelName();

		if (player != null){
			player.GetComponent<Player>().maxHp = playerMaxHp;
			player.GetComponent<Player>().hp = playerHp;
		}

	}

	
	void SetLevelName(){
		switch (currentSceneIndex){
			case 0:
				levelName = "Main Menu";
				break;
			case 1:
				levelName = "Mazmorras";
				break;
			case 2:
				levelName = "Sotano";
				break;
			default:
				levelName = "";
				break;
		}
	}

}
