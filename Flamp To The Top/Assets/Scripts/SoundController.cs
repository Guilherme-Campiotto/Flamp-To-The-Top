using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour {

	public static bool gameWithSound;
	public Sprite iconSound;
	public Sprite iconMutedSound;
	public Button soundButton;

	public AudioSource audioSource;

	// Instancia do controller para que não seja necessario um audioSource diferente para cada objeto
	public static SoundController instanceSoundController;

	void Awake () {
		if (instanceSoundController == null) {
			instanceSoundController = this;
		} else if(instanceSoundController != this) {
			Destroy (instanceSoundController);
		}
						
		// Só toca os efeitos sonoros se o som estiver ligado.
		string soundPref = PlayerPrefs.GetString ("Sound");
		if (soundPref == "On" || soundPref == "") {
			gameWithSound = true;
			soundButton.image.sprite = iconSound;
			PlayerPrefs.SetString("Sound", "On");
		} else if (soundPref == "Off") {
			gameWithSound = false;
			soundButton.image.sprite = iconMutedSound;
			PlayerPrefs.SetString("Sound", "Off");
		}
	}

	public void SwitchGameVolume() {
		if (gameWithSound) {
			MuteSound ();
		} else {
			UnMuteSound ();
		}
	}

	void MuteSound() {
		gameWithSound = false;
		PlayerPrefs.SetString("Sound", "Off");
		soundButton.image.sprite = iconMutedSound;
	}

	void UnMuteSound() {
		gameWithSound = true;
		PlayerPrefs.SetString("Sound", "On");
		soundButton.image.sprite = iconSound;
	}

	public void PlayJumpSound(AudioClip clip) {
		audioSource.volume = 0.3f;
		audioSource.clip = clip;
		audioSource.Play ();
	}
}
