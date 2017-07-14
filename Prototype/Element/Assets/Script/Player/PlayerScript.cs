using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    CharacterController cc = new CharacterController();
    public RodScript PlayerRod;
    public ContainerScript PlayerContainer;
    public PowerScript PlayerPower;
    public float PlayerSpeed = 10.0f;
    public float PlayerGravity = 15.0f;
    // Use this for initialization
    void Start () {

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

        Debug.Log(h + "--" + v);

        
        if (cc.isGrounded)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            cameraForward = cameraForward.normalized;

            Vector3 cameraRight = Camera.main.transform.right;
            cameraRight.y = 0;
            cameraRight = cameraRight.normalized;

            //水平に入力はカメラの左右方向、垂直入力はカメラの前後方向に移動
            moveDir = h * cameraRight + v * cameraForward;
            moveDir = moveDir.normalized;

            moveDir *= PlayerSpeed;

            moveDir.y = -0.001f;
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
            if (PlayerContainer.CanRelease() && Input.GetKey(KeyCode.G))
            {
                UsePower();
                PlayerPower.gameObject.SetActive(true);
                PlayerContainer.ReleaseElement();
            }
            else
            {
                PlayerPower.gameObject.SetActive(false);
            }
        }
        
    }

    private void UpdateRod()
    {
        if (!PlayerRod || !PlayerContainer)
            return;

        if(Input.GetKey(KeyCode.F) && PlayerRod.CanGetElement)
        {
            Color RodColor = PlayerRod.GetElementColor();
            ElementType RodElement = PlayerRod.GetElement();
            PlayerContainer.SetContainerElement(RodElement);

            PlayerContainer.KeepGetElement();
        }
    }

    private void UsePower()
    {
        if (!PlayerRod || !PlayerContainer)
            return;

        ElementType ContainerElement = PlayerContainer.GetContainerElement();
        PlayerPower.SetElement(ContainerElement);

        PlayerPower.UsePower();
    }
}
