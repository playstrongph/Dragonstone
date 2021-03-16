using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UNLUCKY : BuffComponent
{
        
    
    int statMultiplier = -5;

    public int chanceMinus;

    bool causeBuffEffect;

    
    public override IEnumerator CauseBuffEffect(CoroutineTree tree)
    {         
        chanceMinus = counter*statMultiplier;    
            
        int newChance = heroLogic.Chance;
        heroLogic.Chance = newChance;     

        tree.CorQ.CoroutineCompleted();
        yield return null;   
    }

    public override IEnumerator UndoBuffEffect(CoroutineTree tree)
    {
        counter = 0;
        chanceMinus = 0;

        int newChance = heroLogic.Chance;
        heroLogic.Chance = newChance;

        tree.CorQ.CoroutineCompleted();
        yield return null;     
    }

  
     
}
