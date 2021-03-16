using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATTACK_DOWN : BuffComponent
{
    
    public int statMultiplier = -1;
    public int attackMinus;

   
    public override IEnumerator CauseBuffEffect(CoroutineTree tree)
    {
            attackMinus = counter*statMultiplier;    
            int newAttack = heroLogic.Attack;         
            heroLogic.Attack = newAttack;        
            
            //Debug.Log("Attack Down Cause Effect");    

            tree.CorQ.CoroutineCompleted();
            yield return null;


    }

    public override IEnumerator UndoBuffEffect(CoroutineTree tree)
    {           
            counter = 0;
            attackMinus = 0;                    
            int newAttack = heroLogic.Attack;
            heroLogic.Attack = newAttack;

            tree.CorQ.CoroutineCompleted();
            yield return null;

    }

   

     
}
