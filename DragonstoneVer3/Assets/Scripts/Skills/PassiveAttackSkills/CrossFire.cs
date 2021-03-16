using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CrossFire : SkillComponent
{
    // Start is called before the first frame update

    CriticalStrike criticalStrikeSkillEffect;

    float energyAmount = 30;

    bool chance;

    public override void UnRegisterEventEffect()  //this is in case hero dies at pre-attack phase
    {
         if(criticalStrikeSkillEffect != null) 
         Destroy(criticalStrikeSkillEffect);
    }

    public override IEnumerator UsePreAttackPassiveSkill(GameObject Attacker, GameObject Target, CoroutineTree tree)  //Use this if Pre-Atack instead of Post-Attack Trigger
    {
        chance = Attacker.GetComponent<HeroLogic>().ChanceOK();

       if(chance)
       {
          tree.AddCurrent(UsePreAttackPassiveSkillCoroutine(Attacker, Target,tree));
          criticalStrikeSkillEffect = Attacker.AddComponent<CriticalStrike>();         
       }             

        tree.CorQ.CoroutineCompleted();
        yield return null;
       
    }

    public override IEnumerator UsePostAttackPassiveSkill(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
       if(chance)
       {
          tree.AddCurrent(UsePassiveSkillCoroutine(Attacker, Target,tree));
       }            
       if(criticalStrikeSkillEffect != null) 
       {
         Destroy(criticalStrikeSkillEffect);    
       }

       tree.CorQ.CoroutineCompleted();
       yield return null;
       
    }

    IEnumerator UsePassiveSkillCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
      int debuffCounters = 2;

      tree.AddCurrent(AnimationDelay(tree));

      tree.AddCurrent(AddDebuff(Attacker, Target, BuffName.SLOW, debuffCounters, tree));   

      tree.CorQ.CoroutineCompleted(); 
      yield return null;
    }

    IEnumerator UsePreAttackPassiveSkillCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
      
       VisualSystem.Instance.CreateFloatingText(this.skillAsset.skillBasicInfo.skillName, Attacker, Color.yellow);   
       VisualSystem.Instance.Delay(); 

       tree.CorQ.CoroutineCompleted();
       yield return null;       
       
    }


    IEnumerator IncreaseEnergyCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {

        tree.AddCurrent(IncreaseEnergy(Attacker, energyAmount,tree));
        
        tree.CorQ.CoroutineCompleted();
        yield return null;       
    }

    IEnumerator AnimationDelay(CoroutineTree tree)
    {

        VisualSystem.Instance.Delay();

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    

}
