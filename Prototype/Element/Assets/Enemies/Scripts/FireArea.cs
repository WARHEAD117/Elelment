using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour {
    
    public int id = 0;
    void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Player") {
            this.gameObject.GetComponentInParent<FireEnemy>().AreaEnter(id);
		}
	}

	void OnTriggerExit (Collider other){
		if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponentInParent<FireEnemy>().AreaExit(id);
        }
	}
}
