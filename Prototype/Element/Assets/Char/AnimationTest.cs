using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour {

	Animator anim;

	void Start(){
		anim = gameObject.GetComponent<Animator> ();
	}

	void Update () {

		if (Input.GetKeyDown(KeyCode.Q)) {
			anim.SetTrigger("Walk");
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			anim.SetTrigger("Run");
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			anim.SetTrigger("Absorb");
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			anim.SetTrigger("Release");
		}
		if (Input.GetKeyDown(KeyCode.T)) {
			anim.SetTrigger("Sword");
		}
	}
}
