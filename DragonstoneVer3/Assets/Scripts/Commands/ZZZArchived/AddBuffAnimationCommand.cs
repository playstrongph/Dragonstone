using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AddBuffAnimationCommand : Command
{
    
    GameObject Target;
    BuffType buffType;
    GameObject buffAnimation;

    public AddBuffAnimationCommand(GameObject Target, BuffType buffType)
    {
        this.Target = Target;
        this.buffType = buffType;
    }

    public override void StartCommandExecution()
    {
        
        Debug.Log("Play Add Buff Animation");

        List<GameObject> buffAnims = BuffSystem.Instance.buffAnimations;
        foreach(GameObject buffAnim in buffAnims )
        {
            if(buffAnim.GetComponent<BuffCardManager>().buffType == buffType)
            buffAnimation = buffAnim;

        }

        GameObject useBuffAnimation = GameObject.Instantiate(buffAnimation, Target.transform) as GameObject;

        Sequence s = DOTween.Sequence();

        //Animation play time, in the future this can be included in the buff basic info SO
        s.AppendInterval(useBuffAnimation.GetComponent<BuffCardManager>().buffAnimDuration)
        .OnStepComplete(()=>{ GameObject.Destroy(useBuffAnimation);})
        .OnComplete(()=>{ Command.CommandExecutionComplete();});


    }

}
