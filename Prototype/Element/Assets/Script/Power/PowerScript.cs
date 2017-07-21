using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerScript : MonoBehaviour {

    public Material PowerMat;
    public float PowerRange = 3;
    Color ElementColor;
    ElementType PowerElement;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ElementColor = ElementDefine.GetElementColor(PowerElement);

        ElementColor.a = 0.5f;
        PowerMat.color = ElementColor;
    }
    
    public void SetElement(ElementType element)
    {
        PowerElement = element;
    }

    public void UsePower()
    {
        Collider[] PoweredColliders = Physics.OverlapSphere(transform.position, PowerRange);
        List<GameObject> ColliderList = new List<GameObject>();
        foreach (Collider collider in PoweredColliders)
        {
            if (collider.gameObject.tag == "Element" && !collider.isTrigger)
            //if (collider.gameObject.tag != "Ground" && !collider.isTrigger && collider.gameObject.tag != "Player")
            {
                MeshRenderer mr = collider.gameObject.GetComponent<MeshRenderer>();
                ElementScript es = collider.gameObject.GetComponent<ElementScript>();
                if (es)
                {
                    ColliderList.Add(collider.gameObject);
                    //ElementColor = mr.material.color;
                }
                break;
            }
        }

        foreach(GameObject collider in ColliderList)
        {
            ElementScript es = collider.gameObject.GetComponent<ElementScript>();

            es.SetPowerElement(PowerElement);
        }

    }
}
