using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject gameOverScreen;

	public void EndGame(){
		gameOverScreen.SetActive(true);
		Debug.Log("GAME OVER");
	}

}
