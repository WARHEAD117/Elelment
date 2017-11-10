using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : MonoBehaviour {

    public ElementScript element;
    public GameObject ice;
    public GameObject water;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(element.GetElementType()==ElementType.ICE)
        {
            ice.SetActive(true);
            water.SetActive(false);
        }
        else if(element.GetElementType()==ElementType.WATER)
        {
            water.SetActive(true);
            ice.SetActive(false);
        }
	}
}
