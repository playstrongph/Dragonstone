using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillAttackEffect1 : Command  //TODO:  need to rename this class
{
    
    GameObject Attacker;
    GameObject Target;
    SkillComponent skill;
    GameObject specialEffect;
    GameObject specialEffectObject;

    float animDelay = 0.5f;
    public SkillAttackEffect1(GameObject Attacker, GameObject Target, SkillComponent skill)
    {
        this.Attacker = Attacker;
        this.Target = Target;
        this.skill = skill;
    }

     public SkillAttackEffect1(GameObject Attacker, GameObject Target, SkillComponent skill, float animDelay)
    {
        this.Attacker = Attacker;
        this.Target = Target;
        this.skill = skill;
        this.animDelay = animDelay;
    }

    public override void StartCommandExecution()
    {
        //FindSkillEffect Method
        foreach(var skillAnim in SkillSystem.Instance.HeroSkillAnimations)
        {
            
            if (skillAnim.GetComponent<SkillAnimationManager>().skillAsset == skill.skillAsset)
            {
                specialEffectObject = skillAnim.GetComponent<SkillAnimationManager>().heroSkillAnimaiton;                
            }

        }

        if(specialEffectObject != null){}else
        {
            specialEffectObject = GlobalSettings.Instance.specialEffects[1];

            //VisualSystem.Instance.CreateFloatingText("No Hero Skill Animation", Attacker, Color.red);
        }

        specialEffect = GameObject.Instantiate(specialEffectObject, Attacker.transform.position, Quaternion.identity) as GameObject; //default anim

        //delays in Sequence S is for the animation itself, not the queueing

        Sequence s = DOTween.Sequence();

        s.AppendInterval(shortAnimationDuration);
        s.Append(specialEffect.transform.DOMove(Target.transform.position, shortAnimationDuration));
        
        s.AppendInterval(shortAnimationDuration)
        .OnStepComplete(()=> GameObject.Destroy(specialEffect))
        .OnComplete(()=> GameObject.Destroy(specialEffect));

        Sequence s1 = DOTween.Sequence();

        s1.AppendInterval(animDelay*2).OnComplete(()=>{
            Command.CommandExecutionComplete();
        });
        
        //Command.CommandExecutionComplete();
        
        
    }


}
