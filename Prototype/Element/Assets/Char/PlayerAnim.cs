using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour {

    Animator anim;

    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            anim.SetFloat("MoveSpeed", 1);
        }
        else
        {
            anim.SetFloat("MoveSpeed", 0);
        }

        if (Input.GetKey(KeyCode.F))
        {
            anim.SetBool("Absorb", true);
        }
        else
        {
            anim.SetBool("Absorb", false);
        }

        if (Input.GetKey(KeyCode.G))
        {
            anim.SetBool("Release", true);
        }
        else
        {
            anim.SetBool("Release", false);
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            anim.SetBool("Sword",true);
        }
        else
        {
            anim.SetBool("Sword", false);
        }
    }
}
