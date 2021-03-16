using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLOW : BuffComponent
{


    //public List<SpeedModifier> speedModifiers = new List<SpeedModifier>();
    //public int value = 1;

    int statMultiplier = -1;

    public int speedMinus;

    bool causeBuffEffect;

    public override IEnumerator CauseBuffEffect(CoroutineTree tree)
    {      
        speedMinus = counter*statMultiplier;       
        VisualSystem.Instance.DebuffEffectAnimation(this.gameObject, this);  

         //set Property calls the font animation
        int newSpeed = heroLogic.Speed;
        heroLogic.Speed = newSpeed;      

        tree.CorQ.CoroutineCompleted();
        yield return null;


    }

    public override IEnumerator UndoBuffEffect(CoroutineTree tree)
    {
        counter = 0;
        speedMinus = 0;

        int newSpeed = heroLogic.Speed;
        heroLogic.Speed = newSpeed;      

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    

    
   
}
