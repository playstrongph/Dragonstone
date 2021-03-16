using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DelayHeroQCommand : Command
{
    
    float value;

    GameObject Target;

    
    public DelayHeroQCommand(GameObject Target, float value)
    {
        this.value = value;
        this.Target = Target;
        
    }

    public override void StartCommandExecution()
    {
        

        Sequence s = DOTween.Sequence();
        s.AppendInterval(value)        
        
        .OnComplete(()=>{ Target.GetComponent<HeroQueue>().CommandExecutionComplete();});


    }


}
