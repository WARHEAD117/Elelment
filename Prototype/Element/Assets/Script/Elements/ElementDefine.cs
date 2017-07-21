using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ElementDefine {

	public static Color GetElementColor(ElementType element)
    {
        Color elementColor = Color.black;

        switch(element)
        {
            case ElementType.FIRE:
                elementColor = new Color(1, 0.67f, 0.17f);
                break;
            case ElementType.WATER:
                elementColor = new Color(0.45f, 0.70f, 1);
                break;
            case ElementType.GRASS:
                elementColor = new Color(0.16f, 0.80f, 0.42f);
                break;
            case ElementType.ICE:
                elementColor = Color.white;
                break;
            case ElementType.THUNDER:
                elementColor = new Color(0.86f, 0.80f, 0.0f);
                break;
            case ElementType.NONE:
                elementColor = Color.black;
                break;

        }

        return elementColor;
    }

    public static float GetElementDownSpeed(ElementType power, ElementType element)
    {
        float speed = 100;

        switch (element)
        {
            case ElementType.FIRE:
                speed = 200;
                break;
            case ElementType.WATER:
                speed = 100;
                break;
            case ElementType.GRASS:
                speed = 100;
                break;
            case ElementType.ICE:
                speed = 50;
                break;
            case ElementType.THUNDER:
                break;
            case ElementType.NONE:
                speed = 0;
                break;

        }

        return speed;
    }
}
