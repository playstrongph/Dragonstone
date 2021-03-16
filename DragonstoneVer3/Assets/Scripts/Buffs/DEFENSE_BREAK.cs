using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEFENSE_BREAK : BuffComponent
{
    int reduceCounter = 1;

    public override void RegisterEventEffect()
    {               
         //heroEvents.e_BeforeHeroTakesDamage += BuffEventEffect;
         heroEvents.e_AfterHeroTakesDamage += UndoBuffEventEffect;       
    }

    public override void UnRegisterEventEffect()
    {
         //heroEvents.e_BeforeHeroTakesDamage -= BuffEventEffect;
         heroEvents.e_AfterHeroTakesDamage -= UndoBuffEventEffect;
    }


    // public void BuffEventEffect(GameObject Target, CoroutineTree tree)
    // {
    //     tree.AddCurrent(CauseBuffEffectEvent(Target,tree));
    // }

    public void UndoBuffEventEffect(GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(UndoBuffEffectEvent(Target,tree));
    }

    


    public override IEnumerator UndoBuffEffectEvent(GameObject Target, CoroutineTree tree)
    {        
         VisualSystem.Instance.DebuffEffectAnimation(this.gameObject, this);  
         VisualSystem.Instance.CreateFloatingText(buffAsset.buffBasicInfo.description, Target, Color.magenta); 

        if(this != null)
        tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     

        tree.CorQ.CoroutineCompleted();
        yield return null;

    } 
   
}
