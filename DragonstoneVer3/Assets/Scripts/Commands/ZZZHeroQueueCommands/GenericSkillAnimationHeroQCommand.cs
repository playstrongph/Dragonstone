using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GenericSkillAnimationHeroQCommand : Command
{
    
    GameObject Target;
    
    GenericSkillName genSkillName;

    GameObject genSkillAnimation;

    public GenericSkillAnimationHeroQCommand(GameObject Target, GenericSkillName genSkillName)
    {
        this.Target = Target;
        this.genSkillName = genSkillName;
        
    }

    public override void StartCommandExecution()
    {
        
        Debug.Log("Play Generic Skill Animation " +genSkillName);

        List<GameObject> genericSkillAnims = SkillSystem.Instance.GenericSkillAnimations;

        foreach(GameObject genericSkillAnim in genericSkillAnims )
        {
            if(genericSkillAnim.GetComponent<SkillAnimationManager>().genericSkillName == genSkillName)
            genSkillAnimation = genericSkillAnim;

        }

        //GameObject useBuffAnimation = GameObject.Instantiate(buffAnimation, Target.transform) as GameObject;

        // if(buffComponent.buffAnimationObject != null) {}
        // else
        // {
        //       buffComponent.buffAnimationObject = GameObject.Instantiate(buffAnimation, Target.transform) as GameObject;
        //       buffComponent.buffAnimationObject.transform.SetParent(Target.transform);
        // }     
       

        var genSkillAnimationObject = GameObject.Instantiate(genSkillAnimation, Target.transform) as GameObject;
        genSkillAnimationObject.transform.SetParent(Target.transform);


        //Animation - has AutoDestruct feature - otherwise, sequence below is required
        
        // Sequence s = DOTween.Sequence();

        // //Animation play time, in the future this can be included in the buff basic info SO
        // s.AppendInterval(useBuffAnimation.GetComponent<BuffCardManager>().buffAnimDuration)
        // .OnStepComplete(()=>{ GameObject.Destroy(useBuffAnimation);})
        // .OnComplete(()=>{ Command.CommandExecutionComplete();});


        //temp
        Target.GetComponent<HeroQueue>().CommandExecutionComplete();

    }

}
