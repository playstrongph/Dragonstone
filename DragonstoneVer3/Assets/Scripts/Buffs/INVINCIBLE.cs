using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INVINCIBLE : BuffComponent
{
        
    // public int value = 0;  
    // public int defaultValue = 1;
    int reduceCounter = 1;
    //bool causeBuffEffect;

    //int causeBuffEffect = 0;

    

    GameObject Target;

    public override void RegisterEventEffect()
    {       
        heroEvents.e_AfterHeroStartTurn += BuffEventEffect;        
        heroEvents.e_AfterHeroTakesDamage += BuffAnimationEvent;
    }

    public override void UnRegisterEventEffect ()
    {        
        heroEvents.e_AfterHeroStartTurn -= BuffEventEffect;        
        heroEvents.e_AfterHeroTakesDamage -= BuffAnimationEvent;
    }

    public override IEnumerator CauseBuffEffect(CoroutineTree tree)
    {        
               
        if(this != null) // In case buff is destroyed
        VisualSystem.Instance.BuffEffectAnimation(this.gameObject, this);  

        tree.CorQ.CoroutineCompleted();
        yield return null;
        
        
    }

    public void BuffEventEffect(GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(CauseBuffEffectEvent(Target, tree));
    }


     public override IEnumerator CauseBuffEffectEvent(GameObject Target, CoroutineTree tree)
    {              
    
        if(this != null)
        tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    public void BuffAnimationEvent(GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(BuffEffectAnimation(Target,tree));
    }

     public IEnumerator BuffEffectAnimation (GameObject Target, CoroutineTree tree)
    {                  
        if(this != null) 
        VisualSystem.Instance.CreateFloatingText(buffAsset.buffBasicInfo.description, Target, Color.yellow);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    
    
     
}
