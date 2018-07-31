using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public Transform target;
	public float followSpeed = 0.3f;
	private Vector3 currentVelocity;

	/*
	 * Faz a camera seguir o personagem no eixo y ao subir as plataformas,
	 * caso o personagem desça para fora da visão da camera ela não irá segui-lo,
	 * pois indica que o personagem caiu e morreu.
	 */
	void LateUpdate() {
		if (target.position.y > this.transform.position.y) {
			Vector3 nextPosition = new Vector3(this.transform.position.x, target.position.y, this.transform.position.z);
			this.transform.position = Vector3.SmoothDamp( this.transform.position, nextPosition, ref currentVelocity, followSpeed * Time.deltaTime);
		}
	}
}
