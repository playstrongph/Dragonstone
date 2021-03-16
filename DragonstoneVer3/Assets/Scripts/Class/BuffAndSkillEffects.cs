using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///<Summary>
/// Class that contains skills and buff logic
///</Summary>

public class BuffAndSkillEffects : MonoBehaviour
{

    public static BuffAndSkillEffects Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
    public void RunCauseBuffEffect(BuffComponent bc)
    {
        switch(bc.buffAsset.buffBasicInfo.buffName)
        {
            case(BuffName.ATTACK_DOWN):
            AttackDownBuffEffect(bc.GetComponent<ATTACK_DOWN>(), true);
            break;
        }
    }

    public void UndoCauseBuffEffect(BuffComponent bc)
    {
        switch(bc.buffAsset.buffBasicInfo.buffName)
        {
            case(BuffName.ATTACK_DOWN):
            AttackDownBuffEffect(bc.GetComponent<ATTACK_DOWN>(), false);
            break;
        }
    }
    
    
    
    
    ///<Summary>
    /// Add Attack_Down Logic
    ///</Summary>
    public void AttackDownBuffEffect(ATTACK_DOWN attackDown, bool causeEffect)
    {
        /// Get information from buff component
        int attackMinus = attackDown.attackMinus;
        int statMultiplier = attackDown.statMultiplier;
        int counter = attackDown.counter;
        var heroLogic = attackDown.heroLogic;

        if(causeEffect)
        {
            attackDown.attackMinus = counter*statMultiplier;    
            int newAttack = heroLogic.Attack;         

            heroLogic.Attack = newAttack;        
            Debug.Log("Attack Down Cause Effect");    

        }else
        {
            attackDown.counter = 0;
            attackDown.attackMinus = 0;        
            
            int newAttack = heroLogic.Attack;
            heroLogic.Attack = newAttack;
        }

    }

}
