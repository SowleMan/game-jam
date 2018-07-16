using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public float sinkSpeed;

	CapsuleCollider capsuleCollider;
	bool isDead;
	bool isSinking;

	// Use this for initialization
	void Start () {
		capsuleCollider = GetComponent<CapsuleCollider> ();

		currentHealth = startingHealth;
		Debug.Log("Cat health started");
	}
	
	// Update is called once per frame
	void Update () {
		if (isSinking) {
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}

	public void TakeDamage(int amount) {
		Debug.Log("**Take Damage Called**");

		if (isDead) {
			return;
		}

		currentHealth -= amount;

		if (currentHealth <= 0) {
			Death ();
		}

	}

	void Death() {
		Debug.Log("**Death Called**");
		isDead = true;

		capsuleCollider.isTrigger = true;
	}

	public void startSinking() {
		//Get and disable the Nav Mesh Agent
		// GetComponent ().enabled = false;

		//Find the rigidBody component and make it kinetic
		//GetComponent.isKinematic = true;

		isSinking = true;
		Debug.Log("Start Sinking");

		Destroy (gameObject, 2f);
	}
}
