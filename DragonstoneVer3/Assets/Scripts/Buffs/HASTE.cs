using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HASTE : BuffComponent
{


    //public List<SpeedModifier> speedModifiers = new List<SpeedModifier>();
    //public int value = 1;

     int statMultiplier = 1;

     public int speedPlus;

     bool causeBuffEffect;



    public override IEnumerator CauseBuffEffect(CoroutineTree tree)
    {
        speedPlus = counter*statMultiplier;        
        VisualSystem.Instance.BuffEffectAnimation(this.gameObject, this);              
        int newSpeed = heroLogic.Speed;
        heroLogic.Speed = newSpeed;   

        tree.CorQ.CoroutineCompleted();
        yield return null;
        
    }

    public override IEnumerator UndoBuffEffect(CoroutineTree tree)
    {
        counter = 0;
        speedPlus = 0;
        int newSpeed = heroLogic.Speed;
        heroLogic.Speed = newSpeed; 

        tree.CorQ.CoroutineCompleted();
        yield return null;
        
    }

    

  
    
}
