using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public PlayerScript MyPlayer;
    public Text ElementValueText;
    public Text CoinText;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
    void Update()
    {
        string elementType = MyPlayer.GetElementType().ToString();
        string element = ((int)MyPlayer.GetElementValue()).ToString();
        string maxElement = MyPlayer.GetMaxElementValue().ToString();
        ElementValueText.text = "Element:" + elementType + "-" + element + "/" + maxElement;

        string coinCount = MyPlayer.GetCoinCount().ToString();
        CoinText.text = "Coin:" + coinCount;
	}
}
