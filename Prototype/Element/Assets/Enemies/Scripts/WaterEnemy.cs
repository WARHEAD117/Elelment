using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterEnemy : MonoBehaviour {

	public bool PlayerInChaseRange;
	bool PlayerInAttackRange;
	bool SelfNearHome;
	Transform player;
	NavMeshAgent nav;
	Vector3 HomePoint = new Vector3();

	public Transform PatrolPoint;

	public float MaxTravelDistance;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent <NavMeshAgent> ();
		HomePoint = transform.position;

		PlayerInChaseRange = false;
		PlayerInAttackRange = false;
		SelfNearHome = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector3.Distance (transform.position, HomePoint) > MaxTravelDistance) {
			SelfNearHome = false;
		} else {
			SelfNearHome = true;
		}

		if (PlayerInChaseRange && SelfNearHome){
			Chase();
			if (PlayerInAttackRange){
				AttackMelee();
			}
		}else if (!SelfNearHome){
			ReturnHome();
		}else{
			Patrol();
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player") {
			PlayerInAttackRange = true;
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Player") {
			PlayerInAttackRange = false;
		}
	}

	void Chase(){
		nav.SetDestination (player.position);
	}

	void AttackMelee(){
		Debug.Log ("WaterAttack!");
	}

	void ReturnHome(){
		nav.SetDestination (HomePoint);
	}

	void Patrol(){
		if (Vector3.Distance(transform.position,HomePoint)<2.0f){
			nav.SetDestination (PatrolPoint.position);
		}else if (Vector3.Distance(transform.position,PatrolPoint.position)<2.0f){
			ReturnHome ();
		}
	}
}
