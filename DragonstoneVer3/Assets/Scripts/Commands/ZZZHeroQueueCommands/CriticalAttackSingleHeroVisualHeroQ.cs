using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CriticalAttackSingleHeroVisualHeroQ : Command
{
    GameObject Attacker;
    GameObject Target;

    //Default, in case none specified
    //float animdelay = 2f;

    public CriticalAttackSingleHeroVisualHeroQ(GameObject Attacker, GameObject Target)
    {
        this.Attacker = Attacker;
        this.Target = Target;
    }

    public override void StartCommandExecution()
    {        
        if (Attacker != null && Target != null)
        {   
            // Attacker.GetComponent<HeroAttackVisual>().AttackTargetCriticalStrikeHeroQ(Target);            
        }                    
    }

    
}
