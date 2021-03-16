using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Judge : SkillComponent
{
  

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
        foreach(GameObject enemyHero in HeroManager.Instance.AllEnemiesList(this.gameObject))
        {
        //    var skillEffect = enemyHero.AddComponent<JudgeSkillEffect>();
        //    skillEffect.referenceHero = heroLogic.gameObject;
        //    skillEffect.referenceSkill = this;          
        //    skillEffects.Add(skillEffect);

           //Register effect
           enemyHero.GetComponent<HeroLogic>().heroEvents.e_AfterHeroDealsCrit += JudgeEvent;

        }
        
    }    

    public void DisablePassiveSkillLogic()
    {        
        foreach(GameObject enemyHero in HeroManager.Instance.AllEnemiesList(this.gameObject))
        {
            
            // if(enemyHero.GetComponents<JudgeSkillEffect>() != null)
            // {   
            //     foreach (var skillEffect in enemyHero.GetComponents<JudgeSkillEffect>())
            //     {   
            //         //if(skillEffect != null)
            //             if(skillEffect.referenceHero == heroLogic.gameObject && skillEffect.referenceSkill == this)  //even only referenceSkill will do                  
            //             {
            //                 skillEffects.Remove(skillEffect);
            //                 Destroy(skillEffect);    

            //             }                            
            //     }
            // }

             enemyHero.GetComponent<HeroLogic>().heroEvents.e_AfterHeroDealsCrit -= JudgeEvent;
             
             Debug.Log("Unregister Judge");
             
        }
    }

    public void JudgeEvent(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(JudgeSkillEffect(Attacker, Target, tree));
    }

    public IEnumerator JudgeSkillEffect(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        
        Debug.Log("Judge Skill Effect");
        
        if(heroLogic.ChanceOK())
        {
            tree.AddCurrent(FloatingTextCoroutine(heroLogic.gameObject ,tree));    

            tree.AddCurrent(UseSkillAnimationCoroutine(heroLogic.gameObject, Attacker,tree));
            
            tree.AddCurrent(DealDamage(heroLogic.gameObject, Attacker, heroLogic.gameObject.GetComponent<HeroLogic>().Attack,tree));

            tree.AddCurrent(IncreaseArmor(heroLogic.gameObject, 10,tree));

            tree.AddCurrent(IncreaseEnergy(heroLogic.gameObject, 10f, tree));


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



    
}
