using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RESURRECT : BuffComponent
{
    
    public override IEnumerator CauseBuffEffect(CoroutineTree tree)
    {        
       SetBuffCounters(fixedCounter);                    

       tree.CorQ.CoroutineCompleted();
       yield return null;
        
        
    }

    public override IEnumerator CauseBuffEffectEvent(GameObject Target, CoroutineTree tree)
    {
       
       tree.AddCurrent(Target.GetComponent<HeroLogic>().ResurrectHeroCoroutine(tree)); 
       
       tree.CorQ.CoroutineCompleted();
       yield return null;     

    }

    










    


     
}
