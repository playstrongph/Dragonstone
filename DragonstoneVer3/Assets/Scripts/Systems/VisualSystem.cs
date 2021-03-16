using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VisualSystem : MonoBehaviour
{

    public static VisualSystem Instance;
    // Start is called before the first frame update

    private void Awake() {
        if (Instance == null) {
            Instance = this;            
        }else{
            Destroy(gameObject);            
        }
    }

    //public delegate void MultipleAnimDelegate(GameObject Attacker, GameObject Target);
    //public Command.MultipleAnimDelegate m_ParallelCommands;

    // void Start()
    // {
    //     m_ParallelCommands = Command.m_ParallelCommands;
    // }

    

    public void AttackSingleHero(GameObject Attacker, GameObject Target)  //already sequential by default
    {
        new AttackSingleHeroVisualCommand(Attacker, Target).AddToQueue();
        
    }

    public void CriticalAttackSingleHero(GameObject Attacker, GameObject Target)
    {
       new CriticalAttackSinglerHeroVisualCommand(Attacker, Target).AddToQueue();
        
    }

    // public void CriticalAttackSingleHeroQ(GameObject Attacker, GameObject Target)
    // {
    //     new CriticalAttackSingleHeroVisualHeroQ(Attacker, Target).AddToHeroQueue(Attacker);
        
    // }

    public void ShowTargets(List<GameObject> validTargets)
    {
       
        new ShowTargetsCommand(validTargets).AddToQueue();
        
    }

    public void TextUpdateAnimation(GameObject Target, int currValue, int maxValue)
    {
        new TextUpdateAnimationCommand(Target, currValue, maxValue).AddToQueue();
    }

    public void ImageUpdateAnimation(GameObject Target, int currValue, int maxValue)
    {
        new ImageUpdateAnimationCommand(Target, currValue, maxValue).AddToQueue();
    }

    public void CreateFloatingText(string floatingText, GameObject target, Color textColor)
    {
        new CreateFloatingTextCommand(floatingText, target, textColor).AddToQueue();
    }

    public void HideTargetGlow(GameObject go)
    {
        new HideTargetCommand(go).AddToQueue();
    }

    public void HideTargetsGlow(List<GameObject> go)
    {
        new HideTargetsGlowCommand(go).AddToQueue();
    }

    // public void HideTargetsGlow(List<GameObject> go, float commandDelay)
    // {
    //     new HideTargetsGlowCommand(go, commandDelay).AddToQueue();
    // }


    public void UpdateSkillCooldown(SkillComponent skill, int value)
    {
        new UpdateSkillCooldownCommand(skill, value).AddToQueue();
    }

    // public void UpdateSkillCooldown(SkillComponent skill, int value, float commandDelay)
    // {
    //     new UpdateSkillCooldownCommand(skill, value, commandDelay).AddToQueue();
    // }

    // public void UpdateSkillCooldownHeroQ(GameObject Target, SkillComponent skill, int value)
    // {
    //     new UpdateSkillCooldownHeroQCommand(Target, skill, value).AddToHeroQueue(Target);
    // }

    public void CreateDamageEffect(int amount, GameObject Target)
    {
        new CreateDamageEffectCommand(amount, Target).AddToQueue();
    }

    // public void CreateDamageEffect(int amount, GameObject Target, float commandDelay)
    // {
    //     new CreateDamageEffectCommand(amount, Target, commandDelay).AddToQueue();
    // }

    // public void CreateDamageEffectHeroQ(int amount, GameObject Target)
    // {
    //     new DamageEffectHeroQCommand(amount, Target).AddToHeroQueue(Target);
    // }

    public void CreateHealEffect(int amount,GameObject target)
    {
        new CreateHealEffectCommand(amount, target).AddToQueue();
    }


    public void AddBuffSymbol(BuffComponent bc, int cooldown, GameObject target)
    {
        new AddBuffSymbolCommand(bc, cooldown, target) .AddToQueue();
    }

     public void DestroyBuffSymbol(BuffComponent bc)
    {
        new DestroyBuffSymbolCommand(bc) .AddToQueue();

    }


    public void AddBuffAnimation(GameObject Target, BuffType buffType)
    {
        new AddBuffAnimationCommand(Target, buffType).AddToQueue();
    }
    public void BuffEffectAnimation(GameObject Target, BuffComponent buffComponent)
    {
        new BuffEffectAnimationCommand(Target, buffComponent).AddToQueue();
    }

    public void GenericSkillEffectAnimation(GameObject Target,GenericSkillName genSkillName)
    {
        new GenericSkillAnimationCommand(Target, genSkillName).AddToQueue();
    }

    
    public void ResurrectHeroAnimation(GameObject Target,GenericSkillName genSkillName)
    {
        new ResurrectHeroAnimationCommand(Target, genSkillName).AddToQueue();
    }

    // public void ResurrectHeroAnimation(GameObject Target,GenericSkillName genSkillName, float commandDelay)
    // {
    //     new ResurrectHeroAnimationCommand(Target, genSkillName, commandDelay).AddToQueue();
    // }

    public void GenericSkillEffectAnimationHeroQ(GameObject Target,GenericSkillName genSkillName)
    {
        new GenericSkillAnimationHeroQCommand(Target, genSkillName).AddToHeroQueue(Target);
    }

    public void DebuffEffectAnimation(GameObject Target, BuffComponent buffComponent)
    {
        new DebuffEffectAnimationCommand(Target, buffComponent).AddToQueue();
    }

    public void RemoveBuff(BuffComponent bc)
    {        
        new RemoveBuffCommand(bc).AddToQueue();
    }

    public void UpdateBuffCounter(BuffComponent bc, int buffCounter)
    {
        new UpdateBuffCounterCommand(bc, buffCounter).AddToQueue();
    }


    public void ClearAllGlows()
    {
        new ClearAllGlowsCommand(HeroManager.Instance.LivingHeroesList()).AddToQueue();
    }

    public void SetActiveHero(HeroLogic hl)
    {        
        new SetHeroActiveCommand(hl).AddToQueue();
    }

    public void ShowActiveHero(HeroLogic hl, float commandDelay)
    {        
        new SetHeroActiveCommand(hl, commandDelay).AddToQueue();
    }

    public void SetHeroInactive(HeroLogic hl)
    {
        new SetHeroInactiveCommand(hl).AddToQueue();
    }

    //For TunrManager 
    public void UpdateHeroTimer(List<HeroTimer> ht)
    {
        new UpdateHeroTimerCommand(ht).AddToQueue();
    }

    //For skills and other non-turn manager events affecting Energy
    public void UpdateSingleHeroTimer(HeroLogic heroLogic)
    {
        new UpdateSingleHeroTimerCommand(heroLogic).AddToQueue();
    }

    public void UseSkillPreview(SkillComponent skill)
    {
        // Sequential By Default
        new UseSkillPreviewCommand(skill).AddToQueue();
        
    }

    public void ShowSkillPreview(SkillHoverPreview skillHoverPreview)
    {
        new ShowSkillPreviewCommand(skillHoverPreview).AddToQueue();
    }

    public void HideSkillPreview(SkillHoverPreview skillHoverPreview)
    {
        new HideSkillPreviewCommand(skillHoverPreview).AddToQueue();

        //Don't impwlement delay here
    }

    public void HideSkillPreviewHeroQ(GameObject Target, SkillHoverPreview skillHoverPreview)
    {
        new HideSkillPreviewHeroQCommand(Target, skillHoverPreview).AddToHeroQueue(Target);
    }

    public void SkillAttackEffect1(GameObject Attacker, GameObject Target, SkillComponent skill)
    {
        new SkillAttackEffect1(Attacker, Target, skill).AddToQueue();
    }

    public void SkillAttackEffect1(GameObject Attacker, GameObject Target, SkillComponent skill, float animDelay)
    {
        new SkillAttackEffect1(Attacker, Target, skill, animDelay).AddToQueue();
    }


    public Command SkillAttackEffectParallel(GameObject Attacker, GameObject Target)
    {
        foreach(var enemyHero in HeroManager.Instance.AllLivingEnemiesList(Attacker))
        {
            new SkillAttackEffectHeroQCommand(Attacker, enemyHero).AddToHeroQueue(enemyHero);
        }        
        
        return null;
        
    }

    public void ParallelSkillEffectsAttackerToTargets(GameObject Attacker, GameObject Target)
    {
        new ParallelSkillEffectsAttackerToTargets(Attacker, Target).AddToQueue();
    }

    // public void GenericParallelActions(GameObject Attacker, GameObject Target, Command.ParallelCommandsDelegate parallelCommand)
    // {
        
    //     Command.m_ParallelCommands = parallelCommand;        
    //     new GenerciParallelCommands(Attacker, Target, Command.m_ParallelCommands).AddToQueue();
    // }

    public void Delay(float value)
    {
        new DelayCommand(value).AddToQueue();
    }

    public void Delay()
    {
        new DelayCommand(0.5f).AddToQueue();
    }

   

    public void HeroDiesAnimation(GameObject hero)
    {
        new HeroDiesAnimationCommand(hero).AddToQueue();
    }

    // public void EndTurn(GameObject Target)
    // {
    //     //new DelayCommand(.5f);

    //     new EndTurnCommand(Target).AddToQueue();
    // }

    // public void ShowHeroHeroQ (GameObject hero)
    // {
    //     new ShowHeroHeroQCommand(hero).AddToHeroQueue(hero);
    // }

}
