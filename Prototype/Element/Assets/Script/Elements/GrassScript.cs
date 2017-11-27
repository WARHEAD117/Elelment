using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassScript : MonoBehaviour {

    ElementScript elementScript;
    ElementType elementType;

    // Use this for initialization
    void Start () {
        elementScript = this.GetComponent<ElementScript>();

    }
	
	// Update is called once per frame
	void Update () {
        if (!elementScript)
            return;

        elementType = elementScript.GetElementType();
        if (elementType == ElementType.GRASS)
        {
            MeshRenderer[] grassRenderer = this.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in grassRenderer)
            {
                if (mr)
                {
                    float ElementColor = elementScript.GetElementValue() / elementScript.GetMaxElementValue();

                    mr.material.color = new Color(ElementColor, ElementColor, ElementColor);
                }
            }
        }
        else if(elementType == ElementType.FIRE)
        {
            MeshRenderer[] grassRenderer = this.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mr in grassRenderer)
            {
                if (mr)
                {
                    float ElementColor = elementScript.GetElementValue() / elementScript.GetMaxElementValue();

                    mr.material.color = new Color(1, 0, 0);
                }
            }
        }

        
    }
}
