using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUCKY : BuffComponent
{
    //public List<ChanceModifier> chanceModifiers = new List<ChanceModifier>();
 
    int statMultiplier = 5;

    public int chancePlus;

    bool causeBuffEffect;
 
    public override IEnumerator CauseBuffEffect(CoroutineTree tree)
    {       
        chancePlus = counter*statMultiplier;                   
        int newChance = heroLogic.Chance;
        heroLogic.Chance = newChance;

        tree.CorQ.CoroutineCompleted();
        yield return null;      
   
    }

    public override IEnumerator UndoBuffEffect(CoroutineTree tree)
    {      
    
        counter = 0;
        chancePlus = 0;
        int newChance = heroLogic.Chance;
        heroLogic.Chance = newChance;

        tree.CorQ.CoroutineCompleted();
        yield return null;      

    }

   

  
    
    

     
}
