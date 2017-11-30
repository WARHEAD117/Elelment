using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TreeEnemy : MonoBehaviour {

	bool PlayerInHealRange;
	Transform player;
	NavMeshAgent nav;
	Vector3 HomePoint = new Vector3();

	public Transform PatrolPoint;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent <NavMeshAgent> ();
		HomePoint = transform.position;

		PlayerInHealRange = false;
	}

	// Update is called once per frame
	void Update () {
		
		if (PlayerInHealRange){
			HealPlayer();
		}else{
			Patrol();
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player") {
			PlayerInHealRange = true;
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Player") {
			PlayerInHealRange = false;
		}
	}

	void HealPlayer(){
		Debug.Log ("Heal!");
		gameObject.SetActive (false);
	}

	void ReturnHome(){
		nav.SetDestination (HomePoint);
	}

	void Patrol(){
		if (Vector3.Distance(transform.position,HomePoint)<0.5f){
			nav.SetDestination (PatrolPoint.position);
		}else if (Vector3.Distance(transform.position,PatrolPoint.position)<0.5f){
			ReturnHome ();
		}
	}
}
