using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SealMagic : SkillComponent
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
       tree.AddCurrent(UseSkillAnimationCoroutine(Attacker, Target));
       
       tree.AddCurrent(ResetAllSkillCooldownsToMaxCoroutine(Target));  

       tree.AddCurrent(UseSkillAnimationAllEnemies(Attacker, Target));

       tree.AddCurrent(SetAllEnemiesEnergyToValueCoroutine(Attacker, Target));  
       
        //MANDATORY 
       tree.AddCurrent(ResetSkillCooldownAfterUse(Attacker, Target,tree));  
       tree.AddCurrent(TurnManager.Instance.EndTurn(Attacker, tree));      
       tree.CorQ.CoroutineCompleted();
       yield return null;
    }

     
    
    //SKill Coroutines
    IEnumerator ResetAllSkillCooldownsToMaxCoroutine(GameObject Target)
    {
        tree.AddCurrent(ResetAllSkillCooldownsToMax(Target,tree));

        VisualSystem.Instance.Delay(); //0.5secs delay
        
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    


    IEnumerator SetAllEnemiesEnergyToValueCoroutine(GameObject Attacker, GameObject Target)
    {
        foreach(var enemyHero in HeroManager.Instance.AllLivingEnemiesList(heroLogic.gameObject))
        {
            tree.AddCurrent(SetEnergyToValue(enemyHero, 0f,tree));
        }        

        VisualSystem.Instance.Delay();
       
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
