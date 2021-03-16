using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackSingleHeroVisualHeroQ : Command
{
    GameObject Attacker;
    GameObject Target;

    //Default, in case none specified
    //float animdelay = 2f;

    public AttackSingleHeroVisualHeroQ(GameObject Attacker, GameObject Target)
    {
        this.Attacker = Attacker;
        this.Target = Target;
    }

    public override void StartCommandExecution()
    {
        
        if (Attacker != null && Target != null)
        {
            
            //if(Attacker.GetComponent<HeroLogic>().criticalStrikeFactor > 1)                     
            // Attacker.GetComponent<HeroAttackVisual>().AttackTargetHeroQ(Target);
        }              
      
    }

    
}
