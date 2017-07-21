using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodScript : MonoBehaviour {

    public GameObject RodSensor;
    public Material RodMaterial;

    Color ElementColor = Color.white;
    ElementType RodElement;

    public bool CanGetElement { get; set; }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] RodSensorColliders = Physics.OverlapSphere(RodSensor.transform.position, 3.0f);
        List<GameObject> ColliderList = new List<GameObject>();
        foreach (Collider collider in RodSensorColliders)
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
            ElementScript es = closestObj.gameObject.GetComponent<ElementScript>();
            if (es)
            {
                CanGetElement = true;
                RodElement = es.GetElementType();
                ElementColor = ElementDefine.GetElementColor(RodElement);

                DrawLine(RodSensor.transform.position, closestObj.transform.position, ElementColor, ElementColor);
            }
        }
        else
        {
            CanGetElement = false;
            RodElement = ElementType.NONE;
            ElementColor = ElementDefine.GetElementColor(RodElement);
            RemoveLine();
        }

        RodMaterial.color = ElementColor;


    }

    public Color GetElementColor()
    {
        return ElementColor;
    }
    public ElementType GetElement()
    {
        return RodElement;
    }

    void RemoveLine()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer)
        {
            lineRenderer.enabled = false;
        }
    }

    void DrawLine(Vector3 start, Vector3 end, Color startColor, Color endColor)
    {
        GameObject aimLine = transform.gameObject;
        //myLine.transform.position = start;
        LineRenderer lineRenderer = aimLine.GetComponent<LineRenderer>();
        if (!lineRenderer)
        {
            aimLine.AddComponent<LineRenderer>();
            lineRenderer = aimLine.GetComponent<LineRenderer>();
        }
        lineRenderer.enabled = true;
        lineRenderer.numCornerVertices = 2;
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
