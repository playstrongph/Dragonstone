using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class SkillComponent : MonoBehaviour
{

    public SkillAsset skillAsset;

    //public CoroutineTree tree = new CoroutineTree();

    public SkillType skillType;
    public HeroLogic heroLogic;
    public HeroEvents heroEvents;

    public SkillTarget skillTarget;
    public DragType dragType;

    public bool skillUsedLastTurn = false;

    public Queue<IEnumerator> coroutineQueue = GlobalSettings.Instance.coroutineQueue;

    // private int atkDamage;
    // private int finalDamage;

    
    
    public int _coolDown;
    public int baseCoolDown {
        get { return _coolDown; }
        set { if (value >= 0) _coolDown = value; }
    }

    public int _currCoolDown;
    public int currCoolDown {
        get { return _currCoolDown; }
        set { if (value >= 0) _currCoolDown = value; }
    }


    ///<Summary>
    ///Can be left as method since encapsulated in an IEnumerator 
    ///Convert to IEnumerator if problems are encountered
    ///</Summary>
    public void NormalReduceSkillCooldown()  
    {
         if(this.skillType == SkillType.ACTIVE || this.skillType == SkillType.PASSIVE_CD)
         {
            if(skillUsedLastTurn)
            {
                skillUsedLastTurn = false;
              
            }
            else
            {
                currCoolDown--;        
                if(currCoolDown < 0) currCoolDown = 0;
            }

            VisualSystem.Instance.UpdateSkillCooldown(this, currCoolDown);
            
         }
        

    }

    ///<Summary>
    ///Can be left as method since encapsulated in an IEnumerator 
    ///Convert to IEnumerator if problems are encountered
    ///</Summary>
    public void ReduceAllSkillCooldowns(GameObject Target, int counters)
    {
        if(isTargetAlive(Target))
          SkillSystem.Instance.ReduceAllSkillCooldowns(Target, counters);

    }

    ///<Summary>
    ///Can be left as method since encapsulated in an IEnumerator 
    ///Convert to IEnumerator if problems are encountered
    ///</Summary>
    public void IncreaseAllSkillCooldowns(GameObject Target, int counters)
    {
        if(isTargetAlive(Target))            
                SkillSystem.Instance.IncreaseAllSkillCooldowns(Target, counters);

    }

    ///<Summary>
    ///Can be left as method since encapsulated in an IEnumerator 
    ///Convert to IEnumerator if problems are encountered
    ///</Summary>
    public IEnumerator ResetAllSkillCooldownsToMax(GameObject Target, CoroutineTree tree)
    {
        if(isTargetAlive(Target))            
                tree.AddCurrent(SkillSystem.Instance.ResetAllSkillCooldownsToMax(Target,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    
    ///<Summary>
    ///Can be left as method since encapsulated in an IEnumerator 
    ///Convert to IEnumerator if problems are encountered
    ///</Summary>
    public IEnumerator RefreshAllSkillCooldownsToReady(GameObject Target, CoroutineTree tree)
    {
        if(isTargetAlive(Target))            
                tree.AddCurrent(SkillSystem.Instance.RefreshAllSkillCooldownsToReady(Target,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }


    ///<Summary>
    ///Can be left as method since encapsulated in an IEnumerator 
    ///Convert to IEnumerator if problems are encountered
    ///</Summary>
    public void SetAllSkillCooldownsToValue(GameObject Target, int value)
    {
        if(isTargetAlive(Target))            
                SkillSystem.Instance.SetAllSkillCooldownsToValue(Target, value);

    }


    ///<Summary>
    ///Used by Visual Commands
    ///</Summary>
    public void UpdateSkillCooldown(int currentCooldown)
    {
        //call cooldown text update here via commands, temp logic below
        GetComponent<SkillCardManager>().skillCooldown.text = currCoolDown.ToString();

        if (currentCooldown <=0)
        {
            GetComponent<SkillCardManager>().cardGlow.enabled = true;            
        }
        
        else
            GetComponent<SkillCardManager>().cardGlow.enabled = false;

    }   


    ///<Summary>
    ///Can be left as method since encapsulated in an IEnumerator 
    ///Convert to IEnumerator if problems are encountered
    ///</Summary>
    public void TimeDelay(float value)
    {
        VisualSystem.Instance.Delay(value);
    }


    public void New(int coolDown)
    {

    }

    void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
              
    }

    void OnDisable()
    {
        //if (heroEvents != null)
        //    UnRegisterEventEffect();
    }

    void OnEnable()
    {
        //if(heroEvents != null)
        //    RegisterEventEffect();
    }

    void OnDestroy()
    {
       
    }

    public virtual void SkillCommandLogic()
    {
        
    }

    public void SubscribeToEvents()
    {
        DelegateManager.Instance.e_StartOfGame += e_StartOfGame;

        RegisterEventEffect();

    }
    public void e_StartOfGame() {}

    

    ///<Summary>
    /// Create a different default method for Active Skill Cooldown Update
    /// Keep UseSkill Blank
    ///</Summary>        
    public virtual void UseSkill(GameObject Attacker, GameObject Target)
    {       
        
    }

    public IEnumerator ResetSkillCooldownAfterUse(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {

        if(this.skillType == SkillType.ACTIVE || this.skillType == SkillType.PASSIVE_CD)
        {            //Reset Skill CD and end Turn
            currCoolDown = baseCoolDown;  
            skillUsedLastTurn = true;
            VisualSystem.Instance.UpdateSkillCooldown(this, currCoolDown);            
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }



    public virtual void UsePassiveSkillWithCD(GameObject Hero)
    {        
        if(this.skillType == SkillType.PASSIVE_CD)
        {            //Reset Skill CD and end Turn
            currCoolDown = baseCoolDown;  
            skillUsedLastTurn = true;
            VisualSystem.Instance.UpdateSkillCooldown(this, currCoolDown);            
        }
    }

    ///<Summary>
    ///Starter method for Root Coroutine in the skills
    ///Keep as blank methods
    ///</Summary>
    public virtual IEnumerator UsePassiveSkill(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {        
        Debug.Log("Base UsePassiveSkill(Attacker, Target) Called");

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    ///<Summary>
    ///Starter method for Root Coroutine in the skills
    ///Keep as blank methods
    ///</Summary>
    public virtual IEnumerator UsePassiveSkill(GameObject Attacker, CoroutineTree tree)
    {        
        Debug.Log("Base UsePassiveSkill(Attacker) Called");

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    ///<Summary>
    ///Starter method for Root Coroutine in the skills
    ///Keep as blank methods
    ///</Summary>
    public virtual void UsePassiveSkill()
    {        
        Debug.Log("Base UsePassiveSkill() Called");   
    }

    ///<Summary>
    ///Starter method for Root Coroutine in the skills
    ///Keep as blank methods
    ///</Summary>
    public virtual IEnumerator UsePostAttackPassiveSkill(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {        
        Debug.Log("Base UsePostattackPassiveSkill(Attacker, Target) Called");

        tree.CorQ.CoroutineCompleted();
        yield return null;


    }

    ///<Summary>
    ///Starter method for Root Coroutine in the skills
    ///Keep as blank methods
    ///</Summary>
    public virtual void UsePostAttackPassiveSkill(GameObject Attacker)
    {        
        Debug.Log("Base UsePostattackPassiveSkill(Attacker) Called");
    }

    ///<Summary>
    ///Starter method for Root Coroutine in the skills
    ///Keep as blank methods
    ///</Summary>
    public virtual void UsePostAttackPassiveSkill()
    {        
        Debug.Log("Base UsePostattackPassiveSkill() Called");
    }

    ///<Summary>
    ///Starter method for Root Coroutine in the skills
    ///Keep as blank methods
    ///</Summary>
    public virtual IEnumerator UsePreAttackPassiveSkill(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {        
       
       // Debug.Log("Base UsePreAttackPassiveSkill(Attacker, Target) Called");

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    ///<Summary>
    ///Starter method for Root Coroutine in the skills
    ///Keep as blank methods
    ///</Summary>
    public virtual void UsePreAttackPassiveSkill(GameObject Attacker)
    {        
        Debug.Log("Base UsePreAttackPassiveSkill(Attacker) Called");
    }

    ///<Summary>
    ///Starter method for Root Coroutine in the skills
    ///Keep as blank methods
    ///</Summary>
    public virtual void UsePreAttackPassiveSkill()
    {        
        Debug.Log("Base UsePreAttackPassiveSkill() Called");
    }

    ///<Summary>
    /// Keep as method since encapsulated in an IEnumerator     
    ///</Summary>
    public IEnumerator GetPostAttackPassiveSkill(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {        
        if(isTargetAlive(Attacker) && isTargetAlive(Target))
        {
            foreach(var heroSkill in heroLogic.skills)
            {
                if(heroSkill.skillType == SkillType.PASSIVE_ATTACK)      
                {
                    tree.AddCurrent(heroSkill.UsePostAttackPassiveSkill(Attacker, Target,tree));
                }
            }
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    ///<Summary>
    /// Keep as method since encapsulated in an IEnumerator     
    ///</Summary>
    public IEnumerator GetPreAttackPassiveSkill(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {        
        if(isTargetAlive(Attacker) && isTargetAlive(Target))
        {
            foreach(var heroSkill in heroLogic.skills)
            {
                if(heroSkill.skillType == SkillType.PASSIVE_ATTACK)      
                {
                    tree.AddCurrent(heroSkill.UsePreAttackPassiveSkill(Attacker, Target, tree));
                }
            }
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;



    }
    
    ///<Summary>
    /// Keep as method since encapsulated in an IEnumerator     
    /// when used in skills
    ///</Summary>
    public virtual void UseSkillAnimation(GameObject Attacker, GameObject Target, SkillComponent skill)
    {
        
        if(isTargetAlive(Target))
        {
            VisualSystem.Instance.SkillAttackEffect1(Attacker, Target, skill);               
            
        }       
    }

    ///<Summary>
    /// Keep as method since encapsulated in an IEnumerator     
    /// when used in skills
    ///</Summary>
    public virtual void UseSkillAnimation(GameObject Attacker, GameObject Target, SkillComponent skill, float animDelay)
    {
        
        if(isTargetAlive(Target))
        {
            VisualSystem.Instance.SkillAttackEffect1(Attacker, Target, skill, animDelay);               
            
        }       
    }

    // public virtual void UseSkillAnimation(GameObject Attacker, GameObject Target, SkillComponent skill, float commandDelay)
    // {
        
    //     if(isTargetAlive(Target))
    //     {
    //         VisualSystem.Instance.SkillAttackEffect1(Attacker, Target, skill, commandDelay);               
            
    //     }       
    // }

    // public virtual void SkillAttackEffectAllEnemies(GameObject Attacker, GameObject Target, Command.ParallelCommandsDelegate parallelCommands)
    // {
        
    //     if(isTargetAlive(Target))
    //     {
    //         VisualSystem.Instance.GenericParallelActions(Attacker, Target, parallelCommands);               
            
    //     }       
    // }
    
    ///<Summary>
    /// Keep as method since encapsulated in an IEnumerator     
    /// when used in skills
    ///</Summary>
    public virtual void UseSkillPreview(GameObject Attacker, GameObject Target)
    {
        
        if(isTargetAlive(Target))
        {
            VisualSystem.Instance.UseSkillPreview(this);            
            
        }       
    }
    

    //Go Direct to BuffSystem
    // public void AddBuff(GameObject target, BuffName buffName, int buffCooldown)
    // {
    //     BuffSystem.Instance.AddBuff(target, buffName, buffCooldown);
    //     //VisualSystem.Instance.AddBuff(target, buffName, buffCooldown);
    // }


    public virtual void RegisterEventEffect(){}
    public virtual void UnRegisterEventEffect(){}
    public virtual void CauseEventEffect(){}
    // BATTLECRY
    public virtual void UndoEventEffect(){}
    public virtual void WhenHeroIsPlayed(){}
    // DEATHRATTLE
    public virtual void WhenHeroDies(){}
    public virtual void RegisterCooldown(){}
    public virtual void UnregisterCooldown(){}


    ///<Summary>
    /// Keep as method since encapsulated in an IEnumerator     
    /// when used in skills
    ///</Summary>
    public bool isTargetAlive(GameObject target)
    {
        if (target.GetComponent<HeroLogic>().Health > 0)
            return true;
        else
            return false;
    }


    public IEnumerator DealDamage(GameObject Attacker, GameObject Target, int damage, CoroutineTree tree)
    {
        if (isTargetAlive(Attacker) && isTargetAlive(Target)){}                   
        tree.AddCurrent(AttackSystem.Instance.DealDamage(Attacker, Target, damage, tree));        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator LoseLife(GameObject Attacker, GameObject Target, int damage, CoroutineTree tree)
    {
        if (isTargetAlive(Attacker) && isTargetAlive(Target))                   
        tree.AddCurrent(AttackSystem.Instance.LoseLife(Attacker, Target, damage, tree));        

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

     public IEnumerator LifeSteal(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if (isTargetAlive(Attacker) && isTargetAlive(Target))                   
        {
            float lifeStealFactor = 0.33f; 
            int value = Mathf.FloorToInt(AttackSystem.Instance.finalDamage*lifeStealFactor);
            
            tree.AddCurrent(Heal(Attacker, Attacker, value, tree));            
            
        }
       
         

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    public IEnumerator AttackHero(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if(isTargetAlive(Attacker) && isTargetAlive(Target))
        //AttackSystem.Instance.AttackHero(Attacker, Target);
        tree.AddCurrent(AttackSystem.Instance.AttackHero(Attacker, Target, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

   
    public IEnumerator AddBuff(GameObject Attacker, GameObject Target, BuffName BuffName, int buffCounter, CoroutineTree tree)
    {
        if (isTargetAlive(Target))
            tree.AddCurrent(BuffSystem.Instance.AddBuff(Attacker, Target, BuffName, buffCounter, tree));


        tree.CorQ.CoroutineCompleted();
        yield return null;
    }       

    public IEnumerator AddDebuff(GameObject Attacker, GameObject Target, BuffName BuffName, int buffCounter, CoroutineTree tree)
    {
        if (isTargetAlive(Target))
            tree.AddCurrent(BuffSystem.Instance.AddDebuff(Attacker, Target, BuffName, buffCounter, tree));

            tree.CorQ.CoroutineCompleted();
            yield return null;
    }

    public IEnumerator AddRandomDebuff(GameObject Attacker, GameObject Target, int buffCounter, CoroutineTree tree)
    {
        if (isTargetAlive(Target))
            tree.AddCurrent(BuffSystem.Instance.AddRandomDebuff(Attacker, Target, buffCounter, tree));

            tree.CorQ.CoroutineCompleted();
            yield return null;
    }

    public IEnumerator AddRandomBuff(GameObject Attacker, GameObject Target, int buffCounter, CoroutineTree tree)
    {
        if (isTargetAlive(Target))
            tree.AddCurrent(BuffSystem.Instance.AddRandomBuff(Attacker, Target, buffCounter, tree));

            tree.CorQ.CoroutineCompleted();
            yield return null;
    }

    public IEnumerator AddDebuffIgnoreImmunity(GameObject Attacker, GameObject Target, BuffName BuffName, int buffCounter, CoroutineTree tree)
    {
        if (isTargetAlive(Target))
            tree.AddCurrent(BuffSystem.Instance.AddDebuffIgnoreImmunity(Attacker, Target, BuffName, buffCounter, tree));

            tree.CorQ.CoroutineCompleted();
            yield return null;
    }

    public IEnumerator RemoveRandomDebuff(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if (isTargetAlive(Target))
            tree.AddCurrent(BuffSystem.Instance.RemoveRandomBuffOfType(Target, BuffType.DEBUFF, tree));

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    public IEnumerator RemoveRandomBuff(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if (isTargetAlive(Target))
            tree.AddCurrent(BuffSystem.Instance.RemoveRandomBuffOfType(Target, BuffType.BUFF, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator RemoveSpecificBuff(GameObject Target, BuffName buffName, CoroutineTree tree)
    {
        if (isTargetAlive(Target))
            tree.AddCurrent(BuffSystem.Instance.RemoveSpecificBuff(Target, buffName, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator RemoveAllBuffs(GameObject Target, CoroutineTree tree)
    {
        if(isTargetAlive(Target))
        tree.AddCurrent(BuffSystem.Instance.RemoveAllBuffsOfType(Target, BuffType.BUFF, tree));

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    public IEnumerator RemoveAllDebuffs(GameObject Target, CoroutineTree tree)
    {
        if(isTargetAlive(Target))
        tree.AddCurrent(BuffSystem.Instance.RemoveAllBuffsOfType(Target, BuffType.DEBUFF, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

   
    public IEnumerator Heal (GameObject Healer, GameObject Target, int healAmount, CoroutineTree tree)
    {               
        //These are for skills that require Healer information

       if (isTargetAlive(Target)) 
       {
        Target.GetComponent<HeroEvents>().BeforeHeroGetsHealed(Target, tree);      
        
        //Logic Start
        if(Target.GetComponent<UNHEALABLE>() != null){}  
        else
        {
            //healAmount = 0;

            if (healAmount < 0) 
            healAmount = 0;

            int newHealth = Target.GetComponent<HeroLogic>().Health + healAmount;
            if(newHealth > Target.GetComponent<HeroLogic>().BaseHealth)
            newHealth = Target.GetComponent<HeroLogic>().BaseHealth;
            //Logic End


            //visual
            VisualSystem.Instance.CreateHealEffect(healAmount, Target);                        

            Target.GetComponent<HeroLogic>().Health = newHealth;     

            tree.AddCurrent(Target.GetComponent<HeroLogic>().CheckDeath(tree,healAmount));                                    
        }

        //Event
        Target.GetComponent<HeroEvents>().AfterHeroGetsHealed(Target, tree);        
       }

       tree.CorQ.CoroutineCompleted();
       yield return null;

    }

     
   

    

//    void ClearValues()
//     {
//         atkDamage = 0;
//     }

    // public void AttackHero (GameObject Attacker, GameObject Target)
    // {
    //     ClearValues();
    //     StartCoroutine(AttackHeroSequence(Attacker,Target));
    //     //placeholder for logic and computations, with the end calling for Visual and Next Turn

        
        
    // }

    // IEnumerator AttackHeroSequence(GameObject Attacker, GameObject Target)
    // {

    //     yield return (StartCoroutine(PreAttackSequence(Attacker,Target)));
        
    //     yield return (StartCoroutine(Attack(Attacker, Target)));

    //      yield return (StartCoroutine(PostAttackSequence(Attacker,Target)));
        
    //     //yield return (StartCoroutine(EndTurnSequence(Attacker, Target)));

    //     yield return null;

    // }

    // IEnumerator PreAttackSequence(GameObject Attacker, GameObject Target)
    // {
    //     //Attacker.GetComponents<SkillComponent>()[0].PreAttack(); //[0] is for testing only
    //     Debug.Log("Call Pre-attack logic");

    //     atkDamage = ComputeDamageToDeal (Attacker);

    //     Attacker.GetComponent<HeroEvents>().BeforeHeroAttacks(Attacker,Target);
    //     Target.GetComponent<HeroEvents>().BeforeHeroIsAttacked(Attacker,Target);
        
        

    //     yield return null;
    // }

    // IEnumerator Attack(GameObject Attacker, GameObject Target)
    // {
    //     AttackTarget(Attacker, Target, atkDamage);
    //     yield return null;
    // }

    // IEnumerator PostAttackSequence(GameObject Attacker, GameObject Target)
    // {
        
    //     Debug.Log("Call Post-Attack Logic");

    //     Attacker.GetComponent<HeroEvents>().AfterHeroAttacks(Attacker,Target);
    //     Target.GetComponent<HeroEvents>().AfterHeroIsAttacked(Attacker,Target);



    //     //Attacker.GetComponents<SkillComponent>()[0].PostAttack(); //[0] is for testing only
    //     yield return null;
    // }

    // IEnumerator EndTurnSequence(GameObject Attacker, GameObject Target)
    // {
        
    //     //yield return new WaitForSeconds(1f);

    //     Debug.Log("Call End Turn Logic");
    //     TurnManager.Instance.ResetHeroTimer(Attacker.GetComponent<HeroLogic>());   
    //     TurnManager.Instance.NextActiveHero();
    //     yield return null;
    // }

    // public int ComputeDamageToDeal (GameObject Attacker)
    // {
    //     int damage =  Attacker.GetComponent<HeroLogic>().Attack;
    //     if (damage > 0)
    //         return damage;
    //     else
    //         return 0;
    // }
    

    // public void AttackTarget(GameObject Attacker, GameObject Target, int damage)
    // {   
       
    //     //before attack events
    //     Attacker.GetComponent<HeroEvents>().BeforeHeroDealsAtkDamage(Attacker,Target);
    //     Target.GetComponent<HeroEvents>().BeforeHeroTakesAtkDamage(Attacker,Target);        
        
    //     //attack visual   
    //     VisualSystem.Instance.AttackSingleHero(Attacker,Target);

    //     //calculation of damage
    //     int criticalStrikeMultiplier = Attacker.GetComponent<HeroLogic>().criticalStrikeFactor;      
    //     int attackDamage = damage*criticalStrikeMultiplier;
    //     StartCoroutine (DealDamage(Attacker, Target, attackDamage));

    //     //after attack events
    //     Attacker.GetComponent<HeroEvents>().AfterHeroDealsAtkDamage(Attacker,Target);
    //     Target.GetComponent<HeroEvents>().AfterHeroTakesAtkDamage(Attacker,Target);
        
    // }

    // public IEnumerator DealDamage (GameObject Attacker, GameObject Target, int damage)
    // {  
    //     if (isTargetAlive(Target))
    //     {      
    //         //before target takes damage event
    //         Target.GetComponent<HeroEvents>().BeforeHeroTakesDamage(Target);
            
    //         if (ComputeDamageToReceive(Target,damage) < 0)
    //             damage = 0;
            
    //         finalDamage = damage;       //final damage for effect purposes such as Lifesteal              

    //         VisualSystem.Instance.CreateDamageEffect(damage, Target);        

    //         //compute final values first
    //         int newArmor = ComputeNewArmor(Target,damage);  
    //         int newHealth = ComputeNewHealth(Target,damage);
    //         //then modify
    //         Target.GetComponent<HeroLogic>().Armor = newArmor;    
    //         Target.GetComponent<HeroLogic>().Health = newHealth;
            
    //         //after target takes damage event     
    //         //if(finalDamage > 0)     //consider this in case specificity is required
    //         Target.GetComponent<HeroEvents>().AfterHeroTakesDamage(Target);
    //     }

    //     yield return null;
        
    // }

    // public int ComputeDamageToReceive(GameObject Target, int damage)
    // {
    //     int invincibilityMultiplier = Target.GetComponent<HeroLogic>().invincibilityFactor;
    //     int shieldMultiplier = Target.GetComponent<HeroLogic>().shieldFactor;  
    //     int evasionMultiplier = Target.GetComponent<HeroLogic>().evasionFactor;               
        
    //     damage = damage*invincibilityMultiplier*shieldMultiplier*evasionMultiplier;
    //     return damage;
    // }

    

    

    // public int ComputeNewHealth(GameObject Target, int damage)
    // {
    //     if (Target.GetComponent<HeroLogic>().Armor > damage)
    //         return Target.GetComponent<HeroLogic>().Health;
    //     else
    //         return (Target.GetComponent<HeroLogic>().Health + Target.GetComponent<HeroLogic>().Armor) - damage;

    // }

    // public int ComputeNewArmor(GameObject Target, int damage)
    // {
    //     if (Target.GetComponent<HeroLogic>().Armor > damage)
    //         return Target.GetComponent<HeroLogic>().Armor - damage;
    //     else
    //         return 0;
    // }

    //For Cleanup - this should not be here but in AttackSystem
    // public void AfterAttacking()
    // {
    //     Debug.Log("AfterAttacking()");
    //     heroEvents.e_AfterAttacking += UseSkill;
    // }

    // public void AfterEnemyAttack()
    // {
    //     foreach (GameObject go in HeroManager.Instance.EnemiesList(heroLogic.gameObject))
    //     {
    //         go.GetComponent<HeroEvents>().e_AfterAttacking += UseSkill;
    //     }
    // }
    public IEnumerator IncreaseArmor (GameObject Target, int amount, CoroutineTree tree)
    {
        if (isTargetAlive(Target))
        Target.GetComponent<HeroLogic>().IncreaseArmor(amount);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public void IncreaseBaseHealth (GameObject Target, int amount)
    {
        if (isTargetAlive(Target))
        {
            Target.GetComponent<HeroLogic>().IncreaseBaseHealth(amount);         
        }        
    }

    public void DecreaseBaseHealth (GameObject Target, int amount)
    {
        if (isTargetAlive(Target))
        {
            
            Target.GetComponent<HeroLogic>().DecreaseBaseHealth(amount);

            
        }        
    }

    public void IncreaseBaseAttack (GameObject Target, int amount)
    {
        if (isTargetAlive(Target))
        {
            Target.GetComponent<HeroLogic>().IncreaseBaseAttack(amount);                                 
        }        
    }

    public void DecreaseBaseAttack (GameObject Target, int amount)
    {
        if (isTargetAlive(Target))
        {
            Target.GetComponent<HeroLogic>().DecreaseBaseAttack(amount);

           
        }        
    }

    public void IncreaseBaseChance (GameObject Target, int amount)
    {
        if (isTargetAlive(Target))
        {
            Target.GetComponent<HeroLogic>().IncreaseBaseChance(amount);  
            
        }        
    }

    public void DecreaseBaseChance (GameObject Target, int amount)
    {
        if (isTargetAlive(Target))
        {
            Target.GetComponent<HeroLogic>().DecreaseBaseChance(amount);
            
        }        
    }

    public void IncreaseBaseSpeed (GameObject Target, int amount)
    {
        if (isTargetAlive(Target))
        {
            Target.GetComponent<HeroLogic>().IncreaseBaseSpeed(amount);          
            
        }        
    }

    public void DecreaseBaseSpeed (GameObject Target, int amount)
    {
        if (isTargetAlive(Target))
        {
            Target.GetComponent<HeroLogic>().DecreaseBaseSpeed(amount);
            
        }        
    }



    public void DecreaseArmor (GameObject Target, int amount)
    {
        if (isTargetAlive(Target))
        Target.GetComponent<HeroLogic>().DecreaseArmor(amount);
    }



    public IEnumerator IncreaseEnergy(GameObject target, float amount, CoroutineTree tree)
    {
        if(isTargetAlive(target))
        tree.AddCurrent(target.GetComponent<HeroLogic>().IncreaseEnergy(amount, tree));        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator DecreaseEnergy(GameObject target, float amount, CoroutineTree tree)
    {
        if(isTargetAlive(target))

        tree.AddCurrent(target.GetComponent<HeroLogic>().DecreaseEnergy(amount, tree));        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator SetEnergyToValue(GameObject target, float amount, CoroutineTree tree)
    {
        if(isTargetAlive(target))
        tree.AddCurrent(target.GetComponent<HeroLogic>().SetEnergyToValue(amount, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }


    public IEnumerator ExtraTurn(GameObject Hero, CoroutineTree tree)
    {
        //SkillComponent extraTurn;
        if(isTargetAlive(Hero))
            if(Hero.GetComponent<ExtraTurn>() != null){}else  //prevent series of extra turns                
               Hero.AddComponent<ExtraTurn>();

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator IncreaseRandomBuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {
        if(isTargetAlive(Target))
        tree.AddCurrent(BuffSystem.Instance.IncreaseRandomBuffCounters(Target, counters,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator IncreaseSpecificBuffCounters(GameObject Target, BuffName buffName, int counters, CoroutineTree tree)
    {
        if(isTargetAlive(Target))
        tree.AddCurrent(BuffSystem.Instance.IncreaseSpecificBuffCounters(Target, buffName, counters, tree));

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    public IEnumerator IncreaseAllBuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {
        if(isTargetAlive(Target))
        tree.AddCurrent(BuffSystem.Instance.IncreaseAllBuffCounters(Target, counters,tree));

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    public IEnumerator IncreaseRandomDebuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {
        if(isTargetAlive(Target))
        tree.AddCurrent(BuffSystem.Instance.IncreaseRandomDebuffCounters(Target, counters, tree));

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    public IEnumerator IncreaseSpecificDebuffCounters(GameObject Target, BuffName buffName, int counters, CoroutineTree tree)
    {
        if(isTargetAlive(Target))
        tree.AddCurrent(BuffSystem.Instance.IncreaseSpecificDebuffCounters(Target, buffName, counters,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator IncreaseAllDebuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {
        if(isTargetAlive(Target))
        tree.AddCurrent(BuffSystem.Instance.IncreaseAllDebuffCounters(Target, counters,tree));

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    public IEnumerator ReduceRandomBuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {
        if(isTargetAlive(Target))
        tree.AddCurrent(BuffSystem.Instance.ReduceRandomBuffCounters(Target, counters, tree));

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    public IEnumerator ReduceSpecificBuffCounters(GameObject Target, BuffName buffName, int counters, CoroutineTree tree)
    {
        if(isTargetAlive(Target))
        tree.AddCurrent(BuffSystem.Instance.ReduceSpecificBuffCounters(Target, buffName, counters,tree));

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    public IEnumerator ReduceAllBuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {
        if(isTargetAlive(Target))
        tree.AddCurrent(BuffSystem.Instance.ReduceAllBuffCounters(Target, counters,tree));

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    public IEnumerator ReduceRandomDebuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {
        if(isTargetAlive(Target))
        tree.AddCurrent(BuffSystem.Instance.ReduceRandomDebuffCounters(Target, counters,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator ReduceSpecificDebuffCounters(GameObject Target, BuffName buffName, int counters, CoroutineTree tree)
    {
        if(isTargetAlive(Target))
        tree.AddCurrent(BuffSystem.Instance.ReduceSpecificDebuffCounters(Target, buffName, counters,tree));

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    public IEnumerator ReduceAllDebuffCounters(GameObject Target, int counters, CoroutineTree tree)
    {
        if(isTargetAlive(Target))
        tree.AddCurrent(BuffSystem.Instance.ReduceAllDebuffCounters(Target, counters,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public void StealRandomBuff(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if(isTargetAlive(Attacker) && isTargetAlive(Target))
        BuffSystem.Instance.StealRandomBuff(Attacker, Target, tree);

        
    }

    public void StealAllBuffs(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if(isTargetAlive(Attacker) && isTargetAlive(Target))
        BuffSystem.Instance.StealAllBuffs(Attacker, Target, tree);
    }

    public void TransferRandomDebuff(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if(isTargetAlive(Attacker) && isTargetAlive(Target))
        BuffSystem.Instance.TransferRandomDebuff(Attacker, Target, tree);
    }

    public void TransferAllDebuffs(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if(isTargetAlive(Attacker) && isTargetAlive(Target))
        BuffSystem.Instance.TransferAllDebuffs(Attacker, Target, tree);
    }

    public IEnumerator SetHealth(GameObject Target, int value, CoroutineTree tree)
    {
        if(isTargetAlive(Target))
        tree.AddCurrent(Target.GetComponent<HeroLogic>().SetHealth(value,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    


}


