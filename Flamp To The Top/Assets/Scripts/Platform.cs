using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	private GameController gameController;
	public bool playerIsAlive;
	public Vector2 platformPositionLeft = new Vector2 (-3.84f, 6.0f);
	public Vector2 platformPositionCenter = new Vector2 (0.055f, 30.0f);
	public Vector2 platformPositionRight = new Vector2 (4.06f, 6.0f);
	public float jumpForce = 10.0f;
	public bool breakOnContact = false;
	public bool firstPlatform = false;

	public static int platformsAlreadyDestroyed = 0;
	public static int dificultLevel;

	int rangeToDefineIfPlatformCanBreak = 6;
	int rangeToDefineIfPlatformCanDoSuperJump = 5;

	Color32 colorPlatformBreakable = new Color32(221, 89, 89, 255);
	Color32 colorPlatformSuperJump = new Color32(7, 240, 115, 255);

	public AudioClip jumpSound;
	public AudioClip jumpSound2;

	/* 
	 * Garante diferentes tipos de plataformas de forma aleatória.
	 * - Plataformas que quebram no contato.
	 * - Plataformas que fazem o jogador pular mais alto.
	 */
	void Start () {

		gameController = GameObject.Find("GameController").GetComponent<GameController>();

		DefineChanceOfPlatformType ();

		if (!firstPlatform) {
			breakOnContact = RandomTrueOrFalseReturn (rangeToDefineIfPlatformCanBreak);
			if (breakOnContact) {
				this.GetComponent<SpriteRenderer> ().color = colorPlatformBreakable;
			} else {
				bool makePlaformJumpHigher = RandomTrueOrFalseReturn (rangeToDefineIfPlatformCanDoSuperJump);
				if (makePlaformJumpHigher) {
					jumpForce = 30.0f;
					this.GetComponent<SpriteRenderer>().color = colorPlatformSuperJump;
				}
			}
		}
	}

	void DefineChanceOfPlatformType() {
		switch (GameController.dificultLevel) {
		case 2:
			rangeToDefineIfPlatformCanBreak = 5;
			break;
		case 3:
			rangeToDefineIfPlatformCanBreak = 4;
			rangeToDefineIfPlatformCanDoSuperJump = 4;
			break;
		case 4:
			rangeToDefineIfPlatformCanBreak = 2;
			rangeToDefineIfPlatformCanDoSuperJump = 3;
			break;
		}
	}

	/*
	 * Retorna true se o numero sorteado for igual a 1.
	 * Para diminuir a chance de um retorno true, um numero maior
	 * no parametro max deve ser enviado.
	 */
	private bool RandomTrueOrFalseReturn(int max) {
		int numberChosen = Random.Range (0, max);
		if (numberChosen == 1) {
			return true;
		} else {
			return false;
		}
	}
		
	void OnCollisionEnter2D(Collision2D collision) {
		bouncePlayer(collision);
	}

	/*
	 * Garante que o personagem sempre pule ao encostar em uma plataforma.
	 * A força de pulo só é aplicada quando o personagem esta caindo, ou seja,
	 * se o personagem vier por baixo da plataforma ele não irá pular imediatamente.
	 */
	void bouncePlayer(Collision2D collision) {
		if (collision.relativeVelocity.y >= 0.01f && Player.alive) {
			Rigidbody2D collisionRigidBody = collision.collider.GetComponent<Rigidbody2D> ();
			if (collisionRigidBody != null) {
				PlayJumpSound ();
				Vector2 velocity = collisionRigidBody.velocity;
				velocity.y = jumpForce;
				collisionRigidBody.velocity = velocity;
			}

			if (breakOnContact) {
				platformsAlreadyDestroyed += 1;
				DestroyPlatform (this.gameObject);
			}
		}
	}

	void PlayJumpSound() {
		if (SoundController.gameWithSound) {
			int soundNumber = Random.Range (0, 2);
			if (soundNumber == 1) {
				SoundController.instanceSoundController.PlayJumpSound (jumpSound);
			} else {
				SoundController.instanceSoundController.PlayJumpSound (jumpSound2);
			}
		}
	}

	/*
	 * Destroi a plataforma se o jogador encostar
	 */
	void DestroyPlatform(GameObject platform) {
		gameController.RemoveFromListDestroyedPlatform(platform);
	}

}
