using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHIELD : BuffComponent
{
    
    int reduceCounter = 1;

    

    GameObject Target;

    public override void RegisterEventEffect()
    { 
        heroEvents.e_AfterHeroTakesDamage += BuffEventEffect;        
    }

    public override void UnRegisterEventEffect ()
    {
       heroEvents.e_AfterHeroTakesDamage -= BuffEventEffect;
    }

    public void BuffEventEffect(GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(CauseBuffEffectEvent(Target, tree));
    }

    public override IEnumerator CauseBuffEffect(CoroutineTree tree)
    {        

      VisualSystem.Instance.BuffEffectAnimation(this.gameObject, this);  

      tree.CorQ.CoroutineCompleted();
      yield return null;
      
    }    

    public override IEnumerator CauseBuffEffectEvent(GameObject Target, CoroutineTree tree)
    {        
        VisualSystem.Instance.CreateFloatingText(buffAsset.buffBasicInfo.description, Target, Color.yellow);
        tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));          

        tree.CorQ.CoroutineCompleted();
        yield return null;        
    }


    

     
}
