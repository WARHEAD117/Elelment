using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterArea : MonoBehaviour {

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player") {
			GetComponentInParent<WaterEnemy> ().PlayerInChaseRange = true;
			Debug.Log ("Found you!");
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Player") {
			GetComponentInParent<WaterEnemy> ().PlayerInChaseRange = false;
			Debug.Log ("Lost Sight");
		}
	}
}

