using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpdateBuffCounterCommand : Command
{
    
    BuffComponent buffComponent;
    int buffCounter;
 public UpdateBuffCounterCommand(BuffComponent buffComponent, int buffCounter)
    {
        this.buffComponent = buffComponent;
        this.buffCounter = buffCounter;
    }

    public override void StartCommandExecution()
    {
        buffComponent.buffVisualObject.GetComponent<BuffCardManager>().BuffText.text = buffCounter.ToString();     
        
        
        Command.CommandExecutionComplete();

    }




}
