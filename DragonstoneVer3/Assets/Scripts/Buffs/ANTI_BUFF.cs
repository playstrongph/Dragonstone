using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANTI_BUFF : BuffComponent
{
    
    int reduceCounter = 1;
    GameObject Target;

    
    public override IEnumerator CauseBuffEffectEvent(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {       
        if(this != null) // In case buff is destroyed
        {
            VisualSystem.Instance.DebuffEffectAnimation(Target, this);
            VisualSystem.Instance.CreateFloatingText(buffAsset.buffBasicInfo.description, Target, Color.magenta);  
            
            tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;
        
    }

    
   


    

     
}
