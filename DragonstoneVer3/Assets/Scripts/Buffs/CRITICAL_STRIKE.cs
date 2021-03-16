using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRITICAL_STRIKE : BuffComponent
{
   
    int reduceCounter = 1;
   
   
    public override void RegisterEventEffect()
    {        
       heroEvents.e_AfterHeroDealsCrit += BuffEventEffect;
    }

    public override void UnRegisterEventEffect ()
    {       
       heroEvents.e_AfterHeroDealsCrit -= BuffEventEffect;
    }
    
    public void BuffEventEffect(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(CauseBuffEffectEvent(Attacker, Target, tree));
    }


    public override IEnumerator CauseBuffEffectEvent(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {               
        if(this != null) // In case buff is destroyed
        tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     

        tree.CorQ.CoroutineCompleted();
        yield return null;
        
    }

   







    



     
}
