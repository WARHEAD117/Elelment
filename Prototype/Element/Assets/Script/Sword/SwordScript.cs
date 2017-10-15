using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {


    public MeshRenderer PowerRenderer;

    float AttackValue = 0;

    ElementType PowerElement;

    public Transform boundTrans;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        PowerRenderer.material.color = ElementDefine.GetElementColor(PowerElement);

        Collider[] PoweredColliders = Physics.OverlapBox(boundTrans.position, boundTrans.localScale);
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
            }
        }

        foreach (GameObject collider in ColliderList)
        {
            ElementScript es = collider.gameObject.GetComponent<ElementScript>();

            //es.SetPowerElement(PowerElement);
            es.SetPowerElement(PowerElement, AttackValue);
            AttackValue = 0;
        }
    }

    public void SetElement(ElementType element, float eValue)
    {
        PowerElement = element;
        AttackValue = eValue;
    }

    public float GetSwordValue()
    {
        return AttackValue;
    }
}
