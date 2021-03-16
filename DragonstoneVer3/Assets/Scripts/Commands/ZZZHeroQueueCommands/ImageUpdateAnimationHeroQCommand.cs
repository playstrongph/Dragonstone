using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageUpdateAnimationHeroQCommand : Command
{
    
    int currValue;
    int maxValue;
    GameObject Target;

    public ImageUpdateAnimationHeroQCommand(GameObject Target, int currValue, int maxValue)
    {
        this.currValue = currValue;
        this.maxValue = maxValue;
        this.Target = Target;
    }

    public override void StartCommandExecution()
    {
       Debug.Log("Image Update Animation");
       Target.GetComponent<ImageUpdateVisual>().ImageUpdateAnimation(currValue, maxValue);
       Target.GetComponent<HeroQueue>().CommandExecutionComplete();
    }



    
    
    
}
