using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ThunderEnemy : MonoBehaviour {

	public bool SelfInHurtRange;
	bool PlayerInAttackRange;
	bool SelfNearHome;
	bool IsAttacking;
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

		SelfInHurtRange = false;
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

		if (PlayerInAttackRange) {
			if (!SelfInHurtRange) {
				AttackShoot ();
				IsAttacking = true;
			} else {
				Escape ();
				IsAttacking = false;
			}
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

	void Escape(){
		//nav.isStopped = false;
		Debug.Log ("Run!");
	}

	void AttackShoot(){
		nav.isStopped = true;
		Debug.Log ("ElectricAttack!");
	}

	void ReturnHome(){
		nav.SetDestination (HomePoint);
	}

	void Patrol(){
		nav.isStopped = false;
		if (Vector3.Distance(transform.position,HomePoint)<2.0f){
			nav.SetDestination (PatrolPoint.position);
		}else if (Vector3.Distance(transform.position,PatrolPoint.position)<2.0f){
			ReturnHome ();
		}
	}
}

