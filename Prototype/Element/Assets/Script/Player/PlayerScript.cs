﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public enum PlayerState
    {
        IDLE,
        WALK,
        RUN,
        ABSORB,
        RELEASE,
        SWORD,
    }
    PlayerState playerState;

    bool canFinishRelease = true;
    bool canFinishAbsorb = true;
    bool isReleasePower = false;

    CharacterController cc = new CharacterController();
    public RodScript PlayerRod;
    public ContainerScript PlayerContainer;
    public PowerScript PlayerPower;
    public SwordScript PlayerSword;
    public float PlayerSpeed = 10.0f;
    public float PlayerGravity = 15.0f;

    public float ShootValue = 10;

    public float CooldownTime = 1.0f;
    float CooldownTimer = 0;

    int CoinCount = 0;

    public GameObject SwordObj;

    // Use this for initialization
    void Start () {
        playerState = PlayerState.IDLE;
        cc = GetComponent<CharacterController>();
    }
    Vector3 moveDir = new Vector3();
    // Update is called once per frame
    void Update()
    {
        if (!cc)
            return;


        //移動のInput
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //Debug.Log(h + "--" + v);

        
        if (cc.isGrounded)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            cameraForward = cameraForward.normalized;

            Vector3 cameraRight = Camera.main.transform.right;
            cameraRight.y = 0;
            cameraRight = cameraRight.normalized;

            if(!canFinishRelease)
            {
                playerState = PlayerState.RELEASE;
            }
            if (!canFinishAbsorb)
            {
                playerState = PlayerState.ABSORB;
            }


            if (playerState == PlayerState.RUN || playerState == PlayerState.WALK)
            {
                //水平に入力はカメラの左右方向、垂直入力はカメラの前後方向に移動
                moveDir = h * cameraRight + v * cameraForward;
                moveDir = moveDir.normalized;

                moveDir *= PlayerSpeed;

                moveDir.y = -0.001f;
            }
            else
            {
                moveDir.x = 0;
                moveDir.z = 0;
            }

            if(playerState == PlayerState.RELEASE)
            {
                if(isReleasePower)
                {
                    ReleasePower(10);
                }
            }
        }
        else
        {

        }
        //球面補間でプレイヤーの向き変化を自然にする
        Vector3 rotateBeforeDir = transform.forward;
        rotateBeforeDir.y = 0;

        Vector3 rotatAfteroDir = moveDir;
        rotatAfteroDir.y = 0;

        rotatAfteroDir = Vector3.Slerp(rotatAfteroDir, rotateBeforeDir, 0.5f);

        //向きを変える
        if (rotatAfteroDir.x != 0 || rotatAfteroDir.z != 0)
        {
            Quaternion q = Quaternion.LookRotation(rotatAfteroDir, new Vector3(0, 1, 0));

            transform.rotation = q;
        }

        moveDir.y -= PlayerGravity * Time.deltaTime;

        cc.Move(moveDir * Time.deltaTime);

        UpdateRod();

        if (PlayerPower)
        {
            CooldownTimer -= Time.deltaTime;
            CooldownTimer = CooldownTimer < 0 ? 0 : CooldownTimer;
            Debug.Log(CooldownTimer);

        }

        if (leaveInTrigger && leaveOutTrigger)
        {
            if(inDoorObj == outDoorObj)
            {
                ResetElement();
            }
        }
        
    }

    public void SetPlayerState(PlayerState state)
    {
        playerState = state;
    }

    private void ResetElement()
    {
        leaveInTrigger = false;
        leaveOutTrigger = false;
        inDoorObj = null;
        outDoorObj = null;

        PlayerContainer.ResetElement();
    }

    private void UpdateRod()
    {
        
    }

    private void UsePower(float shootValue)
    {
        if (!PlayerRod || !PlayerContainer)
            return;

        ElementType ContainerElement = PlayerContainer.GetContainerElement();
        PlayerPower.SetElement(ContainerElement);

        PlayerPower.UsePower(shootValue);
    }

    public void ReleasePower(float shootValue)
    {
        if (PlayerContainer.CanRelease(ShootValue))
        {
            if (CooldownTimer == 0)
            {
                PlayerContainer.ReleaseElement(ShootValue);
                UsePower(ShootValue);
                //PlayerPower.gameObject.SetActive(true);
                CooldownTimer = CooldownTime;
            }

        }
        else
        {
            PlayerPower.gameObject.SetActive(false);
        }
        moveDir = Vector3.zero;
    }

    public void AbsorePower()
    {
        if (!PlayerRod || !PlayerContainer)
            return;

        if (PlayerRod.CanGetElement)
        {
            moveDir = Vector3.zero;
            Color RodColor = PlayerRod.GetElementColor();
            ElementType RodElement = PlayerRod.GetElement();
            //PlayerContainer.SetContainerElement(RodElement);

            PlayerContainer.KeepGetElement(RodElement);
        }
    }

    public void CreateSword()
    {
        SwordObj.SetActive(true);
        if (PlayerContainer.CanRelease(ShootValue))
        {
            ElementType ContainerElement = PlayerContainer.GetContainerElement();
            PlayerContainer.ReleaseElement(ShootValue - PlayerSword.GetSwordValue());
            PlayerSword.SetElement(ContainerElement, ShootValue - PlayerSword.GetSwordValue());
        }
    }

    public void RemoveSword()
    {
        SwordObj.SetActive(false);
    }

    public void StartReleasePower()
    {
        isReleasePower = true;
    }

    public void EndReleasePower()
    {
        isReleasePower = false;
    }

    public void StartRelease()
    {
        canFinishRelease = false;
    }

    public void FinishRelease()
    {
        canFinishRelease = true;
    }

    public void StartAbsorb()
    {
        canFinishAbsorb = false;
    }

    public void FinishAbsorb()
    {
        canFinishAbsorb = true;
    }

    public float GetElementValue()
    {

        float elementValue = PlayerContainer.GetElementValue();
        return elementValue;

    }
    public float GetMaxElementValue()
    {

        float maxElementValue = PlayerContainer.GetMaxElementValue();
        return maxElementValue;

    }
    public ElementType GetElementType()
    {

        ElementType elementType = PlayerContainer.GetElementType();
        return elementType;

    }

    private bool leaveOutTrigger = false;
    private bool leaveInTrigger = false;
    private GameObject outDoorObj;
    private GameObject inDoorObj;
    void OnTriggerExit(Collider other)
    {
        string name = other.name;
        if (name == "In")
        {
            leaveInTrigger = true;
            inDoorObj = other.transform.parent.gameObject;
        }
        if (name == "Out")
        {
            leaveOutTrigger = true;
            outDoorObj = other.transform.parent.gameObject;
        }

        if (other.tag == "Coin")
        {
            CoinCount++;
            Destroy(other.gameObject);
        }
    }

    public int GetCoinCount()
    {
        return CoinCount;
    }
}
