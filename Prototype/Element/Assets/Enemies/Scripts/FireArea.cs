using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour {

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player") {
			GetComponentInParent<FireEnemy> ().PlayerInChaseRange = true;
			Debug.Log ("Found you!");
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Player") {
			GetComponentInParent<FireEnemy> ().PlayerInChaseRange = false;
			Debug.Log ("Lost Sight");
		}
	}
}
