using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sanctuary : SkillComponent
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
       tree.AddCurrent(UseSkillAnimationAllAllies(Attacker, Target));
       
       //Main
       tree.AddCurrent(RemoveAllDebuffsAllAlliesCoroutine(Attacker, Target));        
       tree.AddCurrent(IncreaseAllAlliesEnergyCoroutine(Target));          
       tree.AddCurrent(GiveImmunityToAllAlliesCoroutine(Attacker, Target)); 
       
        //MANDATORY 
       tree.AddCurrent(ResetSkillCooldownAfterUse(Attacker, Target,tree));  
       tree.AddCurrent(TurnManager.Instance.EndTurn(Attacker, tree));      
       tree.CorQ.CoroutineCompleted();
       yield return null;
    }

    //SKill Coroutines
    

    IEnumerator IncreaseAllAlliesEnergyCoroutine(GameObject Target)
    {
        
        float energyValue = 30f;

        foreach(var allyHero in HeroManager.Instance.AllLivingAlliesList(heroLogic.gameObject))
        {            
            tree.AddCurrent(IncreaseEnergy(allyHero,energyValue,tree));
        }        

        VisualSystem.Instance.Delay();
       
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }
    


    IEnumerator RemoveAllDebuffsAllAlliesCoroutine(GameObject Attacker, GameObject Target)
    {
        foreach(var allyHero in HeroManager.Instance.AllLivingAlliesList(heroLogic.gameObject))
        {            
            tree.AddCurrent(RemoveAllDebuffs(allyHero,tree));
        }        

        VisualSystem.Instance.Delay();
       
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator GiveImmunityToAllAlliesCoroutine(GameObject Attacker, GameObject Target)
    {
        foreach(var allyHero in HeroManager.Instance.AllLivingAlliesList(heroLogic.gameObject))
        {
            tree.AddCurrent(AddBuff(Attacker, allyHero, BuffName.IMMUNITY, 3, tree));
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

    IEnumerator UseSkillAnimationAllAllies(GameObject Attacker, GameObject Target)
    {
        foreach(var allyHero in HeroManager.Instance.AllLivingAlliesList(heroLogic.gameObject))
        {
            base.UseSkillAnimation(Attacker, allyHero, this, 0f);            
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
