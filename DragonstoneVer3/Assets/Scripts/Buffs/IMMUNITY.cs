using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMMUNITY : BuffComponent
{
    int reduceCounter = 1;    

    
   
    public override IEnumerator CauseBuffEffectEvent(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {   
         if(this != null) // In case buff is destroyed
        {
            VisualSystem.Instance.BuffEffectAnimation(Target, this);
            VisualSystem.Instance.CreateFloatingText(buffAsset.buffBasicInfo.description, Target, Color.yellow);                      
            
            tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }
   

     
}
