using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerScript : MonoBehaviour {

    Material PowerMat;
    public float PowerRange = 3;
    Color ElementColor;
    ElementType PowerElement;
    public Transform initTransform;

    public GameObject PowerBallPrefab;
    public float ShootSpeed = 50;
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

    public void UsePower(float shootValue)
    {
        GameObject newPowerBall = GameObject.Instantiate(PowerBallPrefab, initTransform.position, Quaternion.identity);
        PowerBall powerBallScript = newPowerBall.GetComponent<PowerBall>();
        powerBallScript.SetDirection(this.transform.forward);
        powerBallScript.SetElement(PowerElement, shootValue);
        powerBallScript.SetSpeed(ShootSpeed);
    }
}
