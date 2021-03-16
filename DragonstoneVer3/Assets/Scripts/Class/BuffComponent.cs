using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffComponent : MonoBehaviour
{
    public BuffAsset buffAsset;

    public BuffComponent thisBuff;

    public int counter;

    //public CoroutineTree tree = new CoroutineTree();
    
    public int fixedCounter = 1;
    public int buffCounter
    {
        get {return counter;}
        set
        {
            counter = value;
            if(counter > 0)
            {
                //for Implementation
                //new UpdateBuffCommand(this).AddToQueue();
            }
            if(counter <= 0)
            {}
                //For Implementation
                //RemoveBuff();
        }
    }

    public int newCounters;

    public HeroLogic heroLogic;
    public HeroEvents heroEvents;
    public GameObject buffVisualObject;

    public GameObject buffAnimationObject;

    //public delegate void BuffCommandLogicDelegate();
    public Command.CommandLogicDelegate m_CommandLogicDelegate;

    public void New(int counter)
    {

    }

    void Awake()
    {
        heroLogic = GetComponent<HeroLogic>();
        heroEvents = GetComponent<HeroEvents>();
        RegisterEventEffect();
        RegisterCooldown();

        //for command wrapping
        //thisBuff = BuffSystem.Instance.FindBuffComponent(this.gameObject, buffAsset.buffBasicInfo.buffName);
        
    }

    void OnDestroy()
    {
        UnregisterCooldown();
        UnRegisterEventEffect();
    }
    
    public IEnumerator ReduceBuffCounters(int counters, CoroutineTree tree)
    {
        
        buffCounter = buffCounter - counters;       

        if(buffCounter < 1) 
        tree.AddCurrent(BuffSystem.Instance.DestroyBuff(this, tree));
        else
        VisualSystem.Instance.UpdateBuffCounter(this, buffCounter);

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    public void SetBuffCounters(int counters)
    {
        if(this != null)
        {
            Debug.Log("Set Buff Counters");
            buffCounter = counters;               
            VisualSystem.Instance.UpdateBuffCounter(this, buffCounter);
            if(buffCounter < 1) {}    
            //BuffSystem.Instance.DestroyBuff(this);
        }
        
    }

    public IEnumerator IncreaseBuffCounters(int counters, BuffComponent bc, CoroutineTree tree)
    {
        //buffCounter++;
        BuffName buffName = this.buffAsset.buffBasicInfo.buffName;
        switch(buffName)
        {
            case BuffName.STUN:
            buffCounter = fixedCounter;
            break;

            // case BuffName.SLEEP:
            // buffCounter = fixedCounter;
            // break;

            case BuffName.RESURRECT:
            buffCounter = fixedCounter;
            break;

            default:
            buffCounter += counters;
            break;
        }

        if(buffCounter <=0)
        {
            tree.AddCurrent(BuffSystem.Instance.DestroyBuff(bc, tree));            
        }else        
        //call Visual System
        VisualSystem.Instance.UpdateBuffCounter(this, buffCounter);

         tree.CorQ.CoroutineCompleted();
         yield return null;
        
    }

     public bool isTargetAlive(GameObject target)
    {
        if (target.GetComponent<HeroLogic>().Health > 0)
            return true;
        else
            return false;
    }


    public IEnumerator DealDamageNoAttacker(GameObject Target, int damage, CoroutineTree tree)
    {
        if (isTargetAlive(Target)){}                   
        tree.AddCurrent(AttackSystem.Instance.DealDamageNoAttacker(Target, damage, tree));        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }
    
//    Moved this to Awake of Buff
//    public void SubscribeToEvents()
//     {
//         DelegateManager.Instance.e_StartOfGame += e_StartOfGame;

//         RegisterEventEffect();

//     }
    public void e_StartOfGame() {}

     public void PreAttack()
    {
        Debug.Log("Execute PreAttack");
    }

    // public void Heal (GameObject Target, int healAmount, CoroutineTree tree)
    // {        
        
    //    //Event
    //     Target.GetComponent<HeroEvents>().BeforeHeroGetsHealed(Target, tree);      
        
    //     //Logic Start
    //     if(Target.GetComponent<UNHEALABLE>() != null)  //Unhealable effect
    //     healAmount = 0;

    //     if (healAmount < 0) 
    //     healAmount = 0;

    //     int newHealth = Target.GetComponent<HeroLogic>().Health + healAmount;

    //     if(newHealth > Target.GetComponent<HeroLogic>().BaseHealth)
    //     newHealth = Target.GetComponent<HeroLogic>().BaseHealth;
        

        
    //     VisualSystem.Instance.CreateHealEffect(healAmount, Target);                        
    //     Target.GetComponent<HeroLogic>().Health = newHealth;                            

    //     //Event
    //     Target.GetComponent<HeroEvents>().AfterHeroGetsHealed(Target, tree);   

    // }

    public IEnumerator Heal (GameObject Target, int healAmount, CoroutineTree tree)
    {        
        
       //Event
        Target.GetComponent<HeroEvents>().BeforeHeroGetsHealed(Target, tree);      
        
        //Logic Start
        if(Target.GetComponent<UNHEALABLE>() != null)  //Unhealable effect
        healAmount = 0;

        if (healAmount < 0) 
        healAmount = 0;

        int newHealth = Target.GetComponent<HeroLogic>().Health + healAmount;


        if(newHealth > Target.GetComponent<HeroLogic>().BaseHealth)
        newHealth = Target.GetComponent<HeroLogic>().BaseHealth;        

        
        VisualSystem.Instance.CreateHealEffect(healAmount, Target);         

        Target.GetComponent<HeroLogic>().Health = newHealth;             
        tree.AddCurrent(Target.GetComponent<HeroLogic>().CheckDeath(tree, healAmount));

        //Event
        Target.GetComponent<HeroEvents>().AfterHeroGetsHealed(Target, tree);   

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    

    public int ComputeNewHealth(GameObject Target, int damage)
    {
        if(Target.GetComponent<DEFENSE_BREAK>() != null)
            return (Target.GetComponent<HeroLogic>().Health - damage);
        else if (Target.GetComponent<HeroLogic>().Armor > damage)
            return Target.GetComponent<HeroLogic>().Health;
        else
            return (Target.GetComponent<HeroLogic>().Health + Target.GetComponent<HeroLogic>().Armor) - damage;

    }

    public int ComputeNewArmor(GameObject Target, int damage)
    {
        if(Target.GetComponent<DEFENSE_BREAK>() != null)
            return Target.GetComponent<HeroLogic>().Armor;
        else if (Target.GetComponent<HeroLogic>().Armor > damage)
            return Target.GetComponent<HeroLogic>().Armor - damage;
        else
            return 0;
    }

    public IEnumerator EndTurn (GameObject Target, CoroutineTree tree)
    {
        tree.AddCurrent(TurnManager.Instance.EndTurn(Target, tree));
        
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    
    public virtual IEnumerator CauseBuffEffect(CoroutineTree tree)
    {
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public virtual IEnumerator UndoBuffEffect(CoroutineTree tree)
    {
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public virtual IEnumerator CauseBuffEffectEvent(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public virtual IEnumerator CauseBuffEffectEvent(GameObject Target, CoroutineTree tree)
    {
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public virtual IEnumerator CauseBuffEffectEvent(CoroutineTree tree)
    {
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public virtual IEnumerator UndoBuffEffectEvent(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public virtual IEnumerator UndoBuffEffectEvent(GameObject Target, CoroutineTree tree)
    {
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public virtual IEnumerator UndoBuffEffectEvent(CoroutineTree tree)
    {
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }
    
    public virtual IEnumerator CauseBuffEffect(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public virtual IEnumerator UndoBuffEffect(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public virtual IEnumerator CauseBuffEffect(GameObject Target, CoroutineTree tree)
    {
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public virtual IEnumerator UndoBuffEffect(GameObject Target, CoroutineTree tree)
    {
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public virtual void BuffCommandLogic()
    {

    }

    public void PostAttack()
    {
        Debug.Log("Execute PostAttack");
    }

    
    public virtual void RegisterEventEffect(){ }
    public virtual void UnRegisterEventEffect(){ }
    public virtual void CauseEventEffect(){}
    // BATTLECRY
    public virtual void WhenHeroIsPlayed(){}
    // DEATHRATTLE
    public virtual void WhenHeroDies(){}
    public virtual void RegisterCooldown(){}
    public virtual void UnregisterCooldown(){}

    void Start()
    {

    }
    
    
    
}
