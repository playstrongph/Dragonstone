using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuffBasicInfo
{
    public BuffName buffName;
    public string description;

    [TextAreaAttribute(1,3)]
    public string effectDescription;
    public Sprite thumbnail;
    public BuffType buffType;
}
