using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{

    PlayerScript player;
    CameraFollower cameraBase;
    MeshRenderer mr;

    List<Wall> invisableWallList = new List<Wall>();
   

    // Use this for initialization
    void Start()
    {
        GameObject playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<PlayerScript>();

        GameObject cameraBaseObj = GameObject.Find("CameraBase");
        cameraBase = cameraBaseObj.GetComponent<CameraFollower>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform cameraTransform = cameraBase.gameObject.transform;
        Transform playerTransform = player.gameObject.transform;
        Vector3 dir = cameraTransform.position - playerTransform.position;
        float length = Vector3.Distance(cameraTransform.position, playerTransform.position);
        RaycastHit[] hitInfo;
        hitInfo = Physics.SphereCastAll(playerTransform.position, 1, dir, length);

        foreach(Wall wallS in invisableWallList)
        {
            wallS.Reset();
        }
        invisableWallList.Clear();

        foreach (RaycastHit hit in hitInfo)
        {
            if (hit.transform.tag == "Wall")
            {
                GameObject wallObj = hit.transform.gameObject;

                Wall wallScript = wallObj.GetComponent<Wall>();
                if (wallScript != null)
                {
                    if (!invisableWallList.Contains(wallScript))
                    {
                        invisableWallList.Add(wallScript);

                        wallScript.SetDisappear();
                    }
                }
            }
        }
    }
}