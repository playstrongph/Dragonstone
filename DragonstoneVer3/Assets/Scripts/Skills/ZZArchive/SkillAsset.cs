using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New Skill", menuName = "SO/Skills")]
public class SkillAsset : ScriptableObject
{
	
    public SkillBasicInfo skillBasicInfo;

	public SkillComponent Use(GameObject source)
	{
		SkillComponent skillComponent = source.AddComponent(Type.GetType(skillBasicInfo.scriptName)) as SkillComponent;
		skillComponent.skillAsset = this;
        return skillComponent;
	}
}
