using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Discernment : SkillComponent
{
    public int value = 1;

    bool causeSkillEffect;

    public List<SkillEffect> skillEffects = new List<SkillEffect>();

    public override void RegisterEventEffect()
    {
        UsePassiveSkill();        
    }

    public override void UnRegisterEventEffect()
    {             
        DisablePassiveSkill();
    }

    public override void UsePassiveSkill()
    {       
       UsePassiveSkillLogic();

    }    

    public void DisablePassiveSkill()
    {        
       DisablePassiveSkillLogic();
    }    

    public void UsePassiveSkillLogic()
    {
        foreach(GameObject hero in HeroManager.Instance.AllAlliesList(this.gameObject))
        {
           var skillEffect = hero.AddComponent<AddDebuffCounters>();
           skillEffect.referenceHero = heroLogic.gameObject;
           skillEffect.referenceSkill = this;
           
           skillEffect.value = value;
           skillEffects.Add(skillEffect);

        }
        
    }    

    public void DisablePassiveSkillLogic()
    {        
        foreach(GameObject hero in HeroManager.Instance.AllAlliesList(this.gameObject))
        {
            
            if(hero.GetComponents<AddDebuffCounters>() != null)
            {   
                foreach (var skillEffect in hero.GetComponents<AddDebuffCounters>())
                {   
                    //if(skillEffect != null)
                        if(skillEffect.referenceHero == heroLogic.gameObject && skillEffect.referenceSkill == this)  //even only referenceSkill will do                  
                        {
                            skillEffects.Remove(skillEffect);
                            Destroy(skillEffect);    

                        }                            
                }
            }
             
        }
    }



    
}
