using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATTACK_UP : BuffComponent
{
        
    //public List<AttackModifier> attackModifiers = new List<AttackModifier>();

    public int statMultiplier = 1;

    public int attackPlus;

    bool causeBuffEffect;


    public override IEnumerator CauseBuffEffect(CoroutineTree tree)
    {
        attackPlus = counter*statMultiplier;                  
        int newAttack = heroLogic.Attack;
        heroLogic.Attack = newAttack;      

        tree.CorQ.CoroutineCompleted();
        yield return null;   
        
    }

    public override IEnumerator UndoBuffEffect(CoroutineTree tree)
    {
        counter = 0;
        attackPlus = 0;          
        int newAttack = heroLogic.Attack;
        heroLogic.Attack = newAttack;

         tree.CorQ.CoroutineCompleted();
        yield return null;   


    }





    
     
}
