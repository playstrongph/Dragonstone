using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POISON : BuffComponent
{
        
    
    int value = 5; 
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
          VisualSystem.Instance.CreateFloatingText(buffAsset.buffBasicInfo.description, Target, Color.magenta);
          VisualSystem.Instance.DebuffEffectAnimation(Target, this);

          tree.AddCurrent(DealDamageNoAttacker(Target, value,tree));  
                  
          tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     

        }     

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    
     
}
