using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillBasicInfo
{
    public string skillName;
    public string scriptName;

    [TextAreaAttribute(1,3)]
    public string description;
    public Sprite thumbnail;
    public int skillCooldown;
    public SkillType skillType;

    public SkillTarget skillTarget = SkillTarget.ENEMY;
    public DragType dragType = DragType.BASIC_ATTACK;
    
}
