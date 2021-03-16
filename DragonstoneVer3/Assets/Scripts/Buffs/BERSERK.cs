using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BERSERK : BuffComponent
{
    
    int reduceCounter = 1;
    GameObject Attacker;
    GameObject Target;

    public override void RegisterEventEffect()
    {
        
        ///<TODO>
        heroEvents.e_BeforeHeroAttacks += BuffEventEffect;

    }

    public override void UnRegisterEventEffect()
    {

        ///<TODO>
        heroEvents.e_BeforeHeroAttacks -= BuffEventEffect;

    }


    public void BuffEventEffect(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {       
        tree.AddCurrent(CauseBuffEffectEvent(Attacker, Target,tree));

    }


    public override IEnumerator CauseBuffEffectEvent(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {       
        if(this != null) // In case buff is destroyed
        {
            VisualSystem.Instance.DebuffEffectAnimation(Attacker, this);
            VisualSystem.Instance.CreateFloatingText(buffAsset.buffBasicInfo.description, Attacker, Color.magenta);   
             tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }
     
}
