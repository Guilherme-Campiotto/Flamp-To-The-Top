using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public Text resume;
	public Text menu;
	public Text quit;
	public Text restart;

	public static bool gameIsPaused = false;
	public GameObject pauseMenuUI;


	// Use this for initialization
	void Start () {
		resume.text = Localization.currentLanguageDictionary["resume"].ToUpper();
		restart.text = Localization.currentLanguageDictionary["restart"].ToUpper();
		menu.text = Localization.currentLanguageDictionary["menu"].ToUpper();
		quit.text = Localization.currentLanguageDictionary["quit"].ToUpper();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (gameIsPaused) {
				Resume();
			} else {
				Pause();
			}
		}
	}

	public void Resume(){
		pauseMenuUI.SetActive (false);
		Time.timeScale = 1f;
		gameIsPaused = false;		
	}

	public void Pause() {
		pauseMenuUI.SetActive (true);
		Time.timeScale = 0f;
		gameIsPaused = true;
	}

	public void QuitGame() {
		Application.Quit ();
	}

	public void LoadMenu() {
		Time.timeScale = 1f;
		SceneManager.LoadScene ("Menu");
	}

	public void RestartGame() {
		Time.timeScale = 1f;
		SceneManager.LoadScene ("Main Game");
	}
}
