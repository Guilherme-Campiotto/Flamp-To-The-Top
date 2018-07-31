using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

	public Text restart;
	public Text menu;
	public Text gameOver;

	// Use this for initialization
	void Start () {
		restart.text = Localization.currentLanguageDictionary["restart"];
		menu.text = Localization.currentLanguageDictionary["menu"];
		gameOver.text = Localization.currentLanguageDictionary["gameOver"];
	}

	public void RestartGame() {
		SceneManager.LoadScene ("Main Game");
	}

	public void OpenMenu() {
		SceneManager.LoadScene ("Menu");
	}
}
