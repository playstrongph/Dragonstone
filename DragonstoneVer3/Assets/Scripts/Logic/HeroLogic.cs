using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public enum HeroProperties
{
    HEALTH,
    ATTACK,
    ARMOR,
    SPEED,
    CHANCE,    
    ENERGY
}

public class HeroLogic : MonoBehaviour
{
    public HeroAsset heroAsset;
    public int id;

 
    public int health;

    [SerializeField]
    int attack;

    //public int currentAttack;

    [SerializeField]
    int armor;

    [SerializeField]
    float energy;

    [SerializeField]
    int speed;

    //public int currentSpeed;

    [SerializeField]
    int chance;

    
    public HeroTimer heroTimer;
    
    //public int currentChance;


    //[HideInInspector]
    public int criticalStrikeFactor = 2;

    [HideInInspector]
    public int invincibilityFactor = 1;

    [HideInInspector]
    public int shieldFactor = 1;

    [HideInInspector]
    public int evasionFactor = 1;

    //public bool takeExtraTurn = false;

    public bool extraTurn = false;


    public int baseHealth;  
    public int baseArmor; 
    public int baseAttack;   
    public int baseSpeed;    
    public int baseChance;

    [HideInInspector]
    public int additionalDebuffCounters = 0;    

    

    public int Attack
    {
        get
        {
            
            int total = 0;
            int attackPlus = 0;
            int attackMinus = 0;
            
            if(GetComponent<ATTACK_UP>() != null)
            attackPlus = GetComponent<ATTACK_UP>().attackPlus;

            if(GetComponent<ATTACK_DOWN>() != null)
            attackMinus = GetComponent<ATTACK_DOWN>().attackMinus;

            total = attackPlus + attackMinus;

            int getAttack = baseAttack + total;
            if(getAttack < 0) getAttack = 0;
            return getAttack;

            
        }
        set
        {
            attack = value;
            int setAttack = attack;
            if(setAttack < 0) setAttack = 0;
            VisualSystem.Instance.TextUpdateAnimation(GetComponent<CardManager>().AttackObject, setAttack, baseAttack);

            
        }
    }

    

    public int BaseAttack    
    {
        get {return baseAttack;}        
        set
        {
            baseAttack = value;
            if(baseAttack < 0) baseAttack = 0;
            //can't have a negative base attack            
        }        
        
    }

   

    public int Armor
    {
        get
        {
            int getArmor = armor;
            if(getArmor < 0) getArmor = 0;
            return getArmor;
            
            //getStat preserves the actual value (can be negative) of the current stat
            
        }
        set
        {
            armor = value;            
            if(armor < 0) armor = 0;
            VisualSystem.Instance.TextUpdateAnimation(GetComponent<CardManager>().ArmorObject, 
            armor, armor);            
            
            //setStat preserves the actual value (can be negative) of the current stat
        }
    } 

    //Do NOT use in Turnmanager update hero timers, unless total revamp
    public float Energy   //Energy specified in terms of percentage (no decimal)  Exmample - 35
    {
        get
        {
            energy = heroTimer.timerValue;             
            

            float getEnergy = Mathf.FloorToInt(energy*100/GlobalSettings.Instance.timerFull);

            if(getEnergy < 0 ) energy = 0f;
            return getEnergy;

            //getStat preserves the actual value (can be negative) of the current stat
        }
        set
        {           
            //Set Energy is specified in percentage*100.  Eg:  50, 75

            energy = value;     
            float setEnergy = energy;
            if(energy < 0) energy =  0f;
            if(setEnergy < 0) setEnergy =  0f;

            heroTimer.timerValue = setEnergy*GlobalSettings.Instance.timerFull/100;

            heroTimer.timerValuePercentage = Mathf.FloorToInt(heroTimer.timerValue*100/GlobalSettings.Instance.timerFull);
            
            VisualSystem.Instance.UpdateSingleHeroTimer(this);
            

            //setStat preserves the actual value (can be negative) of the current stat
            
        }
    } 
    public int Chance
    {
        get
        {
            int total = 0;
            int chancePlus = 0;
            int chanceMinus = 0;

            if(GetComponent<LUCKY>() != null)
            chancePlus = GetComponent<LUCKY>().chancePlus;

            if(GetComponent<UNLUCKY>() != null)
            chanceMinus = GetComponent<UNLUCKY>().chanceMinus;

            total = chancePlus + chanceMinus;
           
           
            // foreach (ChanceModifier modifier in GetComponents<ChanceModifier>().ToList())
            // {
            //     total += modifier.amount;
            // }



            int getChance = baseChance + total;
            if(getChance < 0) getChance = 0;
            return getChance;

            //getStat preserves the actual value (can be negative) of the current stat
        }
        set
        {
            chance = value;   
            int setChance = chance;
            if(setChance < 0) setChance = 0;
            
            //setStat preserves the actual value (can be negative) of the current stat
        }
    }

    public int BaseChance    
    {
        get {return baseChance;}        
        set
        {
            baseChance = value;
            if(baseChance < 0) baseChance = 0;
            //can't have a negative base chance            
        }        
        
    }

    public int Health
    {
        get
        {                
            return health;

        }
        set
        {
            health = value;
            if(health > baseHealth) health = baseHealth;            
            VisualSystem.Instance.TextUpdateAnimation(GetComponent<CardManager>().HealthObject, health, baseHealth);
            
            
            ///<TODO>:  Remove this and replace with CheckDeath
            // if (health <= 0)
            // {   
            //     CoroutineTree tree = new CoroutineTree();
            //     HeroDies(tree);   
            // }
             
                          
          
        }
    }

    public IEnumerator CheckDeath(CoroutineTree tree, int damage)
    {
         if (Health <= 0)
         tree.AddCurrent(HeroDiesCoroutine(tree));
         
         
        //  if (Health <= 0)
        //     {  

        //         if(GetComponent<PreventFatalDamageSkillEffect>() !=null)
        //         {
        //             tree.AddCurrent(GetComponent<PreventFatalDamageSkillEffect>().UseSkillEffect(this.gameObject, tree));
        //             heroEvents.AfterHeroPreventsFatalDamage(this.gameObject, tree);

        //         }else               
        //         tree.AddCurrent(HeroDiesCoroutine(tree));
        //     }
         
         tree.CorQ.CoroutineCompleted();
         yield return null;

    }

    public IEnumerator CheckFatalDamage(CoroutineTree tree, int damage)
    {         
         int totalHealth = Health + Armor;
         if (damage > totalHealth)
            {  
                if(GetComponent<PreventFatalDamageSkillEffect>() !=null)
                {
                    tree.AddCurrent(GetComponent<PreventFatalDamageSkillEffect>().UseSkillEffect(this.gameObject, tree));
                    AttackSystem.Instance.finalDamage = 0;
                    heroEvents.AfterHeroPreventsFatalDamage(this.gameObject, tree);                    
                }

            }else
                AttackSystem.Instance.finalDamage = damage;
         
         tree.CorQ.CoroutineCompleted();
         yield return null;
    }



    public int BaseHealth
    {
        get {return baseHealth;}        
        set
        {
            baseHealth = value;
            if(baseHealth < 0) baseHealth = 0;
        }        
        
    }

    public int Speed
    {
        get
        {
            int total = 0;
            int speedPlus = 0;
            int speedMinus = 0;

            if(GetComponent<HASTE>() != null)
            speedPlus = GetComponent<HASTE>().speedPlus;

            if(GetComponent<SLOW>() != null)
            speedMinus = GetComponent<SLOW>().speedMinus;

            total = speedPlus + speedMinus;

            // foreach (SpeedModifier modifier in GetComponents<SpeedModifier>().ToList())
            // {
            //     total += modifier.amount;
            // }
            //currentSpeed = baseSpeed + total;


            int getSpeed = baseSpeed + total;
            if(getSpeed < 0) getSpeed = 0;
            return getSpeed;
            
            //getStat preserves the actual value (can be negative) of the current stat
        }
        set
        {
            speed = value;   
            int setSpeed = speed;
            if(setSpeed < 0) setSpeed = 0;    
            VisualSystem.Instance.ImageUpdateAnimation(GetComponent<CardManager>().EnergyObject, setSpeed, baseSpeed);

            //setStat preserves the actual value (can be negative) of the current stat     
        }
    }

    public int BaseSpeed    
    {
        get {return baseSpeed;}        
        set
        {
            baseSpeed = value;
            if(baseSpeed < 0) baseSpeed = 0;
            //can't have a negative base speed
        }        
        
    }
    

    //these flags will be more complicated soon. consider using prop or return method
    public bool isActive = false;
    public bool isValidTarget = false;
    //public bool criticalStrike = true;
    //public bool criticalStrike = false;

    public HeroEvents heroEvents;

    public List<SkillComponent> skills;

    public List<BuffComponent> buffs;


    public void ReadCreatureFromAsset()   //Note:  Don't use property here
    {
        health = heroAsset.hero.health;
        baseHealth = heroAsset.hero.health;

        attack = heroAsset.hero.attack;
        baseAttack = heroAsset.hero.attack;
        //currentAttack = heroAsset.hero.attack;

        speed = heroAsset.hero.speed;
        baseSpeed = heroAsset.hero.speed;
        //currentSpeed = heroAsset.hero.speed;

        chance = heroAsset.hero.chance;
        baseChance = heroAsset.hero.chance;
        //currentChance = heroAsset.hero.chance;

        armor = heroAsset.hero.armor;
        baseArmor = heroAsset.hero.armor;

        energy = 0;

        // if(heroAsset.hero.taunt)
        // {
        //     gameObject.AddComponent<Taunt>();
        // }
       
    }

    public void RestoreHeroToBaseStats()
    {
        Health = baseHealth;               

        Attack = heroAsset.hero.attack;       
        //currentAttack = heroAsset.hero.attack;

        Speed = heroAsset.hero.speed;        
        //currentSpeed = heroAsset.hero.speed;

        Chance = heroAsset.hero.chance;
        //currentChance = heroAsset.hero.chance;

        Armor = heroAsset.hero.armor;        

        // if(heroAsset.hero.taunt)   //Moved Taunt as Passive Skill
        // {
        //     gameObject.AddComponent<Taunt>();
        // }
       
    }

    public void InitBasicAttack()
    {
        BasicAttack ba = gameObject.AddComponent<BasicAttack>();
        ba.baseCoolDown = 0;
        ba.currCoolDown = 0;
        ba.heroLogic = this;
        ba.heroEvents = GetComponent<HeroEvents>();
        ba.skillTarget = SkillTarget.ENEMY;
        ba.dragType = DragType.BASIC_ATTACK;
        ba.SubscribeToEvents();
    }

    public void InitSkills(GameObject hero)
    {
        GameObject go;
        
        if(gameObject.tag == "TOP_PLAYER")
            {
                go = Instantiate(GlobalSettings.Instance.SkillPanelPrefab, GlobalSettings.Instance.TopSkillPanelTransform.transform);
                go.name = hero.name +"SkillPanel";
            }
         
        else
            {
                go = Instantiate(GlobalSettings.Instance.SkillPanelPrefab, GlobalSettings.Instance.SkillPanelTransform.transform);  
                go.name = hero.name +"SkillPanel";
            }
          

        GetComponent<CardManager>().SkillPanel = go;

        HeroAsset ha = heroAsset;

        for(int j=0; j<ha.skill.Count; j++)
            {
                //this is for decoupled skill go and component
                GameObject skillCard = Instantiate(go.GetComponent<SkillPanelManager>().SkillCardPrefab, go.GetComponent<SkillPanelManager>().SkillPanel.transform);
                //set skill game object tag to the same as its hero's
                skillCard.tag = gameObject.tag;
                SkillComponent skillComponent = ha.skill[j].skillAsset.Use(skillCard);
                

                skillCard.name = skillComponent.skillAsset.skillBasicInfo.skillName;
                
                //Original
                //skillComponent.coolDown = ha.skill[j].cooldown;

                //info should come from the skill's Scriptable Object
                skillComponent.baseCoolDown = skillComponent.skillAsset.skillBasicInfo.skillCooldown;
                skillComponent.currCoolDown = skillComponent.baseCoolDown;
                skillComponent.heroLogic = this;
                skillComponent.heroEvents = GetComponent<HeroEvents>();
                skillComponent.skillTarget = skillComponent.skillAsset.skillBasicInfo.skillTarget;
                skillComponent.skillType = skillComponent.skillAsset.skillBasicInfo.skillType;
                skillComponent.dragType = skillComponent.skillAsset.skillBasicInfo.dragType;
                skillComponent.SubscribeToEvents();
                skills.Add(skillComponent);


                //load the asset to SCM for visuals
                skillComponent.GetComponent<SkillCardManager>().skillAsset = ha.skill[j].skillAsset;
                skillComponent.GetComponent<SkillCardManager>().ReadFromAsset();
                switch (skillComponent.dragType)
                {
                    case DragType.SKILL_ATTACK:
                        skillComponent.GetComponent<SkillCardManager>().target.AddComponent<DragHeroSkill>();
                        break;
                    
                }
                if(skillComponent.dragType != DragType.NONE)
                skillComponent.GetComponent<SkillCardManager>().target.AddComponent<Draggable>();
                

 
            }

        GetComponent<CardManager>().SkillPanel.SetActive(false);
        
        GameObject heroPanel;

        if(gameObject.tag == "TOP_PLAYER")
            heroPanel = Instantiate(GlobalSettings.Instance.HeroPanelPrefab, GlobalSettings.Instance.TopHeroPanelTransform.transform);
        else
            heroPanel = Instantiate(GlobalSettings.Instance.HeroPanelPrefab, GlobalSettings.Instance.LowHeroPanelTransform.transform);

        GetComponent<CardManager>().HeroPanel = heroPanel;

        heroPanel.GetComponent<CardManager>().heroPanelImage.sprite = heroAsset.hero.heroImage;        


        GetComponent<CardManager>().HeroPanel.SetActive(false);


    }

    

    public IEnumerator SetHeroActiveCoroutine(CoroutineTree tree)
    {
        //yield return StartCoroutine(SetHeroActiveLogicCoroutine());
        tree.AddCurrent(SetHeroActiveLogicCoroutine(tree));       
        
        //yield return StartCoroutine(SetHeroActiveAfterHeroStartEventCoroutine());
        tree.AddCurrent(SetHeroActiveAfterHeroStartEventCoroutine(tree));   

        //yield return StartCoroutine(SetHeroActiveAfterHeroStartEventBuffsCoroutine());
        tree.AddCurrent(SetHeroActiveAfterHeroStartEventBuffsCoroutine(tree));        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator SetHeroActiveLogicCoroutine(CoroutineTree tree)
    {
                isActive = true;

                TurnManager.Instance.ResetHeroTimer(this); ///<TODO>: Convert to IEnumerator if there are problems

                VisualSystem.Instance.ClearAllGlows();                       
                VisualSystem.Instance.SetActiveHero(this);           

                if(this.gameObject.GetComponent<STUN>() != null || this.gameObject.GetComponent<SLEEP>() != null ){}else
                NormalReduceSkillsCooldown();  ///<TODO>: Convert to IEnumerator if there are problems

                TargetSystem.Instance.FindValidTargets(this);  ///<TODO>: Convert to IEnumerator if there are problems

                tree.CorQ.CoroutineCompleted();
                yield return null;
    }

    IEnumerator SetHeroActiveAfterHeroStartEventCoroutine(CoroutineTree tree)
    {
        if(this.gameObject.GetComponent<STUN>() != null || this.gameObject.GetComponent<SLEEP>() != null){}else
        heroEvents.AfterHeroStartTurn(this.gameObject, tree); 


        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator SetHeroActiveAfterHeroStartEventBuffsCoroutine(CoroutineTree tree)
    {
        if(this.gameObject.GetComponent<STUN>() != null || this.gameObject.GetComponent<SLEEP>() != null){}else
        heroEvents.AfterHeroStartTurnBuffs(this.gameObject, tree);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }


    public void SetHeroInactive(CoroutineTree tree)
    {        
        StartCoroutine(SetHeroInactiveCoroutine(tree));
    }

    IEnumerator SetHeroInactiveCoroutine(CoroutineTree tree)
    {
        yield return StartCoroutine(SetHeroInactiveLogic());

        yield return StartCoroutine(SetHeroInactiveEvents(tree));
        
        yield return null;
    }

    IEnumerator SetHeroInactiveLogic()
    {
        isActive = false;
        VisualSystem.Instance.SetHeroInactive(this);
        yield return null;
    }

    IEnumerator SetHeroInactiveEvents(CoroutineTree tree)
    {
        heroEvents.AfterHeroEndTurn(this.gameObject, tree);
        yield return null;
    }


    

    public void NormalReduceSkillsCooldown()
    {
        
        // if !silence        
        //SkillComponent[] skills = gameObject.GetComponents<SkillComponent>();       

        for (int i=0; i<skills.Count; i++)
        {
            if(skills[i].skillType != SkillType.PASSIVE)
            skills[i].NormalReduceSkillCooldown();
            
        }
    }

    
    // public void HeroDies(CoroutineTree tree)  
    // {
    //     //StartCoroutine(HeroDiesCoroutine());
    //     tree.Start();
    //     tree.AddRoot(HeroDiesCoroutine(tree));
        
        
    // }

    IEnumerator HeroDiesCoroutine(CoroutineTree tree)
    {
        tree.AddCurrent((HeroDiesLogic(tree)));

        //yield return StartCoroutine(HeroDiesEvent());
        tree.AddCurrent((HeroDiesEvent(tree)));

        //yield return StartCoroutine(HeroDiesResurrectCheck());
        tree.AddCurrent((HeroDiesResurrectCheck(tree)));

        tree.CorQ.CoroutineCompleted();
        yield return null;

        
    }

    IEnumerator HeroDiesLogic(CoroutineTree tree)
    {
        
        tree.AddCurrent(BuffSystem.Instance.RemoveAllBuffsOfTypeWithException(this.gameObject, BuffType.BUFF, BuffName.RESURRECT,tree));  
        tree.AddCurrent(BuffSystem.Instance.RemoveAllBuffsOfType(this.gameObject, BuffType.DEBUFF,tree));        

        tree.AddCurrent(DisableSkills(tree));  

        //Set energy to Zero
        Energy = 0f;       //Remove        
        TurnManager.Instance.DeactivateHeroTimer(gameObject);
        HeroManager.Instance.AddHeroToDeadHeroesList(gameObject);  

        //Animation
        VisualSystem.Instance.HeroDiesAnimation(gameObject);

        

         tree.CorQ.CoroutineCompleted();
         yield return null;
        

        
    }    

    IEnumerator HeroDiesEvent(CoroutineTree tree)
    {

        //may need to insert delay hero for herodies animation = 1.5secs    
        heroEvents.AfterHeroDies(this.gameObject, tree);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator HeroDiesResurrectCheck(CoroutineTree tree)
    {
       
        if(GetComponent<RESURRECT>() != null)
        {
            GetComponent<RESURRECT>().buffCounter = 0;

            //ResurrectHero();
            tree.AddCurrent(ResurrectHeroCoroutine(tree));
        }

        if(isActive)              
        tree.AddCurrent(TurnManager.Instance.EndTurn(this.gameObject, tree));  
        //new EndTurnCommand(this.gameObject).AddToQueue();

        tree.CorQ.CoroutineCompleted();

        yield return null;
    }

    // public void ResurrectHero()
    // {   
    //     StartCoroutine(ResurrectHeroCoroutine());

    // }   

    public IEnumerator ResurrectHeroCoroutine(CoroutineTree tree)
    {
        //yield return StartCoroutine(ResurrectHeroLogic());
        tree.AddCurrent(ResurrectHeroLogic(tree));
        
        //yield return StartCoroutine(ResurrectHeroEvent());
        tree.AddCurrent(ResurrectHeroEvent(tree));

        tree.CorQ.CoroutineCompleted();
         
        yield return null;
    }

    IEnumerator ResurrectHeroLogic(CoroutineTree tree)
    {
        HeroManager.Instance.RemoveHeroFromDeadHeroesList(gameObject);
        TurnManager.Instance.ReactivateHeroTimer(gameObject);

        tree.AddCurrent(BuffSystem.Instance.RemoveAllBuffsOfType(this.gameObject, BuffType.BUFF, tree));
        tree.AddCurrent(BuffSystem.Instance.RemoveAllBuffsOfType(this.gameObject, BuffType.DEBUFF,tree));
       
        ///<TODO> Keep as method first, convert to IEnum when required
        RestoreSkills();

        //TO DO: Collapse this into 1 animation       
        
        // Sequential Animation  
        VisualSystem.Instance.Delay(0.5f);  //To allow for Die Animaiton
        VisualSystem.Instance.ResurrectHeroAnimation(this.gameObject, GenericSkillName.RESURRECT); 

        tree.CorQ.CoroutineCompleted();    

        yield return null;
    }

    IEnumerator ResurrectHeroEvent(CoroutineTree tree)
    {
        
        heroEvents.AfterHeroRevives(this.gameObject, tree);        

        tree.CorQ.CoroutineCompleted();      
        yield return null;          
        //Command.CommandExecutionComplete();
    }

    



    IEnumerator DisableSkills(CoroutineTree tree)
    {
        foreach (SkillComponent sc in skills)
        {
            sc.UnregisterCooldown();

            sc.UnRegisterEventEffect();
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    void RestoreSkills()
    {
        foreach (SkillComponent sc in skills)
        {
            sc.RegisterCooldown();
            sc.RegisterEventEffect();
        }

    }

    public void RestoreHeroBaseStats()
    {
        this.ReadCreatureFromAsset();

        this.gameObject.GetComponent<CardManager>().ReadCreatureFromAsset();

        GetComponent<CardManager>().SkillPanel.SetActive(false);

 
    }

    

    public virtual bool ChanceOK ()
    {
        if(Random.Range(0,100)<=chance)
            return true;

        else
            return false;
    }

    
    public void IncreaseArmor (int amount)
    {        
        Armor += amount;
    }

    public void DecreaseArmor (int amount)
    {        
        Armor -= amount;
    }

    public void IncreaseBaseAttack(int amount)
    {
        BaseAttack += amount;

        int attack = Attack;
        Attack = attack;
    }

    public void DecreaseBaseAttack(int amount)
    {
        BaseAttack -= amount;

        int attack = Attack;
        Attack = attack;
    }

    public void IncreaseBaseHealth(int amount)
    {
        BaseHealth += amount;
        Health += amount;
    }

    public void DecreaseBaseHealth(int amount)
    {
        BaseHealth -= amount;
        Health -= amount;
        
    }

    public void IncreaseBaseChance(int amount)
    {
        BaseChance += amount;

        int chance = Chance;
        Chance = chance;
    }

    public void DecreaseBaseChance(int amount)
    {
        BaseChance -= amount;

        int chance = Chance;
        Chance = chance;
    }

    public void IncreaseBaseSpeed(int amount)
    {
        BaseSpeed += amount;

        int speed = Speed;
        Speed = speed;
    }

    public void DecreaseBaseSpeed(int amount)
    {
        BaseSpeed -= amount;

        int speed = Speed;
        Speed = speed;
    }


    public IEnumerator IncreaseEnergy(float amount, CoroutineTree tree)
    {
        

        tree.AddCurrent(IncreaseEnergyCoroutine(amount, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
                
    }

    IEnumerator IncreaseEnergyCoroutine(float amount, CoroutineTree tree)
    {
        tree.AddCurrent(BeforeIncreaseEnergyEvent(amount, tree));

        tree.AddCurrent(IncreaseEnergyLogic(amount,tree));

        tree.AddCurrent(AfterIncreaseEnergyEvent(amount, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator BeforeIncreaseEnergyEvent(float amount, CoroutineTree tree)
    {
        heroEvents.BeforeHeroGainsEnergy(this.gameObject, tree);    

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator IncreaseEnergyLogic(float amount, CoroutineTree tree)
    {
        Energy += amount;
        string energyFloatingText = "+" +amount +" ENERGY";
        VisualSystem.Instance.ImageUpdateAnimation(GetComponent<CardManager>().EnergyObject, 0, 0);
        VisualSystem.Instance.CreateFloatingText(energyFloatingText, this.gameObject, Color.yellow);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator AfterIncreaseEnergyEvent(float amount, CoroutineTree tree)
    {
        heroEvents.AfterHeroGainsEnergy(this.gameObject, tree);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator DecreaseEnergy(float amount, CoroutineTree tree)
    {        
        StartCoroutine(DecreaseEnergyCoroutine(amount, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator DecreaseEnergyCoroutine(float amount, CoroutineTree tree)
    {
        tree.AddCurrent(BeforeDecreaseEnergyEvent(amount, tree));
        tree.AddCurrent(DecreaseEnergyLogic(amount,tree));
        tree.AddCurrent(AfterDecreaseEnergyEvent(amount, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator BeforeDecreaseEnergyEvent(float amount, CoroutineTree tree)
    {
        heroEvents.BeforeHeroLosesEnergy(this.gameObject, tree);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator DecreaseEnergyLogic(float amount,CoroutineTree tree)
    {
        Energy -= amount;
        string energyFloatingText = "-" +amount +" ENERGY";
        VisualSystem.Instance.ImageUpdateAnimation(GetComponent<CardManager>().EnergyObject, 0, 0);
        VisualSystem.Instance.CreateFloatingText(energyFloatingText, this.gameObject, Color.magenta);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator AfterDecreaseEnergyEvent(float amount, CoroutineTree tree)
    {
        heroEvents.AfterHeroLosesEnergy(this.gameObject, tree);

       tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator SetEnergyToValue(float amount, CoroutineTree tree)
    {

        tree.AddCurrent(SetEnergyToValueCoroutine(amount, tree));        

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator SetEnergyToValueCoroutine(float amount, CoroutineTree tree)
    {
        tree.AddCurrent(BeforeSetEnergyToValueEvent(amount, tree));

       tree.AddCurrent(SetEnergyToValueLogic(amount,tree));

       tree.AddCurrent(AfterSetEnergyToValueEvent(amount, tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator BeforeSetEnergyToValueEvent(float amount, CoroutineTree tree)
    {   
        float oldEnergy = Energy;
        if(amount > oldEnergy)
        heroEvents.BeforeHeroGainsEnergy(this.gameObject, tree);
        else if(amount < oldEnergy)
        heroEvents.BeforeHeroLosesEnergy(this.gameObject, tree);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator SetEnergyToValueLogic(float amount, CoroutineTree tree)
    {

        float oldEnergy = Energy;
        Energy = amount;
        var energyDifference = amount - oldEnergy;
        string energyFloatingText;
        if(energyDifference >=0)
        {
            energyFloatingText = "+" +energyDifference +" ENERGY";
        }else
        {
            energyFloatingText = "-" +Mathf.Abs(energyDifference) +" ENERGY";
        }

        VisualSystem.Instance.ImageUpdateAnimation(GetComponent<CardManager>().EnergyObject, 0, 0);
        VisualSystem.Instance.CreateFloatingText(energyFloatingText, this.gameObject, Color.magenta);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    IEnumerator AfterSetEnergyToValueEvent(float amount, CoroutineTree tree)
    {
        float oldEnergy = Energy;
        if(amount > oldEnergy)
        heroEvents.AfterHeroGainsEnergy(this.gameObject, tree);
        else if(amount < oldEnergy)
        heroEvents.AfterHeroLosesEnergy(this.gameObject, tree);

         tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public IEnumerator SetHealth(int amount, CoroutineTree tree)
    {
        if(amount > baseHealth)
        baseHealth = amount;        
        Health = amount;
        
        tree.AddCurrent(CheckDeath(tree, amount));

        tree.CorQ.CoroutineCompleted();
        yield return null;

    }

    void Awake()
    {
        heroEvents = gameObject.AddComponent<HeroEvents>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
