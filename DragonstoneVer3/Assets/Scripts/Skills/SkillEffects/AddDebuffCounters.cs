using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDebuffCounters : SkillEffect
{
    
    //PASSIVE SKILL
    public int value;    
    GameObject Attacker;

    int buffCounter;

   

    public int UseSkillEffect(GameObject Attacker, int buffCounter)
    {
        if(referenceHero.GetComponent<HeroLogic>().ChanceOK())                
        {
            //Debug.Log("Reference Hero is: " +referenceHero.name);
            VisualSystem.Instance.CreateFloatingText(referenceSkill.skillAsset.skillBasicInfo.skillName, referenceHero, Color.yellow);
            return buffCounter += value;
            
        }
        
        else
        return buffCounter;
    }

    
}
