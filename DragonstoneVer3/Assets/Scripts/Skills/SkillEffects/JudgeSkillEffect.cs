using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeSkillEffect : SkillEffect
{
    
    //PASSIVE SKILL
    
   
    public IEnumerator UseSkillEffect(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if(referenceHero.GetComponent<HeroLogic>().ChanceOK())                
        {
         
            VisualSystem.Instance.CreateFloatingText(referenceSkill.skillAsset.skillBasicInfo.skillName, referenceHero, Color.yellow);            
            
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;
        
        
    }

    
}
