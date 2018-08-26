using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour {

	public static bool GameIsPaused = false;

	public GameObject pauseMenuUi;
	public GameObject gameOverScreen;
	public LevelChanger lvlChanger;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
		{
			if(GameIsPaused)
			{
				Resume();
			} else
			{
				Pause();
			}
		}
	}

	public void Resume()
	{
		pauseMenuUi.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	void Pause()
	{
		pauseMenuUi.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

    public void LoadMenu()
    {
        Time.timeScale = 1f;
		lvlChanger.SetActive(true);
        lvlChanger.FadeToLevel(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

	public void Restart(){
		Time.timeScale = 1f;
		lvlChanger.SetActive(true);
		lvlChanger.FadeToLevel(1);
		AudioManager.instance.StopGameOverSound();
	}
}
