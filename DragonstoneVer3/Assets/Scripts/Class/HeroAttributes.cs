using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroAttributes
{

    public Attributes attribute;
    public int value;

    public HeroAttributes (Attributes attribute, int value)
    {
        this.attribute = attribute;
        this.value = value;
    }

}
