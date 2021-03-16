using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GiganticRoar : SkillComponent
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
       tree.AddCurrent(UseSkillAnimationAllEnemies(Attacker, Target));
       
       //Main
       tree.AddCurrent(InflictDebuffsToAllEnemiesCoroutine(Attacker, Target));       
       tree.AddCurrent(DecreaseAllEnemiesEnergyCoroutine(Attacker, Target));  

       tree.AddCurrent(UseSkillAnimationAllAllies(Attacker, Target));
       tree.AddCurrent(GrantAllAlliesHasteCoroutine(Attacker, Target));       

      

       
        //MANDATORY 
       tree.AddCurrent(ResetSkillCooldownAfterUse(Attacker, Target,tree));  
       tree.AddCurrent(TurnManager.Instance.EndTurn(Attacker, tree));      
       tree.CorQ.CoroutineCompleted();
       yield return null;
    }

    //SKill Coroutines    


    IEnumerator DecreaseAllEnemiesEnergyCoroutine(GameObject Attacker, GameObject Target)
    {
        float energyDecrease = 75f;
        foreach(var enemyHero in HeroManager.Instance.AllLivingEnemiesList(heroLogic.gameObject))
        {
            
            tree.AddCurrent(DecreaseEnergy(enemyHero, energyDecrease,tree));
        }        

        VisualSystem.Instance.Delay();
       
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator InflictDebuffsToAllEnemiesCoroutine(GameObject Attacker, GameObject Target)
    {
        foreach(var enemyHero in HeroManager.Instance.AllLivingEnemiesList(heroLogic.gameObject))
        {
            tree.AddCurrent(AddDebuff(Attacker, enemyHero, BuffName.DEFENSE_BREAK, 2, tree));
            tree.AddCurrent(AddDebuff(Attacker, enemyHero, BuffName.ATTACK_DOWN, 2, tree));
        }        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator GrantAllAlliesHasteCoroutine(GameObject Attacker, GameObject Target)
    {
        foreach(var allyHero in HeroManager.Instance.AllLivingAlliesList(heroLogic.gameObject))
        {
            tree.AddCurrent(AddBuff(Attacker, allyHero, BuffName.HASTE, 2, tree));            
        }        

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

         tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator UseSkillAnimationAllAllies(GameObject Attacker, GameObject Target)
    {
        foreach(var allyHero in HeroManager.Instance.AllLivingAlliesList(heroLogic.gameObject))
        {
            base.UseSkillAnimation(Attacker, allyHero, this, 0f);            
        }      

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
