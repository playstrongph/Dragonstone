using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ResurrectHeroAnimationCommand : Command
{
    
    GameObject Target;
    
    GenericSkillName genSkillName;

    GameObject genSkillAnimation;

   
    public ResurrectHeroAnimationCommand(GameObject Target, GenericSkillName genSkillName)
    {
        this.Target = Target;
        this.genSkillName = genSkillName;
        
    }

    public override void StartCommandExecution()
    {
        
        Sequence s = DOTween.Sequence();
        
         float heroDiesAnimDelay = 2.5f;

        s.AppendInterval(heroDiesAnimDelay);  //to account for HeroDies Animation
        
        s.AppendCallback(()=>{

           
            Target.GetComponent<CardManager>().ShowHero();
            Target.GetComponent<HeroLogic>().RestoreHeroBaseStats();  //this is where the skillpanel is hidden

            Debug.Log("Play Generic Skill Animation " +genSkillName);

            List<GameObject> genericSkillAnims = SkillSystem.Instance.GenericSkillAnimations;

            foreach(GameObject genericSkillAnim in genericSkillAnims )
            {
                if(genericSkillAnim.GetComponent<SkillAnimationManager>().genericSkillName == genSkillName)
                genSkillAnimation = genericSkillAnim;

            }

            var genSkillAnimationObject = GameObject.Instantiate(genSkillAnimation, Target.transform) as GameObject;
            genSkillAnimationObject.transform.SetParent(Target.transform);

            VisualSystem.Instance.CreateFloatingText("RESURRECT", Target, Color.yellow);
            
        });     
      
        //Animation - has AutoDestruct feature - otherwise, sequence below is required
        
        // Sequence s = DOTween.Sequence();

        // //Animation play time, in the future this can be included in the buff basic info SO
        // s.AppendInterval(useBuffAnimation.GetComponent<BuffCardManager>().buffAnimDuration)
        // .OnStepComplete(()=>{ GameObject.Destroy(useBuffAnimation);})
        // .OnComplete(()=>{ Command.CommandExecutionComplete();});

        Command.CommandExecutionComplete();

    }

}
