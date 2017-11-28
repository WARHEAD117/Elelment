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
		int id = 1;
		if (other.gameObject.tag == "Player") {
			AreaEnter(id);
		}
	}

	void OnTriggerExit (Collider other){
		int id = 1;
		if (other.gameObject.tag == "Player") {
			AreaExit(id);
		}
	}

	public void AreaEnter(int id)
	{
		if (id == 1)
		{
			PlayerInChaseRange = true;
			Debug.Log("Found you!");
		}
		else if (id == 0)
		{
			PlayerInAttackRange = true;
		}
	}
	public void AreaExit(int id)
	{
		if (id == 1)
		{
			PlayerInChaseRange = false;
			Debug.Log("Lost Sight");
		}
	}


	void Chase(){
		nav.SetDestination (player.position);
	}

	void AttackMelee(){
		Debug.Log ("GameOver!");
		Patrol ();
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
