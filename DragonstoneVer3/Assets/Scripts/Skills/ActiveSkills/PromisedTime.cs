using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PromisedTime : SkillComponent
{
    
    CoroutineTree tree = new CoroutineTree();

    GameObject Attacker, Target;
 
      public override void UseSkill(GameObject Attacker, GameObject Target)
    {   
         //UseSkillLogic();         
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
       tree.AddCurrent(UseSkillAnimationAllEnemies(Attacker, Target));

       tree.AddCurrent(DealAttackDamageToAllEnemiesCoroutine(Attacker, Target));  
       tree.AddCurrent(InflictStunDebuffCoroutine(Attacker, Target));        

        ///MANDATORY 
       tree.AddCurrent(ResetSkillCooldownAfterUse(Attacker, Target,tree));  
       tree.AddCurrent(TurnManager.Instance.EndTurn(Attacker, tree));      
       tree.CorQ.CoroutineCompleted();
       yield return null;
    }

     
    
    //SKill Coroutines
    IEnumerator InflictStunDebuffCoroutine(GameObject Attacker, GameObject Target)
    {
        int counter = 1;

        tree.AddCurrent(AddDebuff(Attacker, Target, BuffName.STUN, counter,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator DealAttackDamageToAllEnemiesCoroutine(GameObject Attacker, GameObject Target)
    {
        foreach(var enemyHero in HeroManager.Instance.AllLivingEnemiesList(heroLogic.gameObject))
        {
            tree.AddCurrent(DealDamage(Attacker, enemyHero, Attacker.GetComponent<HeroLogic>().Attack,tree));            
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }


    IEnumerator UseSkillPreviewCoroutine(GameObject Attacker, GameObject Target)
    {
        base.UseSkillPreview(Attacker, Target); //pure visual
       tree.CorQ.CoroutineCompleted();
       yield return null;
    }    

     IEnumerator UseSkillAnimationCoroutine(GameObject Attacker, GameObject Target)
    {
        base.UseSkillAnimation(Target, Target, this); //pure visual

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




}
