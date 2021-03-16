using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STUN : BuffComponent
{
        
    
    int reduceCounter = 1;

    public override IEnumerator CauseBuffEffect(CoroutineTree tree)
    {                
        
        VisualSystem.Instance.DebuffEffectAnimation(this.gameObject, this);  
        SetBuffCounters(fixedCounter);        

       tree.CorQ.CoroutineCompleted();
       yield return null;


    }

    public override IEnumerator CauseBuffEffectEvent(GameObject Target, CoroutineTree tree)
    {       
       BuffEffectAnimation(Target);

       if(this != null)
       tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     

       tree.AddCurrent(EndTurn(Target,tree));           
       
       tree.CorQ.CoroutineCompleted();
       yield return null;

    }

    
    public void BuffEffectAnimation (GameObject Target)
    {          
        VisualSystem.Instance.CreateFloatingText(buffAsset.buffBasicInfo.description, Target, Color.magenta);      
        
    }

     
}
