using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	public float timeBetweenAttacks = 1f;
	public int attackDamage = 10;

	GameObject cat;
	CatHealth catHealth;
	bool catInRange;
	float timer;

	// Use this for initialization
	void Start () {
		cat = GameObject.FindGameObjectWithTag ("Cat");
		catHealth = cat.GetComponent<CatHealth> ();
		Debug.Log("Player Attack Started");
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("Cat in range check", other.gameObject);
		if (other.gameObject == cat) {
			Debug.Log("**Cat is in range**");
			catInRange = true;
		}
	}

	void OnTriggerExit(Collider other) {
		Debug.Log("Cat out of range check", other.gameObject);
		if (other.gameObject == cat) {
			catInRange = false;
		}
	}

	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer >= timeBetweenAttacks && catInRange && catHealth.currentHealth > 0) {
			Debug.Log("**Atack**");
			Attack ();
		}

		if (catHealth.currentHealth <= 0) {
			//cat is dead, do something
		}
	}

	void Attack() {
		timer = 0f;

		if (catHealth.currentHealth > 0) {
			catHealth.TakeDamage (attackDamage);
		}
	}
}
