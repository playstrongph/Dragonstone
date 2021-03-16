using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chaser : SkillComponent
{

    float decreaseEnergy = 20f;
    int debuffCounters = 1;

    public override void RegisterEventEffect()
    {   
        heroEvents.e_AfterHeroEndTurn += UsePassiveSkillEffect;        
    }

    public override void UnRegisterEventEffect()
    {        
        heroEvents.e_AfterHeroEndTurn -= UsePassiveSkillEffect;     
    }

    public void UsePassiveSkillEffect(GameObject Attacker, CoroutineTree tree)
    {
        if(heroLogic.ChanceOK())
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
        
        var enemies = HeroManager.Instance.AllLivingEnemiesList(heroLogic.gameObject);
        var enemyTarget = enemies[UnityEngine.Random.Range(0, enemies.Count)];

       //Mandatory
       tree.AddCurrent(FloatingTextCoroutine(heroLogic.gameObject ,tree));           
       tree.AddCurrent(UseSkillAnimationCoroutine(Attacker, enemyTarget,tree));


        tree.AddCurrent(DecreaseEnergy(enemyTarget, decreaseEnergy, tree));       
        tree.AddCurrent(AddDebuff(Attacker, enemyTarget, BuffName.DEFENSE_BREAK, debuffCounters, tree));       
       
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

      IEnumerator UseSkillAnimationCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        base.UseSkillAnimation(Attacker, Target, this); //pure visual

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
