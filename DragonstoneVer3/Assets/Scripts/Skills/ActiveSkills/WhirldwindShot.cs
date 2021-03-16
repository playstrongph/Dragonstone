using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WhirlwindShot : SkillComponent
{
     GameObject Attacker, Target;

    CoroutineTree tree = new CoroutineTree();

    public override void UseSkill(GameObject Attacker, GameObject Target)
    {
        this.Attacker = Attacker;
        this.Target = Target;

         tree.AddRoot(UseSkillLogic());
         tree.Start();
       
    }

     public IEnumerator UseSkillLogic()
    {
        tree.AddCurrent(UseSkillCoroutine(Attacker, Target));    

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }
    

    IEnumerator UseSkillCoroutine(GameObject Attacker, GameObject Target)
    {                  
        //Active Skills Mandatory
       tree.AddCurrent(UseSkillPreviewCoroutine(Attacker, Target));         
       //tree.AddCurrent(UseSkillAnimationCoroutine(Attacker, Target));
       
       //Main
       tree.AddCurrent(UseSkillAnimationAllEnemies(Attacker, Target));
      
       tree.AddCurrent(DealAttackDamageToAllEnemiesCoroutine(Attacker, Target));  
       tree.AddCurrent(DecreaseAllEnemiesEnergyCoroutine(Attacker, Target));  
     
        //MANDATORY 
       tree.AddCurrent(ResetSkillCooldownAfterUse(Attacker, Target,tree));  
       tree.AddCurrent(TurnManager.Instance.EndTurn(Attacker, tree));      
       tree.CorQ.CoroutineCompleted();
       yield return null;
    }

    //SKill Coroutines
   
    IEnumerator DecreaseAllEnemiesEnergyCoroutine(GameObject Attacker, GameObject Target)
    {
        float energyDecrease = 15f;
        foreach(var enemyHero in HeroManager.Instance.AllLivingEnemiesList(heroLogic.gameObject))
        {
            
            tree.AddCurrent(DecreaseEnergy(enemyHero, energyDecrease ,tree));
        }        

        VisualSystem.Instance.Delay();
       
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator DealAttackDamageToAllEnemiesCoroutine(GameObject Attacker, GameObject Target)
    {
        int damage = heroLogic.Attack;
        foreach(var enemyHero in HeroManager.Instance.AllLivingEnemiesList(heroLogic.gameObject))
        {
            tree.AddCurrent(DealDamage(Attacker, enemyHero, damage, tree));
        }        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }


    IEnumerator UseSkillAnimationCoroutine(GameObject Attacker, GameObject Target)
    {
        base.UseSkillAnimation(Attacker, Target, this);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator UseSkillAnimationAllEnemies(GameObject Attacker, GameObject Target)
    {
        foreach(var enemyHero in HeroManager.Instance.AllLivingEnemiesList(heroLogic.gameObject))
        {
            base.UseSkillAnimation(Attacker, enemyHero, this, 0f);            
        }      

         VisualSystem.Instance.Delay();
         VisualSystem.Instance.Delay();         

         tree.CorQ.CoroutineCompleted();
        yield return null;
    }


    IEnumerator UseSkillPreviewCoroutine(GameObject Attacker, GameObject Target)
    {
        base.UseSkillPreview(Attacker, Target);


        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

}
