using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour {

    public Material ContainerMat;
    Color ElementColor;
    Color ElementBaseColor = Color.black;
    ElementType ContainerElement;
    ElementType ContainerElementBase = ElementType.NONE;
    public float curElementValue = 0;
    public float maxElementValue = 100;
    public float getElementSpeed = 50;
    public float releaseElementSpeed = 50;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //ContainerMat.color = ElementColor;
        ElementColor = ElementDefine.GetElementColor(ContainerElement);
        ContainerMat.color = Color.Lerp(ElementBaseColor, ElementColor, curElementValue / maxElementValue);

    }

    public ElementType GetContainerElement()
    {
        return ContainerElement;
    }

    public void SetContainerElement(ElementType element)
    {
        if (ContainerElement != element)
        {
            ContainerElement = element;
            curElementValue = 0;
        }
    }

    public bool CanRelease()
    {
        return curElementValue > 0 ? true : false;
    }

    public void KeepGetElement()
    {
        if (curElementValue < maxElementValue)
            curElementValue += getElementSpeed * Time.deltaTime;
        else
            curElementValue = maxElementValue;
    }

    public void ReleaseElement()
    {
        if (curElementValue > 0)
            curElementValue -= releaseElementSpeed * Time.deltaTime;
        else
            curElementValue = 0;
    }
}
