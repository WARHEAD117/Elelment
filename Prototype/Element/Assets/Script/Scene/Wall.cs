using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    PlayerScript player;
    CameraFollower cameraBase;
    MeshRenderer mr;

    bool beInvisable = false;

    // Use this for initialization
    void Start ()
    {
        beInvisable = false;
        mr = this.GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(beInvisable == true)
        {
            mr.enabled = false;
        }
        else
        {
            mr.enabled = true;
        }
    }

    public void SetDisappear()
    {
        beInvisable = true;
    }

    public void Reset()
    {
        beInvisable = false;
    }
}
