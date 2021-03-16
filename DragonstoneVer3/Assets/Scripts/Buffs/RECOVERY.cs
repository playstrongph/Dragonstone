using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RECOVERY : BuffComponent
{
        
    
    public int value = 5;  //heal Value

    int reduceCounter = 1;

    GameObject Target;

    

    public override void RegisterEventEffect()
    {

        heroEvents.e_AfterHeroStartTurn += BuffEventEffect;      

    }

    public override void UnRegisterEventEffect ()
    { 

        heroEvents.e_AfterHeroStartTurn -= BuffEventEffect;      

    }

    public void BuffEventEffect(GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(CauseBuffEffectEvent(Target, tree));
    }



    public override IEnumerator CauseBuffEffectEvent(GameObject Target, CoroutineTree tree)
    {
        if(this != null)
        {
            tree.AddCurrent(Heal(Target, value, tree));        
            //BuffEffectAnimation(Target);
           
            tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     

        }

        tree.CorQ.CoroutineCompleted();
        yield return null;
        
    }

    

    

     
}
