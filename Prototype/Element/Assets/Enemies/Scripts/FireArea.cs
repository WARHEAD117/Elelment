using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour {
	void OnTriggerEnter (Collider other){
		int id = 0;
		if (other.gameObject.tag == "Player") {
            this.gameObject.GetComponentInParent<FireEnemy>().AreaEnter(id);
		}
	}

	void OnTriggerExit (Collider other){
		int id = 0;
		if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponentInParent<FireEnemy>().AreaExit(id);
        }
	}
}
