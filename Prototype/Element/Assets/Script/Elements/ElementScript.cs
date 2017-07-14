using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
    FIRE,
    WATER,
    THUNDER,
    GRASS,
    ICE,
    NONE
}

public class ElementScript : MonoBehaviour {

    public ElementType m_ElementType = ElementType.NONE;
    public float ElementValue = 100;
    public float MaxElementValue = 100;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        if (mr)
        {
            Color ElementColor = ElementDefine.GetElementColor(m_ElementType);
            ElementColor = ElementColor * ElementValue / MaxElementValue;

            mr.material.color = ElementColor;
        }
		
        if(ElementValue <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
	}

    public void SetElement(ElementType type)
    {
        m_ElementType = type;
    }

    public ElementType GetElementType()
    {
        return m_ElementType;
    }

    public void SetPowerElement(Color element)
    {

    }
    public void SetPowerElement(ElementType powerElement)
    {

        float ElementDownSpeed = ElementDefine.GetElementDownSpeed(powerElement, m_ElementType);
        if (m_ElementType == ElementType.FIRE)
        {
            if (powerElement == ElementType.WATER)
            {
                ElementValue -= ElementDownSpeed * Time.deltaTime;
                ElementValue = ElementValue < 0 ? 0 : ElementValue;
            }
        }
        if (m_ElementType == ElementType.GRASS)
        {
            if (powerElement == ElementType.FIRE)
            {
                ElementValue -= ElementDownSpeed * Time.deltaTime;
                ElementValue = ElementValue < 0 ? 0 : ElementValue;
            }
        }
    }
}
