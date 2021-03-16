using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rider : SkillComponent
{
    bool singleFire;
    public override void RegisterEventEffect()
    {   
        singleFire = true;
        AddPreventFatalDamage();    
        heroEvents.e_AfterHeroPreventsFatalDamage += GainInvincibility;
    }

    public override void UnRegisterEventEffect()
    {           
       
        heroEvents.e_AfterHeroPreventsFatalDamage -= GainInvincibility;
    }
    
    public void AddPreventFatalDamage()
    {       
        if(heroLogic.gameObject.GetComponent<PreventFatalDamageSkillEffect>() != null &&
         heroLogic.gameObject.GetComponent<PreventFatalDamageSkillEffect>().skill == this)
        {
            
        }else
        {
            var skillEffect = heroLogic.gameObject.AddComponent<PreventFatalDamageSkillEffect>();
            skillEffect.skill = this;
        }
        
    }

    public void GainInvincibility(GameObject Target, CoroutineTree tree)
    {
        if(singleFire)
        {
            tree.AddCurrent(AddBuff(Target, Target, BuffName.INVINCIBLE, 1, tree));
            singleFire = false;
        }
    }

    
    
    

    
    
}
