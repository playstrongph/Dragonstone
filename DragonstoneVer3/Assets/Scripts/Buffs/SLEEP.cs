using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLEEP : BuffComponent
{
        
    //public int value = 0;  
    
    int reduceCounter = 1;
    string onSleepRemoval = "AWAKE!";

    
    public override void RegisterEventEffect()
    {        
        heroEvents.e_AfterHeroTakesDamage += RemoveSleepOnDamage;       
    }

    public override void UnRegisterEventEffect ()
    {        
        heroEvents.e_AfterHeroTakesDamage -= RemoveSleepOnDamage;       
    }


    public override IEnumerator CauseBuffEffect(CoroutineTree tree)
    {   
        
         VisualSystem.Instance.DebuffEffectAnimation(this.gameObject, this);  
         
         //SetBuffCounters(fixedCounter);        
         //TurnManager.Instance.ResetHeroTimer(this.GetComponent<HeroLogic>());   //Set Energy to Zero

        tree.CorQ.CoroutineCompleted();
        yield return null;


    }   

    public override IEnumerator CauseBuffEffectEvent(GameObject Target, CoroutineTree tree)
    {  
        BuffEffectAnimation(Target);

       tree.AddCurrent(ReduceBuffCounters(reduceCounter,tree));     

       tree.AddCurrent(EndTurn(Target,tree));  

       tree.CorQ.CoroutineCompleted();
       yield return null;
    }


    public void BuffEffectAnimation (GameObject Target)
    {          
        VisualSystem.Instance.CreateFloatingText(buffAsset.buffBasicInfo.description, Target, Color.magenta);
        
    }

    public void RemoveSleepOnDamage(GameObject Target, CoroutineTree tree)
    {        
        VisualSystem.Instance.CreateFloatingText(onSleepRemoval, Target, Color.yellow);
        tree.AddCurrent(ReduceBuffCounters(buffCounter,tree));


    }

     
}
