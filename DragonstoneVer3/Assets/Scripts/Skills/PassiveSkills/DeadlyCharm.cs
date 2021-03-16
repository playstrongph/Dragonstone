using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeadlyCharm : SkillComponent
{
    
    
    public override void RegisterEventEffect()
    {   
        heroEvents.e_AfterHeroAttacks += UsePassiveSkillEffect;        
    }

    public override void UnRegisterEventEffect()
    {        
        heroEvents.e_AfterHeroAttacks -= UsePassiveSkillEffect;     
    }

    public void UsePassiveSkillEffect(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if(currCoolDown == 0)
        {           
           tree.AddCurrent(UsePassiveSkillCoroutine(Attacker, Target, tree));
        }        
        
    }
        
    
    IEnumerator UsePassiveSkillCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        
       tree.AddCurrent(FloatingTextCoroutine(Attacker,tree));            

       tree.AddCurrent(AddDebuffCoroutine(Attacker, Target,tree));   

       tree.AddCurrent(ResetSkillCooldownAfterUse(Attacker,Target, tree));

       tree.CorQ.CoroutineCompleted();
       yield return null;
    }

    
    IEnumerator FloatingTextCoroutine(GameObject Attacker, CoroutineTree tree)
    {
        string thisSkillName = this.skillAsset.skillBasicInfo.skillName.ToString();
        VisualSystem.Instance.CreateFloatingText(thisSkillName, Attacker, Color.yellow);       

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    
    IEnumerator AddDebuffCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(AddDebuffIgnoreImmunity(Attacker, Target, BuffName.SILENCE, 1,tree));
        
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

   
    
}
