using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public static AttackSystem Instance;

    public int atkDamage;

    public int finalDamage;
    

    private void Awake() {
        if (Instance == null) {
            Instance = this;            
        }else{
            Destroy(gameObject);            
        }
    }

    public bool IsHeroAlive(GameObject target)
    {
        if (target.GetComponent<HeroLogic>().Health > 0)
            return true;
        else
            return false;
    }
    
    void ClearValues()
    {
        atkDamage = 0;
    }

    public IEnumerator AttackHero (GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        ClearValues();
        
        if(Attacker.GetComponent<BERSERK>() != null)
        Target = TargetSystem.Instance.TargetRandomEnemy(Attacker);


        if(Attacker.GetComponent<CRITICAL_STRIKE>() != null || Attacker.GetComponent<CriticalStrike>() != null)  //buff and skill effect
        {
            //StartCoroutine(CriticalAttackHeroSequence(Attacker,Target));
            tree.AddCurrent(CriticalAttackHeroSequence(Attacker, Target, tree));
            
        }                  
        else
        {
            //StartCoroutine(AttackHeroSequence(Attacker,Target));            
            tree.AddCurrent(AttackHeroSequence(Attacker, Target, tree));
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;
     
    }
   

    IEnumerator AttackHeroSequence(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        
        //yield return (StartCoroutine(PreAttackSequence(Attacker,Target)));        
        tree.AddCurrent(PreAttackSequence(Attacker, Target, tree));
        
        //yield return (StartCoroutine(UseSkillSequence(Attacker, Target)));
        tree.AddCurrent(UseSkillSequence(Attacker, Target, tree));
        
        //yield return (StartCoroutine(PostAttackSequence(Attacker,Target)));
        tree.AddCurrent(PostAttackSequence(Attacker, Target, tree));
                
       

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    IEnumerator CriticalAttackHeroSequence(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {

        
        //yield return (StartCoroutine(PreAttackSequence(Attacker,Target)));        
        tree.AddCurrent(PreAttackSequence(Attacker, Target, tree));
        
        //yield return (StartCoroutine(UseCriticalAttackSkillSequence(Attacker, Target)));
        tree.AddCurrent(UseCriticalAttackSkillSequence(Attacker, Target, tree));
        
        //yield return (StartCoroutine(PostAttackSequence(Attacker,Target)));        
        tree.AddCurrent(PostAttackSequence(Attacker,Target, tree));        

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    IEnumerator PreAttackSequence(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if(IsHeroAlive(Attacker) && IsHeroAlive(Target))
        {
            //Attacker.GetComponents<SkillComponent>()[0].PreAttack(); //[0] is for testing only
            //Debug.Log("Call Pre-attack logic");

            atkDamage = ComputeDamage (Attacker);

            Attacker.GetComponent<HeroEvents>().BeforeHeroAttacks(Attacker,Target,tree);
            Target.GetComponent<HeroEvents>().BeforeHeroIsAttacked(Attacker,Target,tree);
        }      
        
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator UseSkillSequence(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {         
        //StartCoroutine(AttackTargetCoroutine(Attacker, Target, atkDamage));     
        tree.AddCurrent(AttackTargetCoroutine(Attacker, Target, atkDamage, tree));     
        
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator UseCriticalAttackSkillSequence(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {         
        if(IsHeroAlive(Attacker) && IsHeroAlive(Target))
        {
            BuffName buffName = CheckAttackModifier(Attacker, Target);

            switch(buffName)
            {
                case BuffName.CRIPPLE:
                //StartCoroutine(CrippleAttackTargetCoroutine(Attacker, Target, atkDamage));
                tree.AddCurrent(CrippleAttackTargetCoroutine(Attacker, Target, atkDamage, tree));
                break;                         

                default:
                 //StartCoroutine(CriticalAttackTargetCoroutine(Attacker, Target, atkDamage));
                 tree.AddCurrent(CriticalAttackTargetCoroutine(Attacker, Target, atkDamage, tree));
                break;
            }            
        }

        tree.CorQ.CoroutineCompleted();                      
        yield return null;
    }

    BuffName CheckAttackModifier(GameObject Attacker, GameObject Target)
    {
        if(Attacker.GetComponent<CRIPPLE>() != null)
        {          
            return BuffName.CRIPPLE;            
        }else if (Attacker.GetComponent<CRITICAL_STRIKE>() != null)
        {            
            return BuffName.CRITICAL_STRIKE;
        }else{            
            return BuffName.NULL;
        }       
    }

    

    IEnumerator PostAttackSequence(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        
        //Debug.Log("Call Post-Attack Logic");

        if(IsHeroAlive(Attacker))
        Attacker.GetComponent<HeroEvents>().AfterHeroAttacks(Attacker,Target,tree);

        if(IsHeroAlive(Target))
        Target.GetComponent<HeroEvents>().AfterHeroIsAttacked(Attacker,Target,tree);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    


    

    public int ComputeDamage (GameObject Attacker)
    {
        int damage =  Attacker.GetComponent<HeroLogic>().Attack;
        if (damage > 0)
            return damage;
        else
            return 0;
    }


    
    IEnumerator AttackTargetCoroutine(GameObject Attacker, GameObject Target, int damage, CoroutineTree tree)  
    {
        
        finalDamage = damage;
        
        //yield return StartCoroutine(BeforeAttackDamageEventCoroutine(Attacker, Target));
        tree.AddCurrent(BeforeAttackDamageEventCoroutine(Attacker, Target, tree));        

        if(IsHeroAlive(Target))   
        tree.AddCurrent(Target.GetComponent<HeroLogic>().CheckFatalDamage(tree, finalDamage));

        //yield return StartCoroutine(AttackDamageLogicCoroutine(Attacker, Target, damage));
        tree.AddCurrent(AttackDamageLogicCoroutine(Attacker, Target, finalDamage, tree));

        //yield return StartCoroutine(AfterAttackDamageEventCoroutine(Attacker, Target));
        tree.AddCurrent(AfterAttackDamageEventCoroutine(Attacker, Target, tree));

       

        tree.CorQ.CoroutineCompleted();
        yield return null;
        
    }

    IEnumerator BeforeAttackDamageEventCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        
        if(IsHeroAlive(Attacker))   
        Attacker.GetComponent<HeroEvents>().BeforeHeroDealsDamage(Attacker, tree);
            
        if(IsHeroAlive(Target))   
        Target.GetComponent<HeroEvents>().BeforeHeroTakesDamage(Target, tree);

        tree.CorQ.CoroutineCompleted();
        yield return null;        
    }

    IEnumerator AttackDamageLogicCoroutine(GameObject Attacker, GameObject Target, int damage, CoroutineTree tree)
    {
        
        damage = finalDamage;

        if(IsHeroAlive(Attacker) && IsHeroAlive(Target))
        {
  
            if(Target.GetComponent<INVINCIBLE>() != null || Target.GetComponent<SHIELD>() != null 
            || damage < 0)
                damage = 0;

            finalDamage = damage; //final damage for effect purposes such as Lifesteal              

            int newArmor = ComputeNewArmor(Target,finalDamage); 
            int newHealth = ComputeNewHealth(Target,finalDamage);
            

            //VISUALS
            VisualSystem.Instance.AttackSingleHero(Attacker,Target);    

            VisualSystem.Instance.Delay(0.7f);  //TOOD: Delays causes the animation problem during hero transition.

            VisualSystem.Instance.CreateDamageEffect(finalDamage, Target);        
            Target.GetComponent<HeroLogic>().Armor = newArmor;    //Visual:  VisualSystem.Instance.TextUpdateAnimation(GetComponent<CardManager>().ArmorObject, 
            
            Target.GetComponent<HeroLogic>().Health = newHealth;  //Visual:  VisualSystem.Instance.TextUpdateAnimation(GetComponent<CardManager>().HealthObject, health, baseHealth);
            tree.AddCurrent(Target.GetComponent<HeroLogic>().CheckDeath(tree,finalDamage));

        }           
     
        
        tree.CorQ.CoroutineCompleted();
        yield return null;        
    }

    IEnumerator AfterAttackDamageEventCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        
        if(IsHeroAlive(Attacker))   
        Attacker.GetComponent<HeroEvents>().AfterHeroDealsDamage(Attacker, tree);

        if(IsHeroAlive(Target))   
        Target.GetComponent<HeroEvents>().AfterHeroTakesDamage(Target, tree);

        tree.CorQ.CoroutineCompleted();
        yield return null;        
    }


    IEnumerator CriticalAttackTargetCoroutine(GameObject Attacker, GameObject Target, int damage, CoroutineTree tree)
    {
        finalDamage = damage;
        
        //yield return StartCoroutine(BeforeCriticalAttackEventCoroutine(Attacker, Target));
        tree.AddCurrent(BeforeCriticalAttackEventCoroutine(Attacker, Target, tree));

        if(IsHeroAlive(Target))   
        tree.AddCurrent(Target.GetComponent<HeroLogic>().CheckFatalDamage(tree, finalDamage));

        //yield return StartCoroutine(CriticalAttackTargetLogicCoroutine(Attacker, Target, damage));
        tree.AddCurrent(CriticalAttackTargetLogicCoroutine(Attacker, Target, finalDamage, tree));

        //yield return StartCoroutine(AfterCriticalAttackEventCoroutine(Attacker, Target));
        tree.AddCurrent(AfterCriticalAttackEventCoroutine(Attacker, Target, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator BeforeCriticalAttackEventCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        
        
        if(IsHeroAlive(Attacker))  
        {
            Attacker.GetComponent<HeroEvents>().BeforeHeroDealsCrit(Attacker,Target,tree);
            Attacker.GetComponent<HeroEvents>().BeforeHeroDealsDamage(Attacker, tree);
        }            
        if(IsHeroAlive(Target))
        {
           Target.GetComponent<HeroEvents>().BeforeHeroReceivesCrit(Attacker,Target,tree);
           Target.GetComponent<HeroEvents>().BeforeHeroTakesDamage(Target, tree);
        }    

       

        tree.CorQ.CoroutineCompleted();        
        yield return null;
    }

    IEnumerator CriticalAttackTargetLogicCoroutine(GameObject Attacker, GameObject Target, int damage, CoroutineTree tree)
    {
        damage = finalDamage;

        if(IsHeroAlive(Attacker) && IsHeroAlive(Target))
        {
            
            int criticalStrikeMultiplier = Attacker.GetComponent<HeroLogic>().criticalStrikeFactor;             
            damage = damage*criticalStrikeMultiplier;

            if(Target.GetComponent<INVINCIBLE>() != null || Target.GetComponent<SHIELD>() != null 
            || Target.GetComponent<EVASION>() != null || damage < 0)
                damage = 0;

            finalDamage = damage;       //final damage for effect purposes such as Lifesteal

            int newArmor = ComputeNewArmor(Target,damage); 
            int newHealth = ComputeNewHealth(Target,damage);
           
            //VISUALS            
            VisualSystem.Instance.AttackSingleHero(Attacker,Target);     

            //VisualSystem.Instance.CreateFloatingText("CRITICAL STRIKE", Attacker, Color.yellow);

            VisualSystem.Instance.Delay(0.7f);

            VisualSystem.Instance.CreateFloatingText("Critical Strike", Attacker, Color.yellow);
           
            VisualSystem.Instance.GenericSkillEffectAnimation(Target, GenericSkillName.CRITICAL_STRIKE);
            VisualSystem.Instance.CreateDamageEffect(damage, Target);                   
            Target.GetComponent<HeroLogic>().Armor = newArmor;               

             Target.GetComponent<HeroLogic>().Health = newHealth;
             tree.AddCurrent(Target.GetComponent<HeroLogic>().CheckDeath(tree,damage));
            
        }
        
        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator AfterCriticalAttackEventCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if(IsHeroAlive(Attacker))
        {
            Attacker.GetComponent<HeroEvents>().AfterHeroDealsCrit(Attacker,Target,tree);  //call reduce counters of CritStrike and Cripple here            
            Attacker.GetComponent<HeroEvents>().AfterHeroDealsDamage(Attacker, tree);                     
        }      
        if(IsHeroAlive(Target))
        {
            Target.GetComponent<HeroEvents>().AfterHeroTakesCrit(Attacker,Target,tree);
            Target.GetComponent<HeroEvents>().AfterHeroTakesDamage(Target, tree);
        }    
        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }


    

    IEnumerator CrippleAttackTargetCoroutine(GameObject Attacker, GameObject Target, int damage, CoroutineTree tree)
    {
        finalDamage = damage;
        
        //yield return BeforeCrippleAttackEventCoroutine(Attacker, Target);        
        tree.AddCurrent(BeforeCrippleAttackEventCoroutine(Attacker, Target, tree));        

         if(IsHeroAlive(Target))   
        tree.AddCurrent(Target.GetComponent<HeroLogic>().CheckFatalDamage(tree, finalDamage));
        
        //yield return CrippleAttackLogicCoroutine(Attacker, Target, damage);
        tree.AddCurrent(CrippleAttackLogicCoroutine(Attacker, Target, finalDamage, tree));

        //yield return AfterCrippleAttackEventCoroutine(Attacker, Target);
        tree.AddCurrent(AfterCrippleAttackEventCoroutine(Attacker, Target, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator BeforeCrippleAttackEventCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if(IsHeroAlive(Attacker))        
        Attacker.GetComponent<HeroEvents>().BeforeHeroDealsDamage(Attacker, tree);
        if(IsHeroAlive(Target))        
        Target.GetComponent<HeroEvents>().BeforeHeroTakesDamage(Target, tree);   

        if(IsHeroAlive(Target))   
        tree.AddCurrent(Target.GetComponent<HeroLogic>().CheckFatalDamage(tree, finalDamage));    

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator CrippleAttackLogicCoroutine(GameObject Attacker, GameObject Target, int damage, CoroutineTree tree)
    {   
        damage = finalDamage;

        if(IsHeroAlive(Attacker) && IsHeroAlive(Target))
        {

            
            
            if(Target.GetComponent<INVINCIBLE>() != null || Target.GetComponent<SHIELD>() != null 
            || damage < 0)
                damage = 0;

            finalDamage = damage; //final damage for effect purposes such as Lifesteal              

            int newArmor = ComputeNewArmor(Target,damage); 
            int newHealth = ComputeNewHealth(Target,damage);
            

            //VISUALS
            VisualSystem.Instance.AttackSingleHero(Attacker,Target);    

            VisualSystem.Instance.Delay(0.7f);  //TOOD: Delays causes the animation problem during hero transition.

            VisualSystem.Instance.CreateDamageEffect(damage, Target);        
            Target.GetComponent<HeroLogic>().Armor = newArmor;    //Visual:  VisualSystem.Instance.TextUpdateAnimation(GetComponent<CardManager>().ArmorObject, 
            
            Target.GetComponent<HeroLogic>().Health = newHealth;  //Visual:  VisualSystem.Instance.TextUpdateAnimation(GetComponent<CardManager>().HealthObject, health, baseHealth);
            tree.AddCurrent(Target.GetComponent<HeroLogic>().CheckDeath(tree,damage));
        }           

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator AfterCrippleAttackEventCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if(IsHeroAlive(Attacker))
        {
            Attacker.GetComponent<HeroEvents>().AfterHeroDealsCrippleAttack(Attacker,Target,tree);        
            Attacker.GetComponent<HeroEvents>().AfterHeroDealsDamage(Attacker, tree);       
            //CrippleStrikeAnimation(Attacker, Target, GenericSkillName.CRIPPLE);
            VisualSystem.Instance.CreateFloatingText("CRIPPLED", Attacker, Color.magenta);
            VisualSystem.Instance.GenericSkillEffectAnimation(Target, GenericSkillName.CRIPPLE);
        }
        if(IsHeroAlive(Target))        
         Target.GetComponent<HeroEvents>().AfterHeroTakesDamage(Target, tree);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator DealDamage (GameObject Attacker, GameObject Target, int damage, CoroutineTree tree)
    {        
        //StartCoroutine(DealDamageCoroutine(Attacker, Target, damage));        
        tree.AddCurrent(DealDamageCoroutine(Attacker, Target, damage, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator DealDamageCoroutine(GameObject Attacker, GameObject Target, int damage, CoroutineTree tree)
    {
       
        //yield return StartCoroutine(BeforeDealDamageCoroutine(Attacker, Target));
        tree.AddCurrent(BeforeDealDamageCoroutine(Attacker, Target, tree));

         if(IsHeroAlive(Target))   
        tree.AddCurrent(Target.GetComponent<HeroLogic>().CheckFatalDamage(tree, damage));        
        
        //yield return StartCoroutine(DealDamageLogicCoroutine(Attacker, Target, damage));
        tree.AddCurrent(DealDamageLogicCoroutine(Attacker, Target, finalDamage, tree));

        //yield return StartCoroutine(AfterDealDamageCoroutine(Attacker, Target));
        tree.AddCurrent(AfterDealDamageCoroutine(Attacker, Target, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator BeforeDealDamageCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if(IsHeroAlive(Attacker))   
            Attacker.GetComponent<HeroEvents>().BeforeHeroDealsDamage(Attacker, tree);
        if(IsHeroAlive(Target))   
            Target.GetComponent<HeroEvents>().BeforeHeroTakesDamage(Target, tree);

        

        tree.CorQ.CoroutineCompleted();    
        yield return null;
    }

    IEnumerator DealDamageLogicCoroutine(GameObject Attacker, GameObject Target, int damage, CoroutineTree tree)
    {
        
       damage = finalDamage;

        Debug.Log("Deal Damage: " +damage);
        
        if(IsHeroAlive(Attacker) && IsHeroAlive(Target))
        {
            
            if(Target.GetComponent<INVINCIBLE>() != null || Target.GetComponent<SHIELD>() != null 
            || Target.GetComponent<EVASION>() != null || damage < 0)
                damage = 0;

            finalDamage = damage;       

            int newArmor = ComputeNewArmor(Target,damage);  
            int newHealth = ComputeNewHealth(Target,damage);

            VisualSystem.Instance.CreateDamageEffect(damage, Target);     

            Target.GetComponent<HeroLogic>().Armor = newArmor;    
            
            Target.GetComponent<HeroLogic>().Health = newHealth;
            tree.AddCurrent(Target.GetComponent<HeroLogic>().CheckDeath(tree,damage));
            
        }        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator AfterDealDamageCoroutine(GameObject Attacker, GameObject Target, CoroutineTree tree)
    {
        if(IsHeroAlive(Attacker))   
            Attacker.GetComponent<HeroEvents>().AfterHeroDealsDamage(Attacker, tree);
        if(IsHeroAlive(Target))   
            Target.GetComponent<HeroEvents>().AfterHeroTakesDamage(Target, tree);

        tree.CorQ.CoroutineCompleted();    
        yield return null;
    }

    public IEnumerator DealDamageNoAttacker (GameObject Target, int damage, CoroutineTree tree)
    {        
        //StartCoroutine(DealDamageCoroutine(Attacker, Target, damage));        
        tree.AddCurrent(DealDamageCoroutineNoAttacker(Target, damage, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator DealDamageCoroutineNoAttacker(GameObject Target, int damage, CoroutineTree tree)
    {
        
        finalDamage = damage;
        
        //yield return StartCoroutine(BeforeDealDamageCoroutine(Attacker, Target));
        tree.AddCurrent(BeforeDealDamageCoroutineNoAttacker(Target, tree));

        if(IsHeroAlive(Target))   
        tree.AddCurrent(Target.GetComponent<HeroLogic>().CheckFatalDamage(tree, finalDamage));
        
        //yield return StartCoroutine(DealDamageLogicCoroutine(Attacker, Target, damage));
        tree.AddCurrent(DealDamageLogicCoroutineNoAttacker(Target, damage, tree));

        //yield return StartCoroutine(AfterDealDamageCoroutine(Attacker, Target));
        tree.AddCurrent(AfterDealDamageCoroutineNoAttacker(Target, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator BeforeDealDamageCoroutineNoAttacker(GameObject Target, CoroutineTree tree)
    {
        
        if(IsHeroAlive(Target))   
            Target.GetComponent<HeroEvents>().BeforeHeroTakesDamage(Target, tree);

        tree.CorQ.CoroutineCompleted();    
        yield return null;
    }

    IEnumerator DealDamageLogicCoroutineNoAttacker(GameObject Target, int damage, CoroutineTree tree)
    {
        damage = finalDamage;
        
        if(IsHeroAlive(Target))
        {
            
            if(Target.GetComponent<INVINCIBLE>() != null || Target.GetComponent<SHIELD>() != null 
            || Target.GetComponent<EVASION>() != null || damage < 0)
                damage = 0;

            finalDamage = damage;       

            int newArmor = ComputeNewArmor(Target,damage);  
            int newHealth = ComputeNewHealth(Target,damage);

            VisualSystem.Instance.CreateDamageEffect(damage, Target);     

            Target.GetComponent<HeroLogic>().Armor = newArmor;    
            
            Target.GetComponent<HeroLogic>().Health = newHealth;




            tree.AddCurrent(Target.GetComponent<HeroLogic>().CheckDeath(tree,damage));
            
        }        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator AfterDealDamageCoroutineNoAttacker(GameObject Target, CoroutineTree tree)
    {       
        if(IsHeroAlive(Target))   
            Target.GetComponent<HeroEvents>().AfterHeroTakesDamage(Target, tree);

        tree.CorQ.CoroutineCompleted();    
        yield return null;
    }



    public IEnumerator LoseLife (GameObject Attacker, GameObject Target, int damage, CoroutineTree tree) 
    {                
        if(IsHeroAlive(Attacker) && IsHeroAlive(Target))
        {
            
            if(damage < 0)
            damage = 0;

            string lifeFloatingText = "LOSE LIFE";
  
            //VISUALS
            VisualSystem.Instance.CreateFloatingText(lifeFloatingText, Target, Color.magenta);
            VisualSystem.Instance.CreateDamageEffect(damage, Target);   

            Target.GetComponent<HeroLogic>().Health -= damage;            
            tree.AddCurrent(Target.GetComponent<HeroLogic>().CheckDeath(tree,damage));
            
                  
        }        

         tree.CorQ.CoroutineCompleted();
         yield return null;
    }

    public IEnumerator LoseLife(GameObject Target, int damage, CoroutineTree tree)  
    {
        
        if(IsHeroAlive(Target))
        {
           
            if(damage < 0)
            damage = 0;

            //VISUALS
            //string lifeFloatingText = "LOSE LIFE";           
            //VisualSystem.Instance.CreateFloatingText(lifeFloatingText, Target, Color.magenta);

            
            VisualSystem.Instance.CreateDamageEffect(damage, Target);               
            Target.GetComponent<HeroLogic>().Health -= damage;     
            tree.AddCurrent(Target.GetComponent<HeroLogic>().CheckDeath(tree,damage));       
                  
        }           

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


}
