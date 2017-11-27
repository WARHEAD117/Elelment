using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireEnemy : MonoBehaviour {

	public bool PlayerInChaseRange;
	bool PlayerInAttackRange;
	bool SelfNearHome;
	Transform player;
	NavMeshAgent nav;
	Vector3 HomePoint = new Vector3();

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
			if (transform.position != HomePoint) {
				ReturnHome ();
			} else {
				Still ();
			}
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player") {
			//PlayerInAttackRange = true;
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Player") {
			PlayerInAttackRange = false;
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
            Debug.Log("FireAttack!");
        }
    }
    public void AreaExit(int id)
    {
        if (id == 1)
        {
            PlayerInChaseRange = false;
            Debug.Log("Lost Sight");
        }
        else if (id == 0)
        {
            PlayerInAttackRange = false;
        }
    }

    void Chase(){
		nav.SetDestination (player.position);
	}

	void AttackMelee(){
		Debug.Log ("FireAttack!");
	}

	void ReturnHome(){
		nav.SetDestination (HomePoint);
	}

	void Still(){
	}
}
