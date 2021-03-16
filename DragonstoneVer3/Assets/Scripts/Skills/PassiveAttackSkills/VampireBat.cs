using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VampireBat : SkillComponent
{
    // Start is called before the first frame update

    CriticalStrike criticalStrikeSkillEffect;

    

    bool chance;    

    public override void UnRegisterEventEffect()  //this is in case hero dies at pre-attack phase
    {
         if(criticalStrikeSkillEffect != null) 
         Destroy(criticalStrikeSkillEffect);        
    }
    
    
    public override IEnumerator UsePreAttackPassiveSkill(GameObject Attacker, GameObject Target, CoroutineTree tree)  //Use this if Pre-Atack instead of Post-Attack Trigger
    {
        chance = Attacker.GetComponent<HeroLogic>().ChanceOK();

       if(chance)
       {         
          criticalStrikeSkillEffect = Attacker.AddComponent<CriticalStrike>();         
       }             

        tree.CorQ.CoroutineCompleted();
        yield return null;
       
    }

    public override IEnumerator UsePostAttackPassiveSkill(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
            
       if(criticalStrikeSkillEffect != null) 
       {
         Destroy(criticalStrikeSkillEffect);    
       }

       if(chance)
       {
           VisualSystem.Instance.CreateFloatingText(this.skillAsset.skillBasicInfo.skillName, Attacker, Color.yellow);   
           VisualSystem.Instance.Delay(); 
           tree.AddCurrent(LifeSteal(Attacker, Target, tree));

       }

       tree.CorQ.CoroutineCompleted();
       yield return null;
       
    }

    

   


    

}
