using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class ImageUpdateAnimationCommand : Command
{
    
    int currValue;
    int maxValue;
    GameObject Target;

   

    public ImageUpdateAnimationCommand(GameObject Target, int currValue, int maxValue)
    {
        this.currValue = currValue;
        this.maxValue = maxValue;
        this.Target = Target;
    }

    public override void StartCommandExecution()
    {
      // Debug.Log("Image Update Animation");
       
       Target.GetComponent<ImageUpdateVisual>().ImageUpdateAnimation(currValue, maxValue);

       Command.CommandExecutionComplete(); //Located in ImageUpdateVisual.cs

    }



    
    
    
}
