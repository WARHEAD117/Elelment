using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour {

	public GameObject ThunderSpawn;

	float time = 0f;

	void Awake(){
		ThunderSpawn.SetActive (false);
	}

	void Update()
	{
		time += Time.deltaTime;
		if(time >= 5)
		{
			ThunderSpawn.SetActive (true);
			if (time >= 8) {
				ThunderSpawn.SetActive (false);
				time = 0;
			}
		}
	}
}
