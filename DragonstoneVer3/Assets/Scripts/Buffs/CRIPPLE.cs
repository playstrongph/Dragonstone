using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRIPPLE : BuffComponent
{
    int reduceCounter = 1;    

    GameObject Attacker;

    GameObject Target;

    public override void RegisterEventEffect()
    {
        heroEvents.e_AfterHeroDealsCrippleAttack += BuffEventEffect;
    }

    public override void UnRegisterEventEffect ()
    {
        heroEvents.e_AfterHeroDealsCrippleAttack -= BuffEventEffect;
    }


    public void BuffEventEffect(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(CauseBuffEffectEvent(Attacker, Target, tree));
    }

    public override IEnumerator CauseBuffEffectEvent(GameObject Attacker, GameObject Target, CoroutineTree tree)    
    {  
        
        if(this != null)
        tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     
        
        tree.CorQ.CoroutineCompleted();
        yield return null;

        
    }

    





     
}
