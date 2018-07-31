using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour {

	public Text back;
	public Text createdBy;
	public Text musicCreatedBy;

	public void Start() {
		back.text = Localization.currentLanguageDictionary["back"];
		createdBy.text = Localization.currentLanguageDictionary["created_by"] + "\n" + "Guilherme Campiotto Silva";
		musicCreatedBy.text = Localization.currentLanguageDictionary["music_by"] + "\n" + "Dan-O - DanoSongs.com";
	}

	public void BackToMenu() {
		SceneManager.LoadScene ("Menu");
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("Menu");
		}
	}
}
