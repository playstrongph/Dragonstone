using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuffEffectAnimationHeroQCommand : Command
{
    
    GameObject Target;
    BuffComponent buffComponent;
    GameObject buffAnimation;

    public BuffEffectAnimationHeroQCommand(GameObject Target, BuffComponent buffComponent)
    {
        this.Target = Target;
        this.buffComponent = buffComponent;
    }

    public override void StartCommandExecution()
    {
        
        Debug.Log("Play Add Buff Animation");

        List<GameObject> buffAnims = BuffSystem.Instance.buffAnimations;

        foreach(GameObject buffAnim in buffAnims )
        {
            if(buffAnim.GetComponent<BuffAnimationManager>().buffName == buffComponent.buffAsset.buffBasicInfo.buffName)
            buffAnimation = buffAnim;

        }

        //GameObject useBuffAnimation = GameObject.Instantiate(buffAnimation, Target.transform) as GameObject;

        if(buffComponent.buffAnimationObject != null) {}
        else
        {
              buffComponent.buffAnimationObject = GameObject.Instantiate(buffAnimation, Target.transform) as GameObject;
              buffComponent.buffAnimationObject.transform.SetParent(Target.transform);
        }
      


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
