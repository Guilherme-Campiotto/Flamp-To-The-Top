using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localization : MonoBehaviour {

	public static Dictionary<string, string> currentLanguageDictionary = new Dictionary<string, string>();

	public void Awake() {
		CheckVersion ();
		string languageText;
		languageText = GetLanguage();
		if (languageText == "") {
			SetLanguageBySystemLanguage ();
			languageText = GetLanguage ();
		}
		currentLanguageDictionary = LanguageDictionary (languageText);
	}

	/*
	 * Ao clicar em outro idioma atualiza o dicionário que contem todos os textos do jogo
	 */
	public static void restartGameLanguage() {
		string languageText;
		languageText = GetLanguage();
		currentLanguageDictionary = LanguageDictionary (languageText);
	}

	/*
	 * Muda para o idioma selecionado.
	 */
	public static void SetLanguage(SystemLanguage language) {
		TextAsset currentLocalizationText;

		if (language == SystemLanguage.Portuguese) {
			currentLocalizationText = Resources.Load ("Localization/PT-BR", typeof(TextAsset)) as TextAsset;
		} else {
			currentLocalizationText = Resources.Load ("Localization/EN", typeof(TextAsset)) as TextAsset;
		}

		PlayerPrefs.SetString("Language", currentLocalizationText.text);
		restartGameLanguage ();
	}

	public void changeToPortuguese() {
		SetLanguage (SystemLanguage.Portuguese);
	}

	public void changeToEnglish() {
		SetLanguage (SystemLanguage.English);
	}


	/*
	 * Verifica no sistema operacional qual o idioma e coloca no idioma certo o jogo.
	 */
	public void SetLanguageBySystemLanguage() {
		
		TextAsset currentLocalizationText;

		if (Application.systemLanguage == SystemLanguage.Portuguese) {
			currentLocalizationText = Resources.Load ("Localization/PT-BR", typeof(TextAsset)) as TextAsset;
		} else {
			currentLocalizationText = Resources.Load("Localization/EN", typeof(TextAsset)) as TextAsset;
		}

		PlayerPrefs.SetString("Language", currentLocalizationText.text);

	}

	/*
	 * Devolve todos os textos do jogo no idioma definido.
	 */
	public static string GetLanguage() {
		string currentLocalizationText = PlayerPrefs.GetString("Language");
		return currentLocalizationText;
	}

	/*
	 * Faz um dicionário para os textos do jogo, cada chave representa uma palavra ou frase 
	 * em algum idioma, as chaves nunca mudam, só o valor delas dependendo do idioma.
	 */
	public static Dictionary<string, string> LanguageDictionary(string languageStringText) {
		string[] lines = languageStringText.Split(new string[] { "\r\n", "\n\r", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);

		Dictionary<string, string> languageDictionary = new Dictionary<string, string>();

		for (int i = 0; i < lines.Length; i++) {
			string[] pairs = lines[i].Split(new char[] { '\t', '=' }, 2);
			if (pairs.Length == 2)
			{
				languageDictionary.Add(pairs[0].Trim(), pairs[1].Trim());
			}
		}
		return languageDictionary;
	}

	/**
	 * Deleta o dicionario salvo se o jogo possuir uma nova versão para atualizar com o arquivo de texto mais recente
	 */
	public static void CheckVersion() {
		if (!Application.version.Equals (PlayerPrefs.GetString ("Version"))) {
			PlayerPrefs.DeleteKey ("Language");
			PlayerPrefs.SetString ("Version", Application.version);
		}
	}

}
