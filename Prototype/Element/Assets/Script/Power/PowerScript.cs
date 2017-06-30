using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerScript : MonoBehaviour {

    public Material PowerMat;
    Color ElementColor;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ElementColor.a = 0.5f;
        PowerMat.color = ElementColor;
    }

    public void SetElementColor(Color RodColor)
    {
        ElementColor = RodColor;
    }
}
