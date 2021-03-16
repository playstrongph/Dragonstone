using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoilingBlood : SkillComponent
{
  

    int damage = 12;   
    
    public override void RegisterEventEffect()
    {
        heroEvents.e_AfterHeroDealsCrit += BoilingBloodEvent;
    }

    public override void UnRegisterEventEffect()
    {             
        heroEvents.e_AfterHeroDealsCrit -= BoilingBloodEvent;
    }

   

    public void BoilingBloodEvent(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(BoilingBloodEffect(Attacker, Target, tree));
    }

    public IEnumerator BoilingBloodEffect(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        
        if(heroLogic.ChanceOK())
        {
            tree.AddCurrent(FloatingTextCoroutine(heroLogic.gameObject ,tree));   
            tree.AddCurrent(UseSkillAnimationAllAllies(Attacker, Target, tree));

            tree.AddCurrent(IncreaseEnergyAllAlliesCoroutine(Attacker, Target, tree));
            

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

     IEnumerator UseSkillAnimationAllAllies(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        foreach(var allyHero in HeroManager.Instance.AllLivingAlliesList(heroLogic.gameObject))
        {
            base.UseSkillAnimation(Attacker, allyHero, this, 0f);            
        }      

         VisualSystem.Instance.Delay();
        

         tree.CorQ.CoroutineCompleted();
        yield return null;
    }

     IEnumerator IncreaseEnergyAllAlliesCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {

        float energy = 20f;
        foreach(var allyHero in HeroManager.Instance.AllLivingAlliesList(heroLogic.gameObject))
        {
            tree.AddCurrent(IncreaseEnergy(allyHero, energy, tree));
        }      

         tree.CorQ.CoroutineCompleted();
        yield return null;
    }


     

   


    
}
