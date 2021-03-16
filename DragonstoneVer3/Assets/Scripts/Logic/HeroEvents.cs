using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroEvents : MonoBehaviour
{

    

    public delegate void VoidWithNoArguments();



    //public event VoidWithNoArguments e_AfterHeroTurnStart;
    //public event VoidWithNoArguments e_HeroOnTurnEnd;




//attacking related events
    public delegate void VoidWithHL(GameObject Attacker, GameObject Target, CoroutineTree tree);
    

    public delegate void VoidWithHLB(GameObject Attacker, GameObject Target,BuffName buffName, int buffCounter);



#region HERO ATTACK RELATED EVENTS

    //public event VoidWithHL e_AfterAttacking;   //redundant
    public event VoidWithHL e_BeforeHeroAttacks; //@ PreAttackSequence (AttackSystem.cs)   
    public event VoidWithHL e_AfterHeroAttacks;  //@ PostAttackSequence (AttackSystem.cs)
   
    public event VoidWithHL e_AfterHeroIsAttacked; //@ PostAttackSequence (AttackSystem.cs)
    public event VoidWithHL e_BeforeHeroIsAttacked; //@ PreAttackSequence (AttackSystem.cs)

    public event VoidWithHL e_AfterHeroDealsCrit;  //@ CRITICAL_STRIKE: Buffcomponent and CRITICAL_STRIKE: Skillcomponent 

    public event VoidWithHL e_AfterHeroDealsCrippleAttack;  //@ CRITICAL_STRIKE: Buffcomponent and CRITICAL_STRIKE: Skillcomponent 
    public event VoidWithHL e_BeforeHeroDealsCrit;  //@ CRITICAL_STRIKE: Buffcomponent and CRITICAL_STRIKE: Skillcomponent
    public event VoidWithHL e_BeforeHeroReceivesCrit;  //@ CRITICAL_STRIKE: Buffcomponent and CRITICAL_STRIKE: Skillcomponent 
    public event VoidWithHL e_AfterHeroTakesCrit;  //@ CRITICAL_STRIKE: Buffcomponent and CRITICAL_STRIKE: Skillcomponent 


#endregion

#region  HERO BUFF DEBUFF RELATED EVENTS
    public event VoidWithHLB e_AfterHeroInflictsDebuff;  //@ BuffSystem: AddNewDebuff, UpdateDebuff, BlockDebuff
    public event VoidWithHLB e_BeforeHeroInflictsDebuff;  //@BuffSystem:  AddDebuff
    public event VoidWithHLB e_AfterHeroReceivesDebuff;   //@ BuffSystem:  AddNewDebuff, UpdateDebuff
    public event VoidWithHLB e_BeforeHeroReceivesDebuff;  //@BuffSystem:  AddDebuff


    public event VoidWithHLB e_AfterHeroGrantsBuff;     //@ BuffSystem: AddNewBuff, UpdateBuff, BlockBuff
    public event VoidWithHLB e_BeforeHeroGrantsBuff;     //@BuffSystem:  AddBuff
    public event VoidWithHLB e_AfterHeroReceivesBuff;    //@ BuffSystem:  AddNewBuff, UpdateBuff
    public event VoidWithHLB e_BeforeHeroReceivesBuff;   //@BuffSystem:  AddBuff

#endregion


#region  HERO DAMAGE AND HEALING RELATED EVENTS    
    
    
    public event VoidWithHL e_AfterHeroTakesAtkDamage;  //@AttackSystem, AttackTarget, post DealDamage

    public event VoidWithHL e_AfterHeroDealsAtkDamage;  //@AttackSystem, AttackTarget, post DealDamage

    public event VoidWithHL e_BeforeHeroTakesAtkDamage;  //@AttackSystem, AttackTarget, post DealDamage

    public event VoidWithHL e_BeforeHeroDealsAtkDamage;  //@AttackSystem, AttackTarget, post DealDamage

    public event VoidWithHero e_AfterHeroGetsHealed;   //@Skillsystem, Heal (GameObject Healer, GameObject Target, int healAmount)
    public event VoidWithHero e_BeforeHeroGetsHealed;   //@Skillsystem, Heal (GameObject Healer, GameObject Target, int healAmount)

#endregion

//hero only related events. IF attacker/target is needed in logic, move to VoidWithHL
    public delegate void VoidWithHero(GameObject Hero, CoroutineTree tree);

#region  HERO BUFF DEBUFF RELATED EVENTS
    

#endregion

#region  HERO ENERGY RELATED EVENTS
    public event VoidWithHero e_AfterHeroGainsEnergy;   

    public event VoidWithHero e_BeforeHeroGainsEnergy;  

    public event VoidWithHero e_AfterHeroLosesEnergy;   

    public event VoidWithHero e_BeforeHeroLosesEnergy;   
    public event VoidWithHero e_AfterHeroFullEnergy;

#endregion

#region  HERO ATTACK AND DAMAGE RELATED EVENTS
    
    public event VoidWithHero e_BeforeHeroDealsDamage;  //@AttackSystem DealDamage, before damage application
    public event VoidWithHero e_AfterHeroDealsDamage;  //@AttackSystem DealDamage, after damage application

    public event VoidWithHero e_BeforeHeroTakesDamage;  //@AttackSystem DealDamage, before damage application
    public event VoidWithHero e_AfterHeroTakesDamage;  //@AttackSystem DealDamage, after damage application

#endregion



#region  HERO DEATH & RESPAWN RELATED EVENTS
    public event VoidWithHero e_AfterHeroDies;   //@ Herologic: Herodies

     public event VoidWithHero e_AfterHeroPreventsFatalDamage;
    public event VoidWithHero e_AfterHeroRevives;  //Create this at Herologic, opposite of HeroDies

#endregion

#region  HERO TURN RELATED EVENTS
    public event VoidWithHero e_AfterHeroStartTurn;  //@Herologic, SetHeroActive

    public event VoidWithHero e_AfterHeroStartTurnBuffs;  //@Herologic, SetHeroActive
    public event VoidWithHero e_AfterHeroEndTurn;    //@Herologic, SetHeroInActive

#endregion    

   public event VoidWithHero e_AfterHeroCastsSpell;



    public void AfterHeroAttacks(GameObject Attacker, GameObject Target, CoroutineTree tree)  
    {        
        //Debug.Log("AfterHeroAttacks");
        e_AfterHeroAttacks?.Invoke(Attacker,Target,tree);
    }

    public void AfterHeroIsAttacked(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroIsAttacked");
        e_AfterHeroIsAttacked?.Invoke(Attacker,Target,tree);
    }

    public void BeforeHeroIsAttacked(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {        
        //Debug.Log("BeforeHeroIsAttacked");
        e_BeforeHeroIsAttacked?.Invoke(Attacker,Target,tree);
    }

    public void BeforeHeroAttacks(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {        
        //Debug.Log("BeforeHeroAttacks");
        e_BeforeHeroAttacks?.Invoke(Attacker,Target,tree);
    }

    public void BeforeHeroDealsCrit(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {        
        //Debug.Log("BeforeHeroDealsCrit");
        e_BeforeHeroDealsCrit?.Invoke(Attacker,Target,tree);
    }

    public void BeforeHeroReceivesCrit(GameObject Attacker, GameObject Target,CoroutineTree tree)
    {        
        //Debug.Log("BeforeHeroReceivesCrit");
        e_BeforeHeroReceivesCrit?.Invoke(Attacker,Target,tree);
    }


    public void AfterHeroDealsCrit(GameObject Attacker, GameObject Target,CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroDealsCrit");
        e_AfterHeroDealsCrit?.Invoke(Attacker,Target,tree);
    }

    public void AfterHeroDealsCrippleAttack(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroDealsCrippleAttack");
        e_AfterHeroDealsCrippleAttack?.Invoke(Attacker,Target,tree);
    }
    public void AfterHeroTakesCrit(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroTakesCrit");      
        e_AfterHeroTakesCrit?.Invoke(Attacker,Target,tree);
    }
    
    public void BeforeHeroInflictsDebuff(GameObject Attacker, GameObject Target, BuffName buffName, int buffCounter)
    {        
        //Debug.Log("BeforeHeroInflictsDebuff");
        e_BeforeHeroInflictsDebuff?.Invoke(Attacker,Target, buffName, buffCounter);
    }

    public void AfterHeroGrantsBuff(GameObject Attacker, GameObject Target, BuffName buffName, int buffCounter)
    {        
        //Debug.Log("AfterHeroGrantsBuff");
        e_AfterHeroGrantsBuff?.Invoke(Attacker,Target, buffName, buffCounter);
    }

    public void BeforeHeroGrantsBuff(GameObject Attacker, GameObject Target, BuffName buffName, int buffCounter)
    {        
        //Debug.Log("BeforeHeroGrantsBuff");
        e_BeforeHeroGrantsBuff?.Invoke(Attacker,Target, buffName, buffCounter);
    }

    public void AfterHeroInflictsDebuff(GameObject Attacker, GameObject Target, BuffName buffName, int buffCounter)
    {        
        //Debug.Log("AfterHeroInflictsDebuff");
        e_AfterHeroInflictsDebuff?.Invoke(Attacker,Target, buffName, buffCounter);
    }
    
    public void AfterHeroDealsDamage(GameObject Attacker, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroDealsDamage");
        e_AfterHeroDealsDamage?.Invoke(Attacker,tree);
    }
    public void AfterHeroTakesDamage(GameObject Target, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroTakesDamage");
        e_AfterHeroTakesDamage?.Invoke(Target,tree);
    }

    public void BeforeHeroDealsDamage(GameObject Attacker, CoroutineTree tree)
    {        
        //Debug.Log("BeforeHeroDealsDamage");
        e_BeforeHeroDealsDamage?.Invoke(Attacker, tree);
    }
    
    public void BeforeHeroTakesDamage(GameObject Target, CoroutineTree tree)    {        
        //Debug.Log("BeforeHeroTakesDamage");
        e_BeforeHeroTakesDamage?.Invoke(Target, tree);
    }

    

    public void AfterHeroTakesAtkDamage(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroTakesAtkDamage");
        e_AfterHeroTakesAtkDamage?.Invoke(Attacker,Target,tree);
    }

    public void AfterHeroDealsAtkDamage(GameObject Attacker, GameObject Target,CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroDealsAtkDamage");
        e_AfterHeroDealsAtkDamage?.Invoke(Attacker,Target,tree);
    }

    public void BeforeHeroTakesAtkDamage(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {        
        //Debug.Log("BeforeHeroTakesAtkDamage");
        e_BeforeHeroTakesAtkDamage?.Invoke(Attacker,Target,tree);
    }

    public void BeforeHeroDealsAtkDamage(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {        
        //Debug.Log("BeforeHeroDealsAtkDamage");
        e_BeforeHeroDealsAtkDamage?.Invoke(Attacker,Target,tree);
    }
    public void AfterHeroReceivesBuff(GameObject Attacker, GameObject Target, BuffName buffName, int buffCounter)
    {        
        //Debug.Log("AfterHeroReceivesBuff");
        e_AfterHeroReceivesBuff?.Invoke(Attacker,Target, buffName, buffCounter);
    }
    public void AfterHeroReceivesDebuff(GameObject Attacker, GameObject Target, BuffName buffName, int buffCounter)
    {        
        //Debug.Log("AfterHeroReceivesDebuff");
        e_AfterHeroReceivesDebuff?.Invoke(Attacker,Target, buffName, buffCounter);
    }
    public void BeforeHeroReceivesDebuff(GameObject Attacker, GameObject Target, BuffName buffName, int buffCounter)
    {        
        //Debug.Log("BeforeHeroReceivesDebuff");
        e_BeforeHeroReceivesDebuff?.Invoke(Attacker,Target, buffName, buffCounter);
    }
    public void BeforeHeroReceivesBuff(GameObject Attacker, GameObject Target, BuffName buffName, int buffCounter)
    {        
        //Debug.Log("BeforeHeroReceivesBuff");
        e_BeforeHeroReceivesBuff?.Invoke(Attacker,Target, buffName, buffCounter);
    }
    public void AfterHeroGainsEnergy(GameObject Hero, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroGainsEnergy");
        e_AfterHeroGainsEnergy?.Invoke(Hero, tree);
    }

    public void BeforeHeroGainsEnergy(GameObject Hero, CoroutineTree tree)
    {        
        //Debug.Log("BeforeHeroGainsEnergy");
        e_BeforeHeroGainsEnergy?.Invoke(Hero, tree);
    }

    public void BeforeHeroLosesEnergy(GameObject Hero, CoroutineTree tree)
    {        
        //Debug.Log("BeforeHeroLosesEnergy");
        e_BeforeHeroLosesEnergy?.Invoke(Hero,tree);
    }

    public void AfterHeroLosesEnergy(GameObject Hero, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroLosesEnergy");
        e_AfterHeroLosesEnergy?.Invoke(Hero, tree);
    }
    
    public void AfterHeroDies(GameObject Hero, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroDies");
        e_AfterHeroDies?.Invoke(Hero, tree);
    }

     public void AfterHeroPreventsFatalDamage(GameObject Hero, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroDies");
        e_AfterHeroPreventsFatalDamage?.Invoke(Hero, tree);
    }
    public void AfterHeroRevives(GameObject Hero, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroRevives");
        e_AfterHeroRevives?.Invoke(Hero, tree);
    }
    public void AfterHeroStartTurn(GameObject Hero, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroStartTurn");
        e_AfterHeroStartTurn?.Invoke(Hero, tree);
    }

    public void AfterHeroStartTurnBuffs(GameObject Hero, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroStartTurn");
        e_AfterHeroStartTurnBuffs?.Invoke(Hero, tree);
    }
    public void AfterHeroEndTurn(GameObject Hero, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroEndTurn");
        e_AfterHeroEndTurn?.Invoke(Hero, tree);
    }
    public void AfterHeroGetsHealed(GameObject Hero, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroGetsHealed");
        e_AfterHeroGetsHealed?.Invoke(Hero, tree);
    }
    public void BeforeHeroGetsHealed(GameObject Hero, CoroutineTree tree)
    {        
        //Debug.Log("BeforeHeroGetsHealed");
        e_BeforeHeroGetsHealed?.Invoke(Hero, tree);
    }

    


    public void AfterHeroFullEnergy(GameObject Hero, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroFullEnergy");
        e_AfterHeroFullEnergy?.Invoke(Hero, tree);
    }
    public void AfterHeroCastsSpell(GameObject Hero, CoroutineTree tree)
    {        
        //Debug.Log("AfterHeroCastsSpell");
        e_AfterHeroCastsSpell?.Invoke(Hero, tree);
    }
}
