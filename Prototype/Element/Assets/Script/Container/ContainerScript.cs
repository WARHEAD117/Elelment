using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour {

    public Material ContainerMat;
    Color ElementColor;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ContainerMat.color = ElementColor;
	}

    public Color GetElementColor()
    {
        return ElementColor;
    }

    public void SetElementColor(Color RodColor)
    {
        ElementColor = RodColor;
    }
}
