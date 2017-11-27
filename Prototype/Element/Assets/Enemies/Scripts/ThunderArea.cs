using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderArea : MonoBehaviour {

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player") {
			GetComponentInParent<ThunderEnemy> ().SelfInHurtRange = true;
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Player") {
			GetComponentInParent<ThunderEnemy> ().SelfInHurtRange = false;
		}
	}
}
