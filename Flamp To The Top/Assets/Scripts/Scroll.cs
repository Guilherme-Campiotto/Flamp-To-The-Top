using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scroll : MonoBehaviour {

	public float speedVertical = -0.5f;
	public float speedHorizontal = 10.0f;
	public Camera camera;
	Vector3 positionToCamera;
	float cameraPrevRotation;

	// Use this for initialization
	void Start () {
		cameraPrevRotation = camera.transform.position.y;
	}

	
	// Update is called once per frame
	void Update () {
		if (this.CompareTag ("CloudMenu")) {
			MoveHorizontal ();
		} else {
			MoveVertical ();
		}
	}

	void MoveHorizontal() {
		Vector2 offset = new Vector2 (speedHorizontal * Time.deltaTime, 0);
		this.transform.Translate (offset);

		if (this.GetComponent<RectTransform>().position.x > 500.0f) {
			float positionX = -500.0f;
			float positionY = Random.Range(-500.0f, 500.0f);
			this.GetComponent<RectTransform>().position = new Vector2 (positionX, positionY);
		}
	}

	void MoveVertical() {
		// Se a camera se mover, faz as nuvens descerem para dar a impressão de que o personagem esta indo mais alto
		if (cameraPrevRotation != camera.transform.position.y) {
			Vector2 offset = new Vector2 (0, speedVertical * Time.deltaTime);
			this.transform.Translate (offset);
		}

		cameraPrevRotation = camera.GetComponent<Transform> ().transform.position.y;

		// Se a nuvem ficar abaixo do cenario ela volta pro alto em uma posição aleatoria para parecer que é outra nuvem
		positionToCamera = Camera.main.WorldToViewportPoint(this.transform.position);
		if (positionToCamera.y < -1.0f) {
			float positionX = Random.Range(5.0f, 12.0f);
			float positionY = camera.GetComponent<Transform>().position.y + Random.Range(7.0f, 20.0f);;
			this.transform.position = new Vector2 (positionX, positionY);
		}
	}
}
