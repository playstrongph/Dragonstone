using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBuffCounterHeroQCommand : Command
{
    
    BuffComponent buffComponent;
    int buffCounter;

    GameObject Target;

    public UpdateBuffCounterHeroQCommand(GameObject Target, BuffComponent buffComponent, int buffCounter)
    {
        this.buffComponent = buffComponent;
        this.buffCounter = buffCounter;
        this.Target = Target;
    }

    public override void StartCommandExecution()
    {
        buffComponent.buffVisualObject.GetComponent<BuffCardManager>().BuffText.text = buffCounter.ToString();     
        
        Target.GetComponent<HeroQueue>().CommandExecutionComplete();
    }




}
