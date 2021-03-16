using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UNHEALABLE : BuffComponent
{
    
    int reduceCounter = 1;
    GameObject Target;

    public override void RegisterEventEffect()
    {
        heroEvents.e_AfterHeroGetsHealed += BuffEventEffect;
    }

    public override void UnRegisterEventEffect()
    {
        heroEvents.e_AfterHeroGetsHealed -= BuffEventEffect;
    }

   public void BuffEventEffect(GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(CauseBuffEffectEvent(Target, tree));
    }
    public override IEnumerator CauseBuffEffectEvent(GameObject Target, CoroutineTree tree)
    {   
        if(this != null)
        {
            BuffEffectAnimation(Target);        
            tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     
                   
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }   

    public void BuffEffectAnimation (GameObject Target)
    {          
        VisualSystem.Instance.DebuffEffectAnimation(Target, this);
        VisualSystem.Instance.CreateFloatingText(buffAsset.buffBasicInfo.description, Target, Color.magenta);  
        
    }

     
}
