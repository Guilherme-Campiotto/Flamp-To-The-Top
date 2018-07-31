using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameController : MonoBehaviour {

	public Text scoreText;
	public Text highScore;
	int scoreNumber = 0;
	int highScoreNumber;
	int highestPoint = 0;

	public GameObject platform;
	public GameObject movablePlatform;
	GameObject platformToSpawn;
	public List<GameObject> platformsList;

	public GameObject player;

	public Animator gameOverAnimator;
	public Button restartButton;
	public Button menuButton;

	public int numberOfPlatforms = 50;
	public float minY = 0.7f;
	public float maxY = 2.4f;

	public float minX;
	public float maxX;

	float gameTime;
	public float timeToIncreaseDificult = 20.0f;
	bool gameOverScreen;

	Vector3 spawnPosition = new Vector3 ();

	Vector3 platformNormalSize = new Vector3(0.29f, 0.24f, 1);
	Vector3 platformSmallSize = new Vector3(0.15f, 0.24f, 1);

	// Atualmente a dificuldade máxima que o jogo chega é 4
	public static int dificultLevel;
	static int timesPlayedSinceLastAd = 0;

	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		minX = 4.5f;
		maxX = 10.0f;
		dificultLevel = 1;
		platformToSpawn = platform;
		spawnPosition.y = 4.3f;
		generatePlatforms (numberOfPlatforms);
		highScoreNumber = PlayerPrefs.GetInt ("HighScore", 0);
		setTextScore ();
		gameOverScreen = false;

	}

	/*
	 * Cria plataformas em alturas e distancias aleatorias, considerando um intervalo mínimo.
	 */
	void generatePlatforms(int numberOfObjects) {
		for(int i = 0; i < numberOfObjects; i++) {
			spawnPosition.y += Random.Range (minY, maxY);
			spawnPosition.x = Random.Range (minX, maxX);
			CheckDificultAndChangePlatformType ();
			GameObject newPlatform = Instantiate (platformToSpawn, spawnPosition, Quaternion.identity);
			CheckDificultAndChangePlatformSize (newPlatform);
			platformsList.Add (newPlatform);
		}
	}

	/*
	 * Destroi plataformas conforme o jogador vai subindo e outras plataformas são geradas.
	 * Esse método garante uma melhor performance para o jogo.
	 * O numero total de plataformas removidas do jogo é igual ao numero de plataformas criadas menos 
	 * o numero de plataformas já destruídas pelo jogador ao encostar. Sem essa conta o jogo iria destruir
	 * mais plataformas do que gerar novas.
	 */
	void destroyPlatforms(int numberOfObjectsToDestroy) {
		int totalNumberOfObjectsToDestroy = numberOfObjectsToDestroy - Platform.platformsAlreadyDestroyed;
		for(int i = 0; i < totalNumberOfObjectsToDestroy; i++) {
			Destroy (platformsList [0]);
			platformsList.Remove(platformsList[0]);
		}

		Platform.platformsAlreadyDestroyed = 0;
	} 

	/*
	 * Destroi a plataforma quebrável se o jogador encostar
	 */
	public void RemoveFromListDestroyedPlatform(GameObject platformDestroyied) {
		platformsList.Remove(platformDestroyied);
		Destroy (platformDestroyied);
	}

	/**
	 * Ajusta a pontuação do jogo com base na altura alcançada pelo personagem
	 */
	public void setTextScore() {
		if (highestPoint < player.GetComponent<Transform>().position.y) {
			highestPoint = (int)player.GetComponent<Transform>().position.y;
			scoreNumber = highestPoint * 9;
			if (scoreNumber > highScoreNumber) {
				highScoreNumber = scoreNumber;
				saveHighScore ();
			}
		}
		highScore.text = Localization.currentLanguageDictionary["highScore"] + " " + highScoreNumber.ToString ();
		scoreText.text = Localization.currentLanguageDictionary["score"] + " " + scoreNumber.ToString();
	}

	public void saveHighScore() {
		PlayerPrefs.SetInt ("HighScore", highScoreNumber);
	}

	/**
	 * Reseta a pontuação(somente para testes)
	 */
	public void resetHighScore() {
		PlayerPrefs.DeleteKey ("HighScore");
	}

	public void gameOver() {
		//ShowAdvertise ();
		restartButton.interactable = true;
		menuButton.interactable = true;
		saveHighScore ();
		gameOverAnimator.SetTrigger ("GameOver");
	}

	public void RestartGame() {
		SceneManager.LoadScene ("Main Game");
	}

	/**
	 * Mostra a propagando após o jogador jogar duas partidas seguidas.
	 */
	public void ShowAdvertise() {
		if (Advertisement.IsReady ("video") && timesPlayedSinceLastAd == 1) {
			Advertisement.Show ("video");
			timesPlayedSinceLastAd = 0;
		} else {
			timesPlayedSinceLastAd++;
		}
	}

	public void ExitGame() {
		Application.Quit ();
	}
		
	void Update () {

		float distancePlayerLastPlatform = platformsList [platformsList.Count - 1].transform.position.y - player.GetComponent<Transform>().position.y;

		if (distancePlayerLastPlatform < 30.0f) {
			destroyPlatforms (20);
			generatePlatforms (20);
		}

		if (!Player.alive && !gameOverScreen) {
			gameOverScreen = true;
			gameOver ();
		}
			
		setTextScore ();
		
		gameTime += Time.deltaTime;
		if (gameTime >= timeToIncreaseDificult) {
			gameTime = 0f;
			IncreaseDificult ();
		}
	}

	void IncreaseDificult() {
		if (dificultLevel < 4) {
			dificultLevel++;
		}
	}

	/**
	 * Verifica o nivel do jogo e aumenta a dificultada de pular nas plataformas.
	 * As plataformas diminuem de tamanho e começam a se mover.
	 */
	void CheckDificultAndChangePlatformSize(GameObject platform) {
		switch (dificultLevel) {
		case 2:
			platform.transform.localScale = platformSmallSize;
			break;
		case 3:
			platform.transform.localScale = platformNormalSize;
			break;
		case 4:
			platform.transform.localScale = platformSmallSize;
			break;
		}
	}

	/**
	 * Ao chegar no nivel 3 as plataformas começam a se mover.
	 * Diminui a area que a plataformas nascem para que a movimentação delas não acontẽça muito fora da tela.
	 */
	void CheckDificultAndChangePlatformType() {
		if (dificultLevel >= 3) {
			platformToSpawn = movablePlatform;
			minX = 6.5f;
			maxX = 9.0f;
		}
	}

}
