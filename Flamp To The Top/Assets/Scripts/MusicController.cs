using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

	public AudioSource musicAudioSource;
	public AudioClip musicBookOfTheMonkey;
	public AudioClip musicDublinForever;
	public AudioClip musicparticlesOfSalen;

	// Use this for initialization
	void Awake () {
		
		ChooseMusic ();
		musicAudioSource.volume = 0.8f;

		string soundPref = PlayerPrefs.GetString ("Sound");
		if (soundPref == "On" || soundPref == "") {
			PlayMusic ();
		}

	}


	/**
	 * Escolhe uma das três músicas disponiveis de forma aleatória quando o jogo inicia.
	 */
	public void ChooseMusic() {
		int musicNumber = Random.Range (1, 4);
		switch (musicNumber) {
		case 1:
			musicAudioSource.clip = musicBookOfTheMonkey;
			break;
		case 2:
			musicAudioSource.clip = musicDublinForever;
			break;
		case 3:
			musicAudioSource.clip = musicparticlesOfSalen;
			break;
		}

		musicAudioSource.loop = true;
	}

	public void PlayMusic() {
		musicAudioSource.Play ();
	}

	public void PauseAndUnpauseMusic() {
		if (musicAudioSource.isPlaying) {
			musicAudioSource.Pause ();
		} else {
			musicAudioSource.Play ();
		}
	}
}
