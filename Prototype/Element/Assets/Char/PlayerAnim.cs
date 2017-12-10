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

    bool startSword = false;
	// Update is called once per frame
	void Update () {
        player.SetPlayerState(PlayerScript.PlayerState.IDLE);


        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || h!=0 || v != 0)
        {
            player.SetPlayerState(PlayerScript.PlayerState.RUN);
            anim.SetFloat("MoveSpeed", 1);
        }
        else
        {
            player.SetPlayerState(PlayerScript.PlayerState.IDLE);
            anim.SetFloat("MoveSpeed", 0);
        }

        if (Input.GetKey(KeyCode.F) || Input.GetButton("Fire1") )
        {
            player.SetPlayerState(PlayerScript.PlayerState.ABSORB);
            anim.SetBool("Absorb", true);
            anim.SetFloat("MoveSpeed", 0);
        }
        else
        {
            anim.SetBool("Absorb", false);
        }

        if (Input.GetKey(KeyCode.G) || Input.GetButton("Fire2"))
        {
            player.SetPlayerState(PlayerScript.PlayerState.RELEASE);
            anim.SetBool("Release", true);
            anim.SetFloat("MoveSpeed", 0);
        }
        else
        {
            anim.SetBool("Release", false);
        }
        
        if (Input.GetKeyDown(KeyCode.T) || Input.GetButtonDown("Fire3") && player.GetCanSword())
        {
            player.SetPlayerState(PlayerScript.PlayerState.SWORD);
            anim.SetBool("Sword",true);
            anim.SetFloat("SwordSpeed", player.GetSwordSpeed());
        }
        else
        {
            anim.SetBool("Sword", false);
        }

        bool isSword = anim.GetCurrentAnimatorStateInfo(1).IsName("Sword");
        if(isSword)
        {

            startSword = true;
        }
        else
        {
            startSword = false;
        }
        if(startSword)
        {
            anim.SetLayerWeight(1, 1);
            player.CreateSword();
        }
        else
        {
            anim.SetLayerWeight(1, 0);
            player.RemoveSword();
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
    

    void StartRelease()
    {
        if (!player)
        {
            return;
            Debug.Log("No PlayerScript");
        }
        player.StartRelease();

    }
    void FinishRelease()
    {
        if (!player)
        {
            return;
            Debug.Log("No PlayerScript");
        }
        player.FinishRelease();

    }
    void StartAbsorb()
    {
        if (!player)
        {
            return;
            Debug.Log("No PlayerScript");
        }
        player.StartAbsorb();

    }
    void FinishAbsorb()
    {
        if (!player)
        {
            return;
            Debug.Log("No PlayerScript");
        }
        player.FinishAbsorb();

    }

    void RemoveSword()
    {
        if (!player)
        {
            return;
            Debug.Log("No PlayerScript");
        }
        //player.RemoveSword();

    }

    void CreateSword()
    {
        if (!player)
        {
            return;
            Debug.Log("No PlayerScript");
        }
       // player.CreateSword();
    }

}
