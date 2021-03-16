using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New Buff", menuName = "SO/Buffs")]
public class BuffAsset : ScriptableObject
{

    public BuffBasicInfo buffBasicInfo;

	public BuffComponent Use(GameObject target, int buffCounter)
	{
        //Debug.Log("BA Use");
		BuffComponent buffComponent = target.AddComponent(Type.GetType(buffBasicInfo.buffName.ToString())) as BuffComponent;
        buffComponent.buffAsset = this;
        buffComponent.buffCounter = buffCounter;
        
        //new
        buffComponent.newCounters = buffCounter;

        return buffComponent;
	}

}
