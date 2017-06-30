using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodScript : MonoBehaviour {

    public GameObject RodSensor;
    public Material RodMaterial;

    Color ElementColor = Color.white;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] RodSensorColliders = Physics.OverlapSphere(RodSensor.transform.position, 3.0f);
        List<GameObject> ColliderList = new List<GameObject>();
        foreach (Collider collider in RodSensorColliders)
        {
            if (collider.gameObject.tag != "Ground" && !collider.isTrigger && collider.gameObject.tag != "Player")
            {
                MeshRenderer mr = collider.gameObject.GetComponent<MeshRenderer>();
                if(mr)
                {
                    ColliderList.Add(collider.gameObject);
                    ElementColor = mr.material.color;
                }
                break;
            }
        }

        if(ColliderList.Count > 0)
        {
            float minDis = 100.0f;
            GameObject closestObj = ColliderList[0];
            foreach (GameObject collider in ColliderList)
            {
                float dis = Vector3.Distance(collider.transform.position, RodSensor.transform.position);
                if (dis < minDis)
                {
                    minDis = dis;
                    closestObj = collider;
                }
            }

            MeshRenderer mr = closestObj.GetComponent<MeshRenderer>();
            if (mr)
            {
                ElementColor = mr.material.color;
            }
        }
        else
        {
            ElementColor = Color.white;
        }

        RodMaterial.color = ElementColor;


    }

    public Color GetElementColor()
    {
        return ElementColor;
    }
}
