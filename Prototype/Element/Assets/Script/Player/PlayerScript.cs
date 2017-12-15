using System.Collections;
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


    bool canSword = true;
    bool canFinishRelease = true;
    bool canFinishAbsorb = true;
    bool isReleasePower = false;
    bool isAbsorbPower = false;

    CharacterController cc = new CharacterController();
    public RodScript PlayerRod;
    public ContainerScript PlayerContainer;
    public PowerScript PlayerPower;
    public SwordScript PlayerSword;
    public float PlayerSpeed = 10.0f;
    public float PlayerGravity = 15.0f;

    public float ShootAtk = 10;
    public float ShootSpend = 1;
    public float SwordAtk = 30;
    public float SwordSpend = 5;

    public float ShootCooldownTime = 1.0f;
    float ShootCooldownTimer = 0;

    public float SwordSpeed = 1;

    public float MaxContainerValue = 200;

    public float playerHP = 100;
    public float playerMAXHP = 100;

    int CoinCount = 0;

    // Use this for initialization
    void Start () {
        playerState = PlayerState.IDLE;
        cc = GetComponent<CharacterController>();
    }
    Vector3 moveDir = new Vector3();
    // Update is called once per frame
    void Update()
    {
        UpdateMove();
        UpdateContainer();
        UpdateRod();

        if (PlayerPower)
        {
            ShootCooldownTimer -= Time.deltaTime;
            ShootCooldownTimer = ShootCooldownTimer < 0 ? 0 : ShootCooldownTimer;
            //Debug.Log(CooldownTimer);

        }

        if (leaveInTrigger && leaveOutTrigger)
        {
            if(inDoorObj == outDoorObj)
            {
                ResetElement();
            }
        }
        
    }

    void UpdateMove()
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

            if (!canFinishRelease)
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

            if (playerState == PlayerState.RELEASE)
            {
                if (isReleasePower)
                {
                    //ReleasePower(10);
                }
            }
            if (!isAbsorbPower && !isReleasePower)
            {
                canSword = true;
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
    }

    void UpdateContainer()
    {
        if (!PlayerContainer)
            return;

        PlayerContainer.SetMaxContainerValue(MaxContainerValue);
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
        if (PlayerContainer.CanRelease(ShootAtk))
        {
            if (ShootCooldownTimer == 0)
            {
                PlayerContainer.ReleaseElement(ShootSpend);
                UsePower(ShootAtk);
                //PlayerPower.gameObject.SetActive(true);
                ShootCooldownTimer = ShootCooldownTime;
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
        PlayerSword.ActiveSword();
    }

    public void RemoveSword()
    {
        PlayerSword.DeactiveSword();
    }

    public void StartRelease()
    {
        isReleasePower = true;
        canFinishRelease = false;
    }

    public void FinishRelease()
    {
        canFinishRelease = true;
        isReleasePower = false;
    }

    public void StartAbsorb()
    {
        canFinishAbsorb = false;
        isAbsorbPower = true;
    }

    public void FinishAbsorb()
    {
        canFinishAbsorb = true;
        isAbsorbPower = false;
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

    public float GetSwordAtk()
    {
        return SwordAtk;
    }
    public float GetSwordSpend()
    {
        return SwordSpend;
    }

    public float GetShootAtk()
    {
        return ShootAtk;
    }
    public float GetShootSpend()
    {
        return ShootSpend;
    }

    public void ReleaseElement(float value)
    {
        PlayerContainer.ReleaseElement(value);
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

    public bool GetCanSword()
    {
        return canSword;
    }

    public float GetSwordSpeed()
    {
        return SwordSpeed;
    }

    public float GetPlayerHP()
    {
        return playerHP;
    }

    public float GetPlayerMaxHP()
    {
        return playerMAXHP;
    }

    public void GetAttacked(float damage)
    {
        playerHP -= damage;
    }
}
