using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour {

	public Transform building;
	public UnityEngine.AI.NavMeshAgent agent;
	public CatHealth catHealth;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (catHealth.currentHealth > 0) {
			agent.SetDestination (building.position);
		} else {
			agent.enabled = false;
		}
	}
}
