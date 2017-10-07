using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour {

    Animator anim;
    PlayerScript player;
    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();

        GameObject playerObj = GameObject.Find("Player");
        if (!playerObj)
        {
            return;
            Debug.Log("No Player");
        }
        player = playerObj.GetComponent<PlayerScript>();
        if (!player)
        {
            return;
            Debug.Log("No PlayerScript");
        }
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
            anim.SetFloat("MoveSpeed", 0);
        }
        else
        {
            anim.SetBool("Absorb", false);
        }

        if (Input.GetKey(KeyCode.G))
        {
            anim.SetBool("Release", true);
            anim.SetFloat("MoveSpeed", 0);
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
    void ReleasePower(float shootValue)
    {
        if (!player)
        {
            return;
            Debug.Log("No PlayerScript");
        }
        player.ReleasePower(5);
    }
    void AbsorbPower(float shootValue)
    {
        if (!player)
        {
            return;
            Debug.Log("No PlayerScript");
        }
        player.AbsorePower();
    }
    void CreateSword()
    {
        if (!player)
        {
            return;
            Debug.Log("No PlayerScript");
        }
        player.CreateSword();
    }

    void RemoveSword()
    {
        if (!player)
        {
            return;
            Debug.Log("No PlayerScript");
        }
        player.RemoveSword();

    }
}
