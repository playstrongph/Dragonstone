using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVASION : BuffComponent
{
        
    // public int value = 0;  
    // public int defaultValue = 1;

    int reduceCounter = 1;
   //bool test = false;

   GameObject Attacker, Target;

   

    public override void RegisterEventEffect()
    {
        heroEvents.e_AfterHeroTakesCrit += BuffEventEffect;               
    }

    public override void UnRegisterEventEffect ()
    {        
        heroEvents.e_AfterHeroTakesCrit -= BuffEventEffect;
    }

    public void BuffEventEffect(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(CauseBuffEffectEvent(Attacker, Target, tree));
    }

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
