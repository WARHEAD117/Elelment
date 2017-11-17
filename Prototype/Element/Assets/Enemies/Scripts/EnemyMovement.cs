using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	Transform player;               // Reference to the player's position.
	//PlayerHealth playerHealth;      // Reference to the player's health.
	//EnemyHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;//Reference to the nav mesh agent.


	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		//playerHealth = player.GetComponent <PlayerHealth> ();
		//enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
	}
		
	void Update ()
	{
		//Fire
		/*
		 if (player is in Chase range && nearhome){
		  Chase();
		  if (player is in Attack range){
		    AttackMelee();
		  }
		 }else if (!nearhome){
		  ReturnHome();
		 }else{
		 Still();
		 }


		//Water
		if (player is in Chase range && nearhome){
		  Chase();
		  if (player is in Attack range){
		    AttackMelee();
		  }
		 }else if (!nearhome){
		  ReturnHome();
		 }else{
		 Patrol();
		 }


		//Electric
		if (player is in Attack range){
		   AttackShoot();
		  }
		if (player is in hurt range){
		   Escape();
		 }
		if (!player is in hurt range && !nearhome){
		   ReturnHome();
		}else{
		 Patrol();
		}


		//Ice
		if (player is in Attack range){
		   AttackShoot();
		  }
		if (player is in hurt range){
		   Escape();
		 }
		if (!player is in hurt range && !nearhome){
		   ReturnHome();
		}else{
		 Still();
		}

		//Tree
		Patrol();
		if (player is in heal range){
		 Heal();
		}
		*/


		// If the enemy and the player have health left...
		//if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
		//{
			// ... set the destination of the nav mesh agent to the player.
			nav.SetDestination (player.position);
		//}
		// Otherwise...
		//else
		//{
			// ... disable the nav mesh agent.
			//nav.enabled = false;
		//}
	} 

	void Still(){
	}

	void Patrol(){
	}

	void Chase(){
	}

	void AttackMelee(){
	}

	void AttackShoot(){
	}

	void Escape(){
	}

	void ReturnHome(){
	}

	void Heal(){
	}
}
