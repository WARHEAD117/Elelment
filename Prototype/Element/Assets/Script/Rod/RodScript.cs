using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodScript : MonoBehaviour {

    public GameObject RodSensor;
    public MeshRenderer RodRenderer;
    public GameObject RodForward;
    Material RodMaterial;

    public float RodSensorAngle = 90;

    Color ElementColor = Color.white;
    ElementType RodElement;

    public bool CanGetElement { get; set; }

    // Use this for initialization
    void Start () {
        //RodMaterial = this.GetComponent<MeshRenderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        Collider[] RodSensorColliders = Physics.OverlapSphere(RodSensor.transform.position, 2.0f);
        List<Collider> ColliderList = new List<Collider>();
        foreach (Collider collider in RodSensorColliders)
        {
            if (collider.gameObject.tag == "Element" && !collider.isTrigger)
            //if (collider.gameObject.tag != "Ground" && !collider.isTrigger && collider.gameObject.tag != "Player")
            {
                MeshRenderer mr = collider.gameObject.GetComponent<MeshRenderer>();
                ElementScript es = collider.gameObject.GetComponent<ElementScript>();
                if (es)
                {
                    {
                        //DrawLine(RodSensor.transform.position, hitPoint, ElementColor, ElementColor);
                        ColliderList.Add(collider);
                        //ElementColor = mr.material.color;
                    }
                }
                else
                {
                    ElementScript parent_es = collider.transform.parent.gameObject.GetComponent<ElementScript>();
                    if(parent_es)
                    {
                        ColliderList.Add(collider);
                    }
                }
            }
        }

        if(ColliderList.Count > 0)
        {
            float minDis = 100.0f;
            Collider closestObj = ColliderList[0];
            foreach (Collider collider in ColliderList)
            {
                Vector3 hitPoint = collider.ClosestPoint(RodSensor.transform.position);
                hitPoint.y = RodSensor.transform.position.y;
                float dis = Vector3.Distance(hitPoint, RodSensor.transform.position);
                if (dis < minDis)
                {
                    minDis = dis;
                    closestObj = collider;
                }
            }

            MeshRenderer mr = closestObj.gameObject.GetComponent<MeshRenderer>();
            ElementScript es = closestObj.gameObject.GetComponent<ElementScript>();
            if (es)
            {
                CanGetElement = true;
                RodElement = es.GetElementType();
                ElementColor = ElementDefine.GetElementColor(RodElement);

                Vector3 hitPoint = closestObj.ClosestPoint(RodSensor.transform.position);
                DrawLine(RodSensor.transform.position, hitPoint, ElementColor, ElementColor);
            }
            else
            {
                ElementScript parent_es = closestObj.transform.parent.gameObject.GetComponent<ElementScript>();
                if (parent_es)
                {
                    CanGetElement = true;
                    RodElement = parent_es.GetElementType();
                    ElementColor = ElementDefine.GetElementColor(RodElement);

                    Vector3 hitPoint = closestObj.ClosestPoint(RodSensor.transform.position);
                    hitPoint = closestObj.transform.position;
                    DrawLine(RodSensor.transform.position, hitPoint, ElementColor, ElementColor);
                }
            }
        }
        else
        {
            CanGetElement = false;
            RodElement = ElementType.NONE;
            ElementColor = ElementDefine.GetElementColor(RodElement);
            RemoveLine();
        }

        RodRenderer.material.color = ElementColor;
        //RodMaterial.color = ElementColor;


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
