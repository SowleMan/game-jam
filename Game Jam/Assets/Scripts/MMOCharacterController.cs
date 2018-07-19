using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMOCharacterController : MonoBehaviour {

	public Transform playerCam, character, centerPoint;

	private float mouseX, mouseY;
	public float mouseSensitivity = 10f;
	public float mouseYPosition = 1f;

	private float moveFB, moveLR;
	public float moveSpeed = 2f;

	private Vector3 fly;
	public float flyForce;

	private float zoom;
	public float zoomSpeed = 2f;

	public float zoomMin = -2f;
	public float zoomMax = -10f;

	public float rotationSpeed = 5f;

	// Use this for initialization
	void Start () {
		zoom = -3;
		fly = new Vector3 (0.0f, 2.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {

		zoom += Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed;

		if (zoom > zoomMin)
			zoom = zoomMin;
		
		if (zoom < zoomMax)
			zoom = zoomMax;

		playerCam.transform.localPosition = new Vector3 (0, 0, zoom);

		//Uncomment to control camera with RMB
		//if (Input.GetMouseButton (1)) {
			mouseX += Input.GetAxis ("Mouse X");
			mouseY -= Input.GetAxis ("Mouse Y");
		//}

		mouseY = Mathf.Clamp (mouseY, -60f, 60f);
		playerCam.LookAt (centerPoint);
		centerPoint.localRotation = Quaternion.Euler (mouseY, mouseX, 0);

		moveFB = Input.GetAxis ("Vertical") * moveSpeed;
		moveLR = Input.GetAxis ("Horizontal") * moveSpeed;

		Vector3 movement = new Vector3 (moveLR, 0, moveFB);
		movement = character.rotation * movement;

		character.GetComponent<CharacterController> ().Move (movement * Time.deltaTime);
		centerPoint.position = new Vector3 (character.position.x, character.position.y + mouseYPosition, character.position.z);

		if (Input.GetAxis ("Vertical") > 0 | Input.GetAxis("Vertical") < 0) {
			Quaternion turnAngle = Quaternion.Euler (0, centerPoint.eulerAngles.y, 0);

			character.rotation = Quaternion.Slerp (character.rotation, turnAngle, Time.deltaTime * rotationSpeed);
		}

		if (Input.GetKey (KeyCode.Space) && character.transform.position.y < 20f) {
			Fly ();
		}
		if (Input.GetKey (KeyCode.LeftShift)) {
			Fall ();
		}
	}

	void Fly() {
		Debug.Log ("flying");
		Vector3 fly = new Vector3 (0, flyForce, 0);
		character.GetComponent<CharacterController> ().Move (fly * Time.deltaTime);
	}
		
	void Fall() {
		Debug.Log ("falling");
		Vector3 fly = new Vector3 (0, -flyForce, 0);
		character.GetComponent<CharacterController> ().Move (fly * Time.deltaTime);
	}
}
