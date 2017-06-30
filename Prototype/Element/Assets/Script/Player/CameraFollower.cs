using UnityEngine;
using System.Collections;


public class CameraFollower : MonoBehaviour
{
    public GameObject targetObj;
    Vector3 targetPos;
    public float angleLimit = 45;

    public bool isMouse = false;
    Vector3 initCameraForward;
    void Start()
    {
        targetPos = targetObj.transform.position;
        initCameraForward = targetPos - Camera.main.transform.position;
    }

    void Update()
    {
        // targetの移動量分、自分（カメラ）も移動する
        transform.position += targetObj.transform.position - targetPos;
        targetPos = targetObj.transform.position;


        Vector3 baseCameraForward = targetPos - Camera.main.transform.position;

        Vector3 oldCameraForward = Camera.main.transform.forward;

        Quaternion oldCameraRot = Camera.main.transform.rotation;

        Vector3 oldCameraPos = Camera.main.transform.position;
        if (!isMouse)
        {
            // コントローラ操作
            {
                // 右ジョイスティックの移動量(X軸)
                float joyInputX = Input.GetAxis("HorizontalR");
                // 右ジョイスティックの移動量(Y軸)
                float joyInputY = Input.GetAxis("VerticalR");

                if (joyInputX != 0)
                {

                    // targetの位置のY軸を中心に、回転（公転）する
                    transform.RotateAround(Camera.main.transform.position, new Vector3(0, 1, 0), joyInputX * Time.deltaTime * 200f);


                    //角度の制限
                    Vector3 newCameraForward = Camera.main.transform.forward;
                    newCameraForward.y = 0;
                    baseCameraForward.y = 0;
                    float dotCamera = Vector3.Dot(newCameraForward.normalized, baseCameraForward.normalized);
                    float angle = Mathf.Acos(dotCamera);
                    angle *= Mathf.Rad2Deg;
                    if (angle > angleLimit)
                    {
                        //transform.forward = oldCameraForward;
                        transform.rotation = oldCameraRot;
                        transform.position = oldCameraPos;
                    }
                }
                //自動的に元方向に戻る
                else
                {
                    Vector3 newCameraForward = Camera.main.transform.forward;


                    Vector3 newForward = Vector3.Slerp(newCameraForward, baseCameraForward, 0.1f);
                    Quaternion q = Quaternion.FromToRotation(newCameraForward, newForward);

                    transform.forward = newForward;
                }
                

            }
        }
        else
        {
            // マウスの右クリックを押している間
            if (Input.GetMouseButton(1))
            {
                // マウスの移動量(X軸)
                float mouseInputX = Input.GetAxis("Mouse X");
                // targetの位置のY軸を中心に、回転（公転）する
                transform.RotateAround(Camera.main.transform.position, new Vector3(0, 1, 0), mouseInputX * Time.deltaTime * 200f);

                // マウスの移動量(Y軸)
                float mouseInputY = Input.GetAxis("Mouse Y");
                mouseInputY = 0;
                //targetの位置のX軸を中心に、回転（公転）する
                transform.RotateAround(Camera.main.transform.position, Camera.main.transform.right, mouseInputY * Time.deltaTime * 200f);

                Vector3 newCameraForward = Camera.main.transform.forward;
                newCameraForward.y = 0;
                baseCameraForward.y = 0;
                float dotCamera = Vector3.Dot(newCameraForward.normalized, baseCameraForward.normalized);

                //角度の制限
                float angle = Mathf.Acos(dotCamera);
                angle *= Mathf.Rad2Deg;
                if (angle > angleLimit)
                {
                    //transform.forward = oldCameraForward;
                    transform.rotation = oldCameraRot;
                    transform.position = oldCameraPos;
                }

            }
            //自動的に元方向に戻る
            else
            {


                Vector3 newCameraForward = Camera.main.transform.forward;


                Vector3 newForward = Vector3.Slerp(newCameraForward, baseCameraForward, 0.1f);
                Quaternion q = Quaternion.FromToRotation(newCameraForward, newForward);

                //transform.rotation *= q;
                transform.forward = newForward;
            }

        }

        // マウスのホイール
        float fWheel = Input.GetAxis("Mouse ScrollWheel");
        //ズームイン・アウト
        transform.Translate(0, 0, fWheel * 10);
    }
}