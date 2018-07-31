using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour {

	public Text options;
	public Text back;
	public Text language;
	public Text portuguese;
	public Text english;
	public Text sound;

	public void Start() {
		setTexts ();
	}

	public void setTexts() {
		options.text = Localization.currentLanguageDictionary["options"];
		back.text = Localization.currentLanguageDictionary["back"];
		language.text = Localization.currentLanguageDictionary["language"];
		portuguese.text = Localization.currentLanguageDictionary["language_portuguese"];
		english.text = Localization.currentLanguageDictionary["language_english"];
		sound.text = Localization.currentLanguageDictionary["sound"];
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
