using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {

    public GameObject SwordObject;
    public MeshRenderer PowerRenderer;

    float AttackValue = 0;

    ElementType SwordElement = ElementType.NONE;

    Transform boundTrans;

    public PlayerScript player;
    // Use this for initialization
    void Start () {
        SwordElement = ElementType.NONE;

        SwordObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        SwordElement = player.GetElementType();
        boundTrans = SwordObject.transform;

        if (AttackValue == 0)
            return;

        PowerRenderer.material.color = ElementDefine.GetElementColor(SwordElement);
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
                else
                {
                    ElementScript parent_es = collider.transform.parent.gameObject.GetComponent<ElementScript>();
                    if (parent_es)
                    {
                        ColliderList.Add(parent_es.gameObject);
                    }
                }
            }
        }

        foreach (GameObject collider in ColliderList)
        {
            ElementScript es = collider.gameObject.GetComponent<ElementScript>();

            //es.SetPowerElement(PowerElement);

            float curElementValue = player.GetElementValue();

            es.SetPowerElement(SwordElement, Mathf.Min(curElementValue, player.GetShootSpend()));
            player.ReleaseElement(Mathf.Min(curElementValue, player.GetShootSpend()));
            AttackValue = 0;
        }
    }
    
    public float GetSwordValue()
    {
        return AttackValue;
    }

    public void ActiveSword()
    {
        SwordObject.SetActive(true);
        AttackValue = player.GetSwordAtk();
    }

    public void DeactiveSword()
    {
        SwordObject.SetActive(false);
        AttackValue = 0;
    }
}
