using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireEnemy : MonoBehaviour {

	public bool PlayerInChaseRange;
	bool PlayerInAttackRange;
	Transform player;
	NavMeshAgent nav;
	Vector3 HomePoint = new Vector3();
	public bool Attacking;

	public float MaxTravelDistance;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent <NavMeshAgent> ();
		HomePoint = transform.position;

		PlayerInChaseRange = false;
		PlayerInAttackRange = false;
	}
		
	// Update is called once per frame
	void Update () {
		if (PlayerInChaseRange){
			Chase();
			if (PlayerInAttackRange){
				AttackMelee();
			}
		}else{
			if (transform.position != HomePoint) {
				ReturnHome ();
			} else {
				Still ();
			}
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
		if (other.gameObject.tag == "Player" && !Attacking) {
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
        else if (id == 0)
        {
            PlayerInAttackRange = false;
			Debug.Log("StopAttack");
        }
    }

    void Chase(){
		nav.SetDestination (player.position);
	}

	void AttackMelee(){
		Debug.Log ("FireAttack!");
		Attacking = true;
	}

	void ReturnHome(){
		nav.SetDestination (HomePoint);
	}

	void Still(){
	}
}
