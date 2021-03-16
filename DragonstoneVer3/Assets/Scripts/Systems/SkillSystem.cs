using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{

    public static SkillSystem Instance;

    public List<GameObject> GenericSkillAnimations;    

    public List<GameObject> HeroSkillAnimations;    
    public GameObject HealEffectPrefab;

    public CoroutineTree tree = new CoroutineTree();

   

    private void Awake() {
        if (Instance == null) {
            Instance = this;            
        }else{
            Destroy(gameObject);            
        }
       
    }
    
    ///<Summary>
    /// This is the initiator for UseSkill Logic - the Root Coroutine
    ///</Summary>
    public void UseSkill (SkillComponent skill, GameObject Target)
    { 
        tree.Start();
        tree.AddRoot(UseSkillLogic(skill, Target, tree));
        
    }

    ///<Summary>
    /// This is the Root Coroutine
    ///</Summary>
    public IEnumerator UseSkillLogic (SkillComponent skill, GameObject Target, CoroutineTree tree)
    { 

        SkillType skillType = skill.skillType;

        switch(skillType)
        {
            case SkillType.ACTIVE:
            if(skill.heroLogic.gameObject.GetComponent<SILENCE>() != null)
            {skill.heroLogic.gameObject.GetComponent<SILENCE>().BuffEffectAnimation(skill.heroLogic.gameObject); }

            else 
            //StartCoroutine(SkillSequence(skill,Target)); 
            tree.AddCurrent(SkillSequence(skill, Target, tree));

            break;

            case SkillType.PASSIVE:
            if(skill.heroLogic.gameObject.GetComponent<CENSOR>() != null)
            {skill.heroLogic.gameObject.GetComponent<CENSOR>().BuffEffectAnimation(skill.heroLogic.gameObject); }

            else            
            //StartCoroutine(SkillSequence(skill,Target));
            tree.AddCurrent(SkillSequence(skill, Target, tree));

            break;

            case SkillType.PASSIVE_CD:
            if(skill.heroLogic.gameObject.GetComponent<CENSOR>() != null)
            {skill.heroLogic.gameObject.GetComponent<CENSOR>().BuffEffectAnimation(skill.heroLogic.gameObject); }

            else 
            //StartCoroutine(SkillSequence(skill,Target));
            tree.AddCurrent(SkillSequence(skill, Target, tree));

            break;

            default:
            Debug.Log("No Correct SkillType Assigned");

            break;
        } 


        tree.CorQ.CoroutineCompleted();
        yield return null;    
    }



    IEnumerator SkillSequence (SkillComponent skill, GameObject Target, CoroutineTree tree)
    {
        //yield return (StartCoroutine(UseSkillSequence(skill, Target)));
        tree.AddCurrent(UseSkillSequence(skill, Target,tree));

        tree.CorQ.CoroutineCompleted();
        yield return null;

       
    }
   

    IEnumerator UseSkillSequence (SkillComponent skill, GameObject Target, CoroutineTree tree)
    {
        Debug.Log("Using Skill!");
       

        ///<Summary>
        /// Keep this as a method since this shall be the effective "Start"
        /// of Basic Attack and the Hero skills.
        /// UseSkill shall be overriden with the Root Coroutine for the users
         ///</Summary>
        skill.UseSkill(skill.heroLogic.gameObject, Target);


        tree.CorQ.CoroutineCompleted();
        yield return null;
    }
 



    public void ReduceAllSkillCooldowns(GameObject Target, int counters)
    {
        var allSkills = Target.GetComponent<HeroLogic>().skills;

        foreach(var skill in allSkills)
        {
            if(skill.skillType == SkillType.ACTIVE || skill.skillType == SkillType.PASSIVE_CD)
            {   
                if(skill.GetComponent<SkillCDImmunity>() != null){}else
                {
                    skill.currCoolDown -= counters;
                    if(skill.currCoolDown < 0) skill.currCoolDown = 0;
                    VisualSystem.Instance.UpdateSkillCooldown(skill, skill.currCoolDown);
                }    

                
            }
        }
    }

    public void IncreaseAllSkillCooldowns(GameObject Target, int counters)
    {
        var allSkills = Target.GetComponent<HeroLogic>().skills;

        foreach(var skill in allSkills)
        {
            if(skill.skillType == SkillType.ACTIVE || skill.skillType == SkillType.PASSIVE_CD)
            {
                if(skill.GetComponent<SkillCDImmunity>() != null){}else
                {
                    skill.currCoolDown += counters;
                    if(skill.currCoolDown < 0) skill.currCoolDown = 0;
                    VisualSystem.Instance.UpdateSkillCooldown(skill, skill.currCoolDown);
                }
                
            }
        }
    }

    public IEnumerator ResetAllSkillCooldownsToMax(GameObject Target, CoroutineTree tree)
    {
        var allSkills = Target.GetComponent<HeroLogic>().skills;

        foreach(var skill in allSkills)
        {
            if(skill.skillType == SkillType.ACTIVE || skill.skillType == SkillType.PASSIVE_CD)
            {
                if(skill.GetComponent<SkillCDImmunity>() != null){}else
                {
                    skill.currCoolDown = skill.baseCoolDown;
                    if(skill.currCoolDown < 0) skill.currCoolDown = 0;

                    VisualSystem.Instance.UpdateSkillCooldown(skill, skill.currCoolDown);
                   
                }
                 
            }
        }
        VisualSystem.Instance.CreateFloatingText("MAX Cooldown", Target, Color.magenta);

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    

    public IEnumerator RefreshAllSkillCooldownsToReady(GameObject Target, CoroutineTree tree)
    {
        var allSkills = Target.GetComponent<HeroLogic>().skills;

        foreach(var skill in allSkills)
        {
            if(skill.skillType == SkillType.ACTIVE || skill.skillType == SkillType.PASSIVE_CD)
            {

                 if(skill.GetComponent<SkillCDImmunity>() != null){}else
                 {
                     skill.currCoolDown = 0;
                    //if(currCoolDown < 0) currCoolDown = 0;
                    VisualSystem.Instance.UpdateSkillCooldown(skill, skill.currCoolDown);
                 }
                
            }
        }

        tree.CorQ.CoroutineCompleted();
        yield return null;
    }

    public void SetAllSkillCooldownsToValue(GameObject Target, int value)
    {
        var allSkills = Target.GetComponent<HeroLogic>().skills;

        foreach(var skill in allSkills)
        {
            if(skill.skillType == SkillType.ACTIVE || skill.skillType == SkillType.PASSIVE_CD)
            {
                if(skill.GetComponent<SkillCDImmunity>() != null){}else
                {
                    skill.currCoolDown = value;
                    if(skill.currCoolDown < 0) skill.currCoolDown = 0;
                    VisualSystem.Instance.UpdateSkillCooldown(skill, skill.currCoolDown);
                }
                
                
            }
        }
    }

    public IEnumerator DestroySkillEffect(SkillEffect skillEffect, CoroutineTree tree)
    {
        
       if(skillEffect != null)
       GameObject.Destroy(skillEffect);       

        tree.CorQ.CoroutineCompleted();
        yield return null;
          
    }

    

     

    


}
