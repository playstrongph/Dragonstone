using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DebuffEffectAnimationCommand : Command
{
    
    GameObject Target;
    BuffComponent buffComponent;
    GameObject buffAnimation;

    public DebuffEffectAnimationCommand (GameObject Target, BuffComponent buffComponent)
    {
        this.Target = Target;
        this.buffComponent = buffComponent;
    }

    public override void StartCommandExecution()
    {
     
        List<GameObject> buffAnims = BuffSystem.Instance.debuffAnimations;

        foreach(GameObject buffAnim in buffAnims )
        {
            if(buffAnim.GetComponent<BuffAnimationManager>().buffName == buffComponent.buffAsset.buffBasicInfo.buffName)
            buffAnimation = buffAnim;
        }

        if(buffComponent.buffAnimationObject != null) {}
        else
        {
              buffComponent.buffAnimationObject = GameObject.Instantiate(buffAnimation, Target.transform) as GameObject;
              buffComponent.buffAnimationObject.transform.SetParent(Target.transform);
        }

        Command.CommandExecutionComplete();

    }

}
