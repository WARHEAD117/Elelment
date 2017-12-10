using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour {

    Material ContainerMat;
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
        ContainerMat = this.GetComponent<MeshRenderer>().material;
        ContainerElement = ElementType.NONE;
	}
	
	// Update is called once per frame
	void Update () {
        //ContainerMat.color = ElementColor;
        ElementColor = ElementDefine.GetElementColor(ContainerElement);
        ContainerMat.color = Color.Lerp(ElementBaseColor, ElementColor, curElementValue / maxElementValue);

    }

    public void SetMaxContainerValue(float value)
    {
        maxElementValue = value;
    }
    public float GetMaxContainerValue()
    {
        return maxElementValue;
    }

    public ElementType GetElementType()
    {
        return ContainerElement;
    }
    public float GetElementValue()
    {
        return curElementValue;
    }
    public float GetMaxElementValue()
    {
        return maxElementValue;
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
    public bool CanRelease(float shootValue)
    {
        return curElementValue >= shootValue ? true : false;
    }

    public void KeepGetElement(ElementType element)
    {
        if (ContainerElement != element)
        {
            ContainerElement = element;
            curElementValue = 0;
        }
        if (curElementValue < maxElementValue)
            curElementValue += (getElementSpeed * Time.deltaTime);
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

    public void ReleaseElement(float shootValue)
    {
        if (curElementValue >= shootValue)
            curElementValue -= shootValue;
    }

    public void ResetElement()
    {
        curElementValue = 0;
    }
}
