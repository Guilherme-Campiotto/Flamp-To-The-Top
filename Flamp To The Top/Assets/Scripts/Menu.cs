using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public Text title;
	public Text play;
	public Text options;
	public Text credits;
	public Text quit;
	public Text loading;
	public Text back;
	public Text tutorial;
	public Text howToPlay;

	public GameObject loadingScreen;
	public GameObject tutorialScreen;
	public Slider sliderLoading;

	public void Start() {
		title.text = Localization.currentLanguageDictionary["title"].ToUpper();
		play.text = Localization.currentLanguageDictionary["play"].ToUpper();
		options.text = Localization.currentLanguageDictionary["options"].ToUpper();
		credits.text = Localization.currentLanguageDictionary["credits"].ToUpper();
		quit.text = Localization.currentLanguageDictionary["quit"].ToUpper();
		loading.text = Localization.currentLanguageDictionary["loading"];
		tutorial.text = Localization.currentLanguageDictionary["tutorial"];
		howToPlay.text = Localization.currentLanguageDictionary["how_to_play"];
		back.text = Localization.currentLanguageDictionary["back"];
	}

	/**
	 * Carrega o jogo de forma assincrona, chamando a tela de loading e só abrindo
	 * quando o jogo carregar por completo.
	 */
	public void PlayGame() {
		StartCoroutine (LoadAsynchronously());
	}

	public void Options() {
		SceneManager.LoadScene ("Options");
	}

	public void Credits() {
		SceneManager.LoadScene ("Credits");
	}

	public void OpenMenu() {
		SceneManager.LoadScene ("Menu");
	}

	/**
	 * Direciona para a versão paga do jogo.
	 */
	public void OpenPaidGameLink() {
		Application.OpenURL ("https://play.google.com/store/apps/details?id=com.Campiotto.Games.FlampToTheTop");
	}

	public void QuitGame() {
		Application.Quit ();
	}

	public void OpenTutorial() {
		tutorialScreen.SetActive (true);
	}

	public void BackToTheMenu() {
		tutorialScreen.SetActive (false);
	}

	/**
	 * Mostra a tela de loading, aguarda a operação de loading acabar e atualiza a informação do progresso até iniciar.
	 * Progress vai de -0.9 até 0.9, quando chega no maior valor a tela carregou e a váriavel bool isDone passa 
	 * a ser verdadeira.
	 */
	IEnumerator LoadAsynchronously() {
		loadingScreen.SetActive (true);
		AsyncOperation  operation = SceneManager.LoadSceneAsync("Main Game");
		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress / 0.9f);
			sliderLoading.value = progress;
			yield return null;
		}
	}
}
