using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContaminatedBlood : SkillComponent
{
  

    int damage = 12;   
    

   

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
        foreach(GameObject enemyHero in HeroManager.Instance.AllEnemiesList(this.gameObject))
        {
        
           //Register effect
           enemyHero.GetComponent<HeroLogic>().heroEvents.e_AfterHeroDealsCrit += ContaminatedBloodEvent;

        }
        
    }    

    public void DisablePassiveSkillLogic()
    {        
        foreach(GameObject enemyHero in HeroManager.Instance.AllEnemiesList(this.gameObject))
        {
            
            
             enemyHero.GetComponent<HeroLogic>().heroEvents.e_AfterHeroDealsCrit -= ContaminatedBloodEvent;
             
             Debug.Log("Unregister ContaminatedBlood");
             
        }
    }

    public void ContaminatedBloodEvent(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(ContaminatedBloodEffect(Attacker, Target, tree));
    }

    public IEnumerator ContaminatedBloodEffect(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        
        Debug.Log("ContaminatedBlood Skill Effect");
        
        if(heroLogic.ChanceOK() && Target.GetComponent<HeroLogic>() == heroLogic)
        {
            tree.AddCurrent(FloatingTextCoroutine(heroLogic.gameObject ,tree));    

            tree.AddCurrent(UseSkillAnimationCoroutine(heroLogic.gameObject, Attacker,tree));
            
           

            tree.AddCurrent(DealDamage(heroLogic.gameObject, Attacker, damage,tree));    
            tree.AddCurrent(AddDebuff(heroLogic.gameObject, Attacker, BuffName.POISON, 1 ,tree));                    
            tree.AddCurrent(AddDebuff(heroLogic.gameObject, Attacker, BuffName.DEFENSE_BREAK, 1 ,tree));

            //  tree.AddCurrent(InflictPoisonDebuffCoroutine(heroLogic.gameObject, Attacker,tree));
            //  tree.AddCurrent(InflictDefenseBreakDebuffCoroutine(heroLogic.gameObject, Attacker,tree));

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

     IEnumerator UseSkillAnimationCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        base.UseSkillAnimation(Attacker, Target, this); //pure visual

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    //  IEnumerator InflictPoisonDebuffCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    // {
    //     int poisonCounters = 1;

    //     tree.AddCurrent(AddDebuff(Attacker, Target, BuffName.POISON, poisonCounters,tree));

    //     tree.CorQ.CoroutineCompleted();
    //     yield return null;
    // }

    // IEnumerator InflictDefenseBreakDebuffCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    // {
    //     int defenseBreakCounters = 1;

    //     tree.AddCurrent(AddDebuff(Attacker, Target, BuffName.DEFENSE_BREAK, defenseBreakCounters,tree));

    //     tree.CorQ.CoroutineCompleted();
    //     yield return null;
    // }



    
}
