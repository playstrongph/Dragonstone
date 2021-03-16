using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffImmunity : SkillEffect
{
    
    
    public void UseSkillEffect(GameObject Attacker, GameObject Target)
    {
        VisualSystem.Instance.CreateFloatingText("IMMUNE", Target, Color.yellow);
        //skillanimation
    }
}
