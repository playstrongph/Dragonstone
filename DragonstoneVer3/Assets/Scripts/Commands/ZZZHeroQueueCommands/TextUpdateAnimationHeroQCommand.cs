using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextUpdateAnimationHeroQCommand : Command
{
    
    int currValue;
    int maxValue;
    GameObject Target;

    public TextUpdateAnimationHeroQCommand(GameObject Target, int currValue, int maxValue)
    {
        this.currValue = currValue;
        this.maxValue = maxValue;
        this.Target = Target;
    }

    public override void StartCommandExecution()
    {
    //    Debug.Log("Text Update Animation");
    //    Target.GetComponent<TextUpdateVisual>().TextUpdateAnimation(currValue, maxValue, origScale);
    //     Target.GetComponent<HeroQueue>().CommandExecutionComplete();
    }



    
    
    
}
