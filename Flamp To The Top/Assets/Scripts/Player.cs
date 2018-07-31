using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Player : MonoBehaviour {

	public float movementSpeed = 10.0f;
	public static bool alive;
	Transform playerTransform;
	bool  rightDirection = true;
	float positionX;
	float limitXpositionLeft = 4.2f;
	float limitXpositionRight = 11.2f;

	Vector3 position;

	void Start() {
		playerTransform = this.GetComponent<Transform> ();
		alive = true;
	}

	void Update () {

		CheckFall ();

		if (alive && Time.timeScale == 1f) {
			if (Input.GetKey (KeyCode.LeftArrow)) {

				this.transform.Translate (new Vector2 (-movementSpeed * Time.deltaTime, 0));
			}

			if (Input.GetKey (KeyCode.RightArrow)) {
				this.transform.Translate (new Vector2 (movementSpeed * Time.deltaTime, 0));
			}
				
			this.transform.Translate (new Vector2(Input.acceleration.x * 0.5f, 0));

			if (this.transform.position.x < limitXpositionLeft) {
				positionX = limitXpositionRight;
				this.transform.position = new Vector2(positionX, this.transform.position.y);
			}

			if (this.transform.position.x > limitXpositionRight) {
				positionX = limitXpositionLeft;
				this.transform.position = new Vector2(positionX, this.transform.position.y);
			}

			MobileControls ();
		}

	}

	/**
	 * Vira o personagem dependendo da direção que ele esta olhando
	 */
	void flipSpriteImage() {
		rightDirection = !rightDirection;
		Vector3 scale = playerTransform.localScale;
		scale.x *= -1;
		playerTransform.localScale = scale;
	}

	void MobileControls() {
		if (!rightDirection && Input.acceleration.x > 0) {
			flipSpriteImage ();
		}

		if (rightDirection && Input.acceleration.x < 0) {
			flipSpriteImage ();
		}
	}

	/**
	 * O personagem fora da visão da camera indica que ele morreu,
	 * caso verdade chama o método que mostra a tela de Game Over.
	 */
	void CheckFall() {
		position = Camera.main.WorldToViewportPoint(this.transform.position);
		if (position.y < 0.0) {
			alive = false;
		}
	}

}
