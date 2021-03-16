using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TextUpdateAnimationCommand : Command
{
    
    int currValue;
    int maxValue;
    GameObject Target;

    
    public TextUpdateAnimationCommand(GameObject Target, int currValue, int maxValue)
    {
        this.currValue = currValue;
        this.maxValue = maxValue;
        this.Target = Target;
    }

    public override void StartCommandExecution()
    {
       //Debug.Log("Text Update Animation");
        var origScale = Target.transform.localScale;

        Target.GetComponent<TextUpdateVisual>().TextUpdateAnimation(currValue, maxValue, origScale);

        // float delay = 2*Target.GetComponent<TextUpdateVisual>().animDuration;
        // var s = DOTween.Sequence();
        // s.AppendInterval(delay).OnComplete(()=>{  //delay is because object being destroyed
        //     Command.CommandExecutionComplete();    
        // });

        //Command.CommandExecutionComplete();

    }

   



    
    
    
}
