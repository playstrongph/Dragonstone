using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuffEffectAnimationCommand : Command
{
    
    GameObject Target;
    BuffComponent buffComponent;
    GameObject buffAnimation;

    public BuffEffectAnimationCommand(GameObject Target, BuffComponent buffComponent)
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

        if(buffComponent.buffAnimationObject != null) {}
        else
        {
              buffComponent.buffAnimationObject = GameObject.Instantiate(buffAnimation, Target.transform) as GameObject;
              buffComponent.buffAnimationObject.transform.SetParent(Target.transform);
        }
      
        Command.CommandExecutionComplete();
      
    }

}
