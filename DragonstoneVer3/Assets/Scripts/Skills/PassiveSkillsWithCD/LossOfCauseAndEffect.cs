using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LossOfCauseAndEffect : SkillComponent
{
    // Start is called before the first frame update

      
    int debuffCount;

   

    
    public override void RegisterEventEffect()
    {   
        heroEvents.e_AfterHeroStartTurn += UsePassiveSkillEffect;        
    }

    public override void UnRegisterEventEffect()
    {        
        heroEvents.e_AfterHeroStartTurn -= UsePassiveSkillEffect;     
    }

    public void UsePassiveSkillEffect(GameObject Attacker, CoroutineTree tree)
    {
        if(currCoolDown == 0)
        {           
           tree.AddCurrent(UsePassiveSkillLogic(Attacker, tree));
        }        
        
    }

    

    public IEnumerator UsePassiveSkillLogic(GameObject Attacker, CoroutineTree tree)
    {
        tree.AddCurrent(UsePassiveSkillCoroutine(Attacker,tree));   

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    



    IEnumerator UsePassiveSkillCoroutine(GameObject Attacker, CoroutineTree tree)
    {
                
        tree.AddCurrent(FloatingTextCoroutine(Attacker,tree));       

        tree.AddCurrent(RemoveAllDebuffsCorutine(Attacker,tree));       
       
        tree.AddCurrent(HealAllAlliesCorutine(Attacker,tree));      
         
        //MANDATORY
        tree.AddCurrent(ResetSkillCooldownAfterUse(Attacker,Attacker,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;


    }

    IEnumerator RemoveAllDebuffsCorutine(GameObject Attacker, CoroutineTree tree)
    {
        var allBuffs = Attacker.GetComponents<BuffComponent>();
        var allDebuffs = BuffSystem.Instance.GetAllBuffsOfType(allBuffs, BuffType.DEBUFF);

        debuffCount = allDebuffs.Count;

        tree.AddCurrent(RemoveAllDebuffs(Attacker,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }
   

    IEnumerator HealAllAlliesCorutine(GameObject Attacker,CoroutineTree tree)
    {
        foreach(var ally in HeroManager.Instance.AllLivingAlliesList(Attacker))    
        {
            int healAmount = 10;  

            if(debuffCount<=0)
            {
                tree.AddCurrent(Heal(Attacker, ally, healAmount,tree));
            }
            else
            {
                healAmount += 10*debuffCount;  
                tree.AddCurrent(Heal(Attacker, ally, healAmount,tree));
            }                    
            
        }
        
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator FloatingTextCoroutine(GameObject Attacker, CoroutineTree tree)
    {
        VisualSystem.Instance.CreateFloatingText(this.skillAsset.skillBasicInfo.skillName, Attacker, Color.yellow);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

  
  

   
    

    

    
    
}
